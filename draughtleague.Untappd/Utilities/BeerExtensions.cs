using System;
using System.Collections.Generic;
using System.Text;
using DraughtLeague.Untappd.Models.Beer.Info;

namespace DraughtLeague.Untappd.Utilities
{
    public static class BeerExtensions
    {

        public static string StyleFamily(this Info beer)
        {
            return BeerTools.StyleFamily(beer.Style);
        }

    }
}
