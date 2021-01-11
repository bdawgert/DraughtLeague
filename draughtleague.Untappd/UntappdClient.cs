using System.Net.Http;
using DraughtLeague.Untappd.Services;

namespace DraughtLeague.Untappd
{
    public class UntappdClient : HttpClient
    {
        internal string ClientId { get; private set; }
        internal string ClientSecret { get; private set; }
        
        public UntappdClient(string clientId, string clientSecret) {
            ClientId = clientId;
            ClientSecret = clientSecret;
        }
        
    }

    public static class UntappdClientExtensions {

        public static BeerService Beer(this UntappdClient client, int id) {
            BeerService service = BeerService.Instance(id);
            service.Client = client;
            return service;
        }

        public static SearchService Search(this UntappdClient client) {
            SearchService service = SearchService.Instance();
            service.Client = client;
            return service;
        }

    }
}