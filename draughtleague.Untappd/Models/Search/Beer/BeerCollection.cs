using System.Collections.Generic;

namespace DraughtLeague.Untappd.Models.Search.Beer
{
    public class BeerCollection
    {
        public int Count { get; set; }
        public List<BeerItem> Items { get; set; }
    }
}
