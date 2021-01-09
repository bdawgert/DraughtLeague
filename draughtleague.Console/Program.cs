using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using DraughtLeague.Untappd;
using DraughtLeague.Untappd.Models.Beer;
using DraughtLeague.Untappd.Models.Beer.Checkins;
using Newtonsoft.Json;

namespace DraughtLeague.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            //parseArgs()

            var method = System.Console.ReadLine();

            if (method == "fetch") {
                System.Console.WriteLine("Fetching Checkins...");
                fetchDataAsync();
            }
        }

        static async void fetchDataAsync() {

            int[] bids = new int[] { };

            string json = File.ReadAllText("secret.json");
            dynamic secrets = JsonConvert.DeserializeObject(json);

            UntappdClient client = new UntappdClient(secrets.clientId,
                secrets.clientSecret);

            List<Checkin> c = await client.Beer(3489).Checkins().GetAsync();

            foreach (Checkin checkin in c) {
                System.Console.WriteLine($"{checkin.Rating} {checkin.CreatedAt}");
            }


        }

    }
}
