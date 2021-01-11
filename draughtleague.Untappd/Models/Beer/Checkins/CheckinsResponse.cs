using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace DraughtLeague.Untappd.Models.Beer.Checkins
{
    public class CheckinsResponse {
        public CheckinCollection Checkins { get; set; }
        public Pagination Pagination { get; set; }
    }
    
}
