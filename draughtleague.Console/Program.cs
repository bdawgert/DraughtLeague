using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using DraughtLeague.DAL;
using DraughtLeague.Untappd;
using DraughtLeague.Untappd.Endpoints.Beer;
using DraughtLeague.Untappd.Endpoints.Search;
using DraughtLeague.Untappd.Models.Beer.Checkins;
using DraughtLeague.Untappd.Models.Search.Beer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace DraughtLeague.Console
{
    class Program {
        private static IConfiguration configuration;


        static void Main(string[] args) {
            //var services = new ServiceCollection();

            configuration = new ConfigurationBuilder()
                .AddJsonFile("secrets.json", optional: false, reloadOnChange: true)
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddCommandLine(args)
                    .Build();

            //var serviceProvider = services.AddOptions().BuildServiceProvider();

            string dbServer = configuration["FantasyDraughtDatabase:Server"];
            string database = configuration["FantasyDraughtDatabase:Database"];
            string userId = configuration["FantasyDraughtDatabase:UserId"];
            string password = configuration["FantasyDraughtDatabase:Password"];

            string connectionString = string.Format(configuration.GetConnectionString("FantasyDraftProduction"), dbServer, database, userId, password);
            var context = new EFDbContext(connectionString);

            System.Console.CursorVisible = true;

            bool isActive = true;
            while (isActive) {
                System.Console.Write("> ");
                string input = System.Console.ReadLine().TrimStart('>', ' ');
                
                if (string.IsNullOrWhiteSpace(input))
                    continue;

                if (!Regex.IsMatch(input, @"^(\w+)\s+(.+)$")) {
                    System.Console.WriteLine("Unrecognized Input.");
                    System.Console.Write("> ");
                    continue;
                }
                
                string command = Regex.Match(input, @"^(\w+)(\s+.+)+$").Groups[1].Value;
                string[] parameters = Regex.Match(input, @"^(\w+)(\s+.+)+$").Groups[2].Value.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                
                switch (command) {
                    case "fetch" :
                        fetchDataAsync(Convert.ToInt32(parameters[0]));
                        break;

                    case "search" :
                        string query = string.Join(" ", parameters);
                        searchAsync(query);
                        break;

                    case "exit" :
                    case "quit" :
                        isActive = false;
                        break;
                }
            }
        }

        static async void searchAsync(string query) {
            System.Console.WriteLine($"Searching Beers for \"{query}\"...");

            string clientId = configuration["Untappd:ClientId"];
            string clientSecret = configuration["Untappd:ClientSecret"];

            UntappdClient client = new UntappdClient(clientId, clientSecret);

            List<BeerItem> c = await client.Search().Beer(query).GetAsync();
            
            foreach (BeerItem item in c) {
                Beer beer = item.Beer;
                System.Console.WriteLine($"{beer.Name} ({item.Brewery.Name})");
            }

        }
        
        static async void fetchDataAsync(int id) {
            System.Console.WriteLine($"Fetching Checkins for {id}...");
            int[] bids = new int[] { };

            string json = File.ReadAllText("secret.json");
            dynamic secrets = JsonConvert.DeserializeObject(json);

            string clientId = configuration["Untappd:ClientId"];
            string clientSecret = configuration["Untappd:ClientSecret"]; ;

            UntappdClient client = new UntappdClient(clientId, clientSecret);
            
            List<Checkin> c = await client.Beer(id).Checkins().GetAsync();

            foreach (Checkin checkin in c) {
                System.Console.WriteLine($"{checkin.Rating} {checkin.CreatedAt}");
            }


        }

    }
}
