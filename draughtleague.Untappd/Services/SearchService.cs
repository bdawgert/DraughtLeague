namespace DraughtLeague.Untappd.Services
{
    public sealed class SearchService
    {
        private static readonly SearchService _instance = new SearchService();

        private SearchService() { }

        public static SearchService Instance() {
            return _instance;
        }

        internal UntappdClient Client { get; set; }

    }
}
