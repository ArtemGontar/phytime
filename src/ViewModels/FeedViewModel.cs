using Phytime.Models;
using System.Collections.Generic;
using System.ServiceModel.Syndication;
using Phytime.Models.Feed;

namespace Phytime.ViewModels
{
    public class FeedViewModel
    {
        public Feed FeedValue { get; set; }
        public string SortValue { get; set; }
        public List<SyndicationItem> SyndicationItems { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}
