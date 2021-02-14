using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DraughtLeague.DAL;
using DraughtLeague.DAL.Models;
using DraughtLeague.Untappd;
using DraughtLeague.Untappd.Endpoints.Beer;
using DraughtLeague.Untappd.Endpoints.Search;
using DraughtLeague.Untappd.Models.Beer.Checkins;
using DraughtLeague.Untappd.Models.Beer.Info;
using DraughtLeague.Untappd.Models.Search.Beer;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Beer = DraughtLeague.Untappd.Models.Search.Beer.Beer;

namespace DraughtLeague.Console
{
    class Program {
        private static IConfiguration _configuration;
        private static LeagueDbContext _dal = null;
        private static UntappdClient _untappdClient = null;

        private static List<DAL.Models.CheckIn> _checkins { get; set; }

        static async Task Main(string[] args) {
            _configuration = new ConfigurationBuilder()
                .AddJsonFile("secrets.json", optional: false, reloadOnChange: true)
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddCommandLine(args)
                    .Build();

            _dal = loadDbContext();
            
            System.Console.CursorVisible = true;

            bool isActive = true;
            while (isActive) {
                System.Console.Write("> ");
                string input = System.Console.ReadLine().TrimStart('>', ' ');
                
                if (string.IsNullOrWhiteSpace(input))
                    continue;

                if (!Regex.IsMatch(input, @"^(\w+)(\s+.+)*$")) {
                    System.Console.WriteLine("Unrecognized Input.");
                    System.Console.Write("> ");
                    continue;
                }
                
                string command = Regex.Match(input, @"^(\w+)(\s+.+)*$").Groups[1].Value.ToLower();
                string[] parameters = Regex.Match(input, @"^(\w+)(\s+.+)*$").Groups[2].Value
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => x.ToLower()).ToArray();
                
                switch (command) {
                    case "fetch" :
                        if (!parameters.Any())
                            break;
                        string id = parameters[0];
                        if (id.ToLower() == "all")
                            await fetchAllCheckInsAsync();
                        else {
                            int? minId = parameters.Length > 1 ? Convert.ToInt32(parameters[1]) : null;
                            await fetchCheckInsAsync(Convert.ToInt32(id), minId);
                        }

                        break;
                        
                    case "commit":
                        await commitCheckInsAsync();
                        break;

                    case "search" :
                        string query = string.Join(" ", parameters);
                        await searchAsync(query);
                        break;

                    case "exit" :
                    case "quit" :
                        isActive = false;
                        break;
                }
            }
        }

        static async Task searchAsync(string query) {
            System.Console.WriteLine($"Searching Beers for \"{query}\"...");
            
            Task<List<BeerItem>> search = UntappdClient.Search().Beer(query).GetAsync();
            List<BeerItem> results = await search;

            foreach (BeerItem item in results) {
                Beer beer = item.Beer;
                System.Console.WriteLine($"{beer.Name} ({item.Brewery.Name}) [{item.Beer.Rating}]");
            }

            return;
        }

        static LeagueDbContext loadDbContext() {
            if (_dal != null)
                return _dal;

            string dbServer = _configuration["FantasyDraughtDatabase:Server"];
            string database = _configuration["FantasyDraughtDatabase:Database"];
            string userId = _configuration["FantasyDraughtDatabase:UserId"];
            string password = _configuration["FantasyDraughtDatabase:Password"];

            string connectionString = string.Format(_configuration.GetConnectionString("FantasyDraftProduction"), dbServer, database, userId, password);
            return new LeagueDbContext(connectionString);
        }

        static async Task commitCheckInsAsync() {
            if (_checkins == null || !_checkins.Any())
                System.Console.WriteLine("No data has been Fetch'd for Commit");

            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("UntappdId");
            dt.Columns.Add("Rating"); 
            dt.Columns.Add("Timestamp");
            
            foreach (CheckIn checkIn in _checkins) {
                DataRow row = dt.Rows.Add();
                row.ItemArray = new object[] {checkIn.Id, checkIn.UntappdId, checkIn.Rating, checkIn.Timestamp};
            }

            System.Console.Write($"Merging {_checkins.Count} Check Ins into database...");

            string connectionString = _dal.Database.GetConnectionString();
            using (SqlConnection cn = new SqlConnection(connectionString)) {
                await cn.OpenAsync();

                SqlCommand cmd = new SqlCommand("", cn);

                cmd.CommandText = "SELECT TOP 0 * INTO #CheckIns FROM CheckIns;";
                await cmd.ExecuteNonQueryAsync();

                SqlBulkCopy bulkCopy = new SqlBulkCopy(cn) {
                    DestinationTableName = "tempdb..#CheckIns", 
                    BatchSize = 1000
                };
                await bulkCopy.WriteToServerAsync(dt);
                
                cmd.CommandText = @"MERGE INTO CheckIns AS TARGET
                    USING #CheckIns AS SOURCE
                    ON TARGET.Id = SOURCE.Id
                    WHEN NOT MATCHED
                        THEN INSERT (Id, UntappdId, Rating, Timestamp) VALUES (SOURCE.Id, SOURCE.UntappdId, SOURCE.Rating, SOURCE.Timestamp);";
                int changes = await cmd.ExecuteNonQueryAsync();

                System.Console.WriteLine($"Complete ({changes} new records).");
                
                await cn.CloseAsync();
            }

            _checkins.Clear();

        }

        static async Task fetchAllCheckInsAsync() {

            var beers = _dal.Beers.Select(x => new {x.UntappdId, MinId = (int?)x.CheckIns.Max(c => c.Id)});
            
            foreach (var beer in beers) {
                await fetchCheckInsAsync(beer.UntappdId, beer.MinId);
            }


        }

        static async Task fetchCheckInsAsync(int untappdId, int? minId) {
            System.Console.Write($"Fetching Checkins for {untappdId}...");

            Info beer = await UntappdClient.Beer(untappdId).Info().Compact().GetDataAsync();
            if (beer == null) {
                System.Console.WriteLine("BID Not Found");
                return;
            }

            System.Console.WriteLine($"{beer.Name} ({beer.Brewery.Name})");

            if (minId == null) { //Find any existing CheckIn data for this beer and use that if within 10 days
                DateTime minIdThreshhold = DateTime.Today.AddDays(-10);
                minId = _dal.CheckIns.Where(x => x.UntappdId == untappdId && x.Timestamp >= minIdThreshhold)
                    .Select(x => (int?) x.Id).DefaultIfEmpty().Max();
            }

            BeerCheckinsEndpoint checkins = UntappdClient.Beer(untappdId).Checkins();
            if (minId != null)
                checkins = checkins.MinId((int)minId);
            
            List<Checkin> c = await checkins.GetDataAsync();
            if (c == null) {
                System.Console.WriteLine("No Checkins Retrieved.");
                return;
            }

            IEnumerable<CheckIn> c2 = c.Select(x => new CheckIn {
                Id = x.Id, 
                UntappdId = untappdId, 
                Rating = x.Rating == 0 ? beer.Rating : x.Rating,
                Timestamp = x.CreatedAt
            });

            foreach (Checkin checkin in c) {
                System.Console.WriteLine($"{checkin.Rating} {checkin.CreatedAt}");
            }

            if (_checkins == null)
                _checkins = new List<CheckIn>();

            _checkins.AddRange(c2);
        }

        private static UntappdClient UntappdClient {
            get {
                if (_untappdClient == null) {
                    string clientId = _configuration["Untappd:ClientId"];
                    string clientSecret = _configuration["Untappd:ClientSecret"];

                    UntappdClient client = new UntappdClient(clientId, clientSecret);
                    _untappdClient = client;
                }

                return _untappdClient;
            }
        }

    }
}
