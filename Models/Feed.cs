using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ServiceModel.Syndication;

namespace Phytime.Models
{
    public class Feed
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public int ItemsCount { get; set; }
        public List<User> Users { get; set; } = new List<User>();
        [NotMapped]
        public IEnumerable<SyndicationItem> SyndicationItems { get; set; }
        [NotMapped]
        public PageInfo PageInfo { get; set; }
    }
}
