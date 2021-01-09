using Newtonsoft.Json;

namespace DraughtLeague.Untappd.Models
{
    public class Pagination
    {
        [JsonProperty("max_id")]
        public int MaxId { get; set; }
        [JsonProperty("next_url")]
        public string NextUrl { get; set; }
        [JsonProperty("since_url")]
        public string SinceUrl { get; set; }
    }
}
