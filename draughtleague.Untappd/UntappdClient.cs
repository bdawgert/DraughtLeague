using System.Net.Http;

namespace DraughtLeague.Untappd
{
    public class UntappdClient : HttpClient
    {

        private static UntappdClient _untappdClient;
        internal string ClientId { get; private set; }
        internal string ClientSecret { get; private set; }

        private string _url;

        public UntappdClient(string clientId, string clientSecret) : base() {
            ClientId = clientId;
            ClientSecret = clientSecret;

            
            _url = $"https://api.untappd.com/v4/method_name?client_id={ClientId}&client_secret={ClientSecret}";

        }
        
        //public static UntappdClient Create() {
        //    return _untappdClient;
        //}

        //public static UntappdClient Create(string clientId, string clientSecret) {
        //    if (_untappdClient != null)
        //        return _untappdClient;

        //    return _untappdClient = new UntappdClient(clientId, clientSecret);

        //}

        //public BeerSearchResult Search(string q) {

        //    UntappdResponse<BeerSearchResult> searchResult = null;

        //    string searchUrl =
        //        $"https://api.untappd.com/v4/search/beer?client_id={ClientId}&client_secret={ClientSecret}&q={q}";

        //    HttpResponseMessage httpResponse =  this.GetAsync(searchUrl).Result;
        //    string json = httpResponse.Content.ReadAsStringAsync().Result;
        //    searchResult = JsonConvert.DeserializeObject<UntappdResponse<BeerSearchResult>>(json);

        //    return searchResult.Response;
        //}

        //public Beer Lookup(int beerId) {

        //    UntappdResponse<BeerLookupResult> beerResult = null;

        //    string lookupUrl =
        //        $"https://api.untappd.com/v4/beer/info/{beerId}?client_id={ClientId}&client_secret={ClientSecret}&compact=true";

        //    //_httpClient.GetAsync(lookupUrl)
        //    //    .ContinueWith(t => {
        //    //        t.Result.Content.ReadAsStringAsync()
        //    //            .ContinueWith(r => {
        //    //                beer = JsonConvert.DeserializeObject<UntappdResponse<Beer>>(r.Result);
        //    //            });
        //    //    });

        //    HttpResponseMessage httpResponse = _httpClient.GetAsync(lookupUrl).Result;
        //    string json = httpResponse.Content.ReadAsStringAsync().Result;
        //    beerResult = JsonConvert.DeserializeObject<UntappdResponse<BeerLookupResult>>(json);

        //    return beerResult.Response.Beer;

        //}

        

        //public Brewery Brewery { get; set; }
        
    }


    public static class UntappdClientExtensions {

        public static BeerService Beer(this UntappdClient client, int id) {
            BeerService service = BeerService.Instance(id);
            service.Client = client;
            return service;
        }

    }
}