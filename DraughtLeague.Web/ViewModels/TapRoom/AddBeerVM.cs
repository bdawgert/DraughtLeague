using System;
using DraughtLeague.Untappd.Utilities;

namespace DraughtLeague.Web.ViewModels.Bar
{
    public class AddBeerVM
    {
        public Guid TapRoomId { get; set; }
        public string BeerName { get; set; }
        public string BreweryName { get; set; }
        public decimal ABV { get; set; }
        public string StyleName { get; set; }
        public string StyleFamily => BeerTools.StyleFamily(StyleName);
        public int UntappdId { get; set; }


    }
}
