using System;
using Newtonsoft.Json;

namespace DraughtLeague.Untappd.Models.Beer.Checkins
{
    public class Checkin
    {
        [JsonProperty("checkin_id")]
        public int Id { get; set; }
        [JsonProperty("created_at")]
        public string CreatedAtValue { get; set; }
        public DateTime CreatedAt => DateTime.Parse(CreatedAtValue);
        [JsonProperty("rating_score")]
        public decimal Rating { get; set; }

    }
}
