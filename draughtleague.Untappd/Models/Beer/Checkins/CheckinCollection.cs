using System.Collections.Generic;
using System.Net;

namespace DraughtLeague.Untappd.Models.Beer.Checkins
{
    public class CheckinCollection  {
        public int Count { get; set; }
        public List<Checkin> Items { get; set; }
    }
}
