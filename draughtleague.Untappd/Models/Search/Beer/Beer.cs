using Newtonsoft.Json;

namespace DraughtLeague.Untappd.Models.Search.Beer
{
    
    public class BeerItem
    {
        [JsonProperty("checkin_count")]
        public int CheckinCount { get; set; }
        [JsonProperty("have_had")]
        public bool? HaveHad { get; set; }
        [JsonProperty("your_count")]
        public int? YourCount { get; set; }
        public Beer Beer { get; set; }
        public Brewery Brewery { get; set; }
    }

    public class Beer
    {
        [JsonProperty("bid")]
        public string Id { get; set; }
        [JsonProperty("beer_name")]
        public string Name { get; set; }
        [JsonProperty("beer_abv")]
        public decimal ABV { get; set; }
        [JsonProperty("beer_description")]
        public string Description { get; set; }
        [JsonProperty("beer_style")]
        public string Style { get; set; }
        [JsonProperty("rating_score")]
        public decimal Rating { get; set; }
    }

    public class Brewery {
        [JsonProperty("brewery_id")]
        public int Id { get; set; }
        [JsonProperty("brewery_name")]
        public string Name { get; set; }
        [JsonProperty("country_name")]
        public string CountryName { get; set; }

    }

}
