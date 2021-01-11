using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Web;
using DraughtLeague.Untappd.Models.Search.Beer;
using DraughtLeague.Untappd.Services;

namespace DraughtLeague.Untappd.Endpoints.Search
{
    public class SearchBeerEndpoint {

        public SearchService Service { get; set; }
        
        //Untappd API Parameters
        public string Query { get; set; }
        public int Offset { get; set; }
        public int Limit { get; set; }
        public SortOrder? Sort { get; set; }

        public string GenerateUrl() {
            string baseUrl = $"https://api.untappd.com/v4/search/beer?";

            List<string> parameters = new List<string> {
                $"client_id={Service.Client.ClientId}", 
                $"client_secret={Service.Client.ClientSecret}",
                $"q={HttpUtility.UrlEncode(Query)}"
            };
            if (Offset > 0)
                parameters.Add($"offset={Offset}");
            if (Limit > 0)
                parameters.Add($"limit={Limit}");
            if (Sort == SortOrder.Name)
                parameters.Add("sort=name");
            else if (Sort == SortOrder.CheckinCount)
                parameters.Add("sort=checkin");

            return baseUrl + string.Join("&", parameters);
        }

    }
}
