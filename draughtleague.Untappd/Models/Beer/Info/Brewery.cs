using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace DraughtLeague.Untappd.Models.Beer.Info
{
    public class Brewery {
        [JsonProperty("bewery_id")]
        public string Id { get; set; }
        [JsonProperty("brewery_name")]
        public string Name { get; set; }
    }
}
