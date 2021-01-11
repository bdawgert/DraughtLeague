using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using DraughtLeague.Untappd.Models;
using DraughtLeague.Untappd.Models.Beer.Checkins;
using DraughtLeague.Untappd.Services;
using Newtonsoft.Json;

namespace DraughtLeague.Untappd.Endpoints.Beer
{
    public static class BeerCheckinsExtensions
    {

        public static BeerCheckinsEndpoint Checkins(this BeerService beerService)
        {
            return new BeerCheckinsEndpoint {
                Service = beerService,
                MaxResults = 25
            };
        }

        public static BeerCheckinsEndpoint MinId(this BeerCheckinsEndpoint endpoint, int id) {
            endpoint.MinId = id;
            return endpoint;
        }

        public static BeerCheckinsEndpoint MaxId(this BeerCheckinsEndpoint endpoint, int id) {
            endpoint.MaxId = id;
            return endpoint;
        }

        public static BeerCheckinsEndpoint MaxResults(this BeerCheckinsEndpoint endpoint, int count) {
            endpoint.MaxId = count;
            return endpoint;
        }

        public static BeerCheckinsEndpoint MinDate(this BeerCheckinsEndpoint endpoint, DateTime date) {
            endpoint.MinDate = date;
            return endpoint;
        }

        public static async Task<List<Checkin>> GetAsync(this BeerCheckinsEndpoint endpoint) {

            string url = endpoint.GenerateUrl();

            List<Checkin> checkins = new List<Checkin>();
            DateTime minDate;

            do {
                HttpResponseMessage responseMesage = endpoint.Service.Client.GetAsync(url).GetAwaiter().GetResult();
                //if (!responseMesage.IsSuccessStatusCode)

                string json = await responseMesage.Content.ReadAsStringAsync();
                ResponseWrapper<CheckinsResponse> checkinsResponseWrapper = JsonConvert.DeserializeObject<ResponseWrapper<CheckinsResponse>>(json);

                checkins.AddRange(checkinsResponseWrapper.Response.Checkins.Items);
                minDate = checkinsResponseWrapper.Response.Checkins.Items.Min(x => x.CreatedAt);
                url = checkinsResponseWrapper.Response.Pagination.SinceUrl;

            } while (url != null && checkins.Count < endpoint.MaxResults && minDate > endpoint.MinDate);

            return checkins;

        }



    }
}
