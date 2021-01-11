using System;
using System.Collections.Generic;
using DraughtLeague.Untappd.Services;

namespace DraughtLeague.Untappd.Endpoints.Beer
{
    public class SearchBeerEndpoint {

        internal BeerService Service { get; set; }
        
        //Untappd API Parameters
        internal bool Compact { get; set; }

        internal string GenerateUrl() {
            string baseUrl = $"https://api.untappd.com/v4/beer/info/{Service.Id}?";

            List<string> parameters = new List<string> { $"client_id={Service.Client.ClientId}", $"client_secret={Service.Client.ClientSecret}" };
            if (Compact)
                parameters.Add($"compact=true");
            
            return baseUrl + string.Join("&", parameters);
        }

    }
}
