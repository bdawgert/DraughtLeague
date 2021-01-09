using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using DraughtLeague.Untappd.Models.Beer.Checkins;
using Newtonsoft.Json;

namespace DraughtLeague.Untappd.Models.Beer
{
    public static class BeerServiceCheckinExtensions
    {

        public static CheckinsEndpoint Checkins(this BeerService beerService)
        {
            return new CheckinsEndpoint {
                Service = beerService,
                MaxResults = 25
            };
        }

        public static CheckinsEndpoint MinId(this CheckinsEndpoint endpoint, int id) {
            endpoint.MinId = id;
            return endpoint;
        }

        public static CheckinsEndpoint MaxId(this CheckinsEndpoint endpoint, int id) {
            endpoint.MaxId = id;
            return endpoint;
        }

        public static CheckinsEndpoint MaxResults(this CheckinsEndpoint endpoint, int count) {
            endpoint.MaxId = count;
            return endpoint;
        }

        public static CheckinsEndpoint MinDate(this CheckinsEndpoint endpoint, DateTime date) {
            endpoint.MinDate = date;
            return endpoint;
        }

        public static async Task<List<Checkin>> GetAsync(this CheckinsEndpoint endpoint) {

            string url = endpoint.GenerateUrl();

            List<Checkin> checkins = new List<Checkin>();
            DateTime minDate;

            do {
                HttpResponseMessage responseMesage = endpoint.Service.Client.GetAsync(url).GetAwaiter().GetResult();
                //if (!responseMesage.IsSuccessStatusCode)

                string json = await responseMesage.Content.ReadAsStringAsync();
                CheckinsResponseWrapper checkinsResponseWrapper = JsonConvert.DeserializeObject<CheckinsResponseWrapper>(json);

                checkins.AddRange(checkinsResponseWrapper.Response.Checkins.Items);
                minDate = checkinsResponseWrapper.Response.Checkins.Items.Min(x => x.CreatedAt);
                url = checkinsResponseWrapper.Response.Pagination.NextUrl;

            } while (url != null && checkins.Count < endpoint.MaxResults && minDate > endpoint.MinDate);

            return checkins;

        }



    }
}
