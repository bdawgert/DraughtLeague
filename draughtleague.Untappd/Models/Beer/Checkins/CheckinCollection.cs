using System.Collections.Generic;

namespace DraughtLeague.Untappd.Models.Beer.Checkins
{
    public class CheckinCollection  {
        public int Count { get; set; }
        public List<Checkin> Items { get; set; }
    }
}
