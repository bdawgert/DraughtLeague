using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using DraughtLeague.Untappd.Models;
using DraughtLeague.Untappd.Models.Search.Beer;
using DraughtLeague.Untappd.Services;
using Newtonsoft.Json;

namespace DraughtLeague.Untappd.Endpoints.Search
{
    public static class SearchBeerExtensions
    {

        public static SearchBeerEndpoint Beer(this SearchService beerService, string query)
        {
            return new SearchBeerEndpoint {
                Service = beerService,
                Query = query,
                Limit = 25
            };
        }

        public static SearchBeerEndpoint Offset(this SearchBeerEndpoint endpoint, int offset) {
            endpoint.Offset = offset;
            return endpoint;
        }


        public static SearchBeerEndpoint Limit(this SearchBeerEndpoint endpoint, int count) {
            endpoint.Limit = count;
            return endpoint;
        }

        public static SearchBeerEndpoint Sort(this SearchBeerEndpoint endpoint, SortOrder sort) {
            endpoint.Sort = sort;
            return endpoint;
        }

        public static async Task<List<BeerItem>> GetAsync(this SearchBeerEndpoint endpoint) {

            string url = endpoint.GenerateUrl();

            List<BeerItem> results = new List<BeerItem>();

            HttpResponseMessage responseMesage = await endpoint.Service.Client.GetAsync(url);
            //if (!responseMesage.IsSuccessStatusCode)

            string json = await responseMesage.Content.ReadAsStringAsync();
            ResponseWrapper<SearchBeerResponse> checkinsResponseWrapper = JsonConvert.DeserializeObject<ResponseWrapper<SearchBeerResponse>>(json);

            results.AddRange(checkinsResponseWrapper.Response.Beers.Items);
            
            return results;

        }



    }
}
