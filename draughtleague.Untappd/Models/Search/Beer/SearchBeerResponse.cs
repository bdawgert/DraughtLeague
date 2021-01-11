using Newtonsoft.Json;

namespace DraughtLeague.Untappd.Models.Search.Beer
{
    public class SearchBeerResponse
    {
        public int Found { get; set; }
        public int Offset { get; set; }
        public int Limit { get; set; }
        public string Term { get; set; }
        [JsonProperty("parsed_term")]
        public string ParsedTerm { get; set; }

        public BeerCollection Beers { get; set; }
    }
}
