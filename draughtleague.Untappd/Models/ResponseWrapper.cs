namespace DraughtLeague.Untappd.Models
{
    public class ResponseWrapper<T>
    {
        public Meta Meta { get; set; }
        public object Notifications { get; set; }
        public T Response { get; set; }
    }
}
