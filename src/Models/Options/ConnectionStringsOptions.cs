namespace Phytime.Models.Options
{
    public class ConnectionStringsOptions
    {
        public const string ConnectionStrings = "ConnectionStrings";
        public string DefaultConnection { get; set; }
        public string TestsConnection { get; set; }
        public string RssConnection { get; set; }
    }
}
