using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace DraughtLeague.Untappd.Models.Beer.Checkins
{
    public class CheckinsResponseWrapper
    {
        public object Meta { get; set; }
        public object Notifications { get; set; }
        public CheckinsResponse Response { get; set; }
    }

    public class CheckinsResponse {
        public CheckinCollection Checkins { get; set; }
        public Pagination Pagination { get; set; }
    }
    
}
