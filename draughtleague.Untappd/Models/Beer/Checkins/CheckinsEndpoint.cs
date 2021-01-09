
using System;
using System.Collections.Generic;

namespace DraughtLeague.Untappd.Models.Beer.Checkins
{
    public class CheckinsEndpoint {

        public BeerService Service { get; set; }
        
        //Untappd API Parameters
        public int MinId { get; set; }
        public int MaxId { get; set; }

        //Endpoint Service Properties
        public DateTime? MinDate { get; set; }
        public int MaxResults { get; set; }

        public string GenerateUrl() {
            string baseUrl = $"https://api.untappd.com/v4/beer/checkins/{Service.Id}?";

            List<string> parameters = new List<string> { $"client_id={Service.Client.ClientId}", $"client_secret={Service.Client.ClientSecret}" };
            if (MinId != 0)
                parameters.Add($"min_id={MinId}");
            if (MaxId != 0)
                parameters.Add($"max_id={MaxId}");
            
            return baseUrl + string.Join("&", parameters);
        }

    }
}
