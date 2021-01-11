using Newtonsoft.Json;

namespace DraughtLeague.Untappd.Models
{
    public class Meta
    {
        public int Code { get; set; }
        [JsonProperty("init_time")]
        public ActionTime InitTime { get; set; }
        [JsonProperty("response_time")]
        public ActionTime ResponseTime { get; set; }

        [JsonProperty("error_type")]
        public string ErrorType { get; set; }
        [JsonProperty("error_detail")]
        public string ErrorDetail { get; set; }
        [JsonProperty("developer_friendly")]
        public string DeveloperFriendly { get; set; }
    }

    public class ActionTime {
        public decimal Time { get; set; }
        public string Measure { get; set; }
    }


}
