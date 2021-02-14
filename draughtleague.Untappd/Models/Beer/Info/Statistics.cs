using Newtonsoft.Json;

namespace DraughtLeague.Untappd.Models.Beer.Info
{
    public class Statistics
    {
        [JsonProperty("total_count")]
        public int TotalCount { get; set; }
        [JsonProperty("monthly_count")]
        public int MonthlyCount { get; set; }
        [JsonProperty("total_user_count")]
        public int UniqueCount { get; set; }
    }
}
