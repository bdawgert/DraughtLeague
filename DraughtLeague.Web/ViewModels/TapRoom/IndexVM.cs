using System;
using System.Collections.Generic;


namespace DraughtLeague.Web.ViewModels.TapRoom
{
    public class IndexVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<object> BeerList { get; set; }

    }
}
