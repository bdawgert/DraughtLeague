namespace DraughtLeague.Untappd.Utilities
{
    public class BeerTools
    {

        public static string StyleFamily(string style) {
            if (style.Contains("-"))
                return style.Split("-")[0].Trim();

            return style;
        }



    }
}
