
namespace DraughtLeague.Untappd.Services
{
    public sealed class BeerService {

        private static readonly BeerService _instance = new BeerService();

        private BeerService() { }

        public static BeerService Instance(int id) {
            _instance.Id = id;
            return _instance;
        }

        internal int Id { get; set; }
        internal UntappdClient Client { get; set; }

    }
    
}
