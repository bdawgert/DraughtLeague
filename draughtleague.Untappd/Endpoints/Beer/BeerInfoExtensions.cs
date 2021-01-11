using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using DraughtLeague.Untappd.Models;
using DraughtLeague.Untappd.Models.Beer.Info;
using DraughtLeague.Untappd.Services;
using Newtonsoft.Json;

namespace DraughtLeague.Untappd.Endpoints.Beer
{
    public static class BeerInfoExtensions
    {

        public static SearchBeerEndpoint Info(this BeerService beerService)
        {
            return new SearchBeerEndpoint {
                Service = beerService,
            };
        }

        public static SearchBeerEndpoint Compact(this SearchBeerEndpoint endpoint, bool compact = true) {
            endpoint.Compact = compact;
            return endpoint;
        }

        public static async Task<Info> GetAsync(this SearchBeerEndpoint endpoint) {

            string url = endpoint.GenerateUrl();

            Info info = new Info();

            HttpResponseMessage responseMesage = endpoint.Service.Client.GetAsync(url).GetAwaiter().GetResult();
            //if (!responseMesage.IsSuccessStatusCode)

            string json = await responseMesage.Content.ReadAsStringAsync();
            ResponseWrapper<InfoResponse> checkinsResponseWrapper = JsonConvert.DeserializeObject<ResponseWrapper<InfoResponse>>(json);
          
            return info;

        }



    }
}
