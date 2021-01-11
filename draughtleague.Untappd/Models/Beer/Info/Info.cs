using Newtonsoft.Json;

namespace DraughtLeague.Untappd.Models.Beer.Info
{
    public class Info
    {
        [JsonProperty("bid")]
        public string Id { get; set; }
        [JsonProperty("beer_name")]
        public string Name { get; set; }
        [JsonProperty("beer_abv")]
        public double ABV { get; set; }
        [JsonProperty("beer_description")]
        public string Description { get; set; }
        [JsonProperty("beer_style")]
        public string Style { get; set; }
        [JsonProperty("rating_score")]
        public float Rating { get; set; }
    }
}
