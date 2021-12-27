using System.Collections.Generic;

namespace Phytime.Models.Feed
{
    public class Feed
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public int ItemsCount { get; set; }
        public List<User> Users { get; set; } = new List<User>();
    }
}
