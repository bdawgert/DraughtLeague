using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using DraughtLeague.Untappd.Models;
using DraughtLeague.Untappd.Models.Beer.Checkins;
using Newtonsoft.Json;

namespace DraughtLeague.Untappd
{
    public sealed class BeerService : IClientService {

        private static readonly BeerService _instance = new BeerService();

        private BeerService() {
        }

        public static BeerService Instance(int id) {
            _instance.Id = id;
            return _instance;
        }

        public int Id { get; set; }
        public UntappdClient Client { get; set; }

    }
    
}
