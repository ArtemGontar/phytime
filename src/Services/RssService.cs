using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Xml;
using Phytime.Models;
using Phytime.Models.Feed;
using Phytime.Repository;
using Phytime.ViewModels;

namespace Phytime.Services
{
    public interface IRssService
    {
        public IEnumerable<Source> GetSources();

        public FeedViewModel GetSourceByUrl(string url, string sortValue, int page);
    }
    
    public class RssService : IRssService
    {
        public const int DefaultPage = 1;
        public const string DefaultSortValue = "Newest";
        
        private readonly RssSource _rssSource;
        private readonly IRepository<Feed> _feedRepository;

        public RssService(IRepository<Feed> feedRepository)
        {
            _feedRepository = feedRepository ?? throw new ArgumentNullException(nameof(feedRepository));
            _rssSource = RssSource.getInstance();
        }

        public IEnumerable<Source> GetSources()
        {
            return _rssSource.Sources;
        }

        public FeedViewModel GetSourceByUrl(string url, string sortValue, int page)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException(nameof(url));
            }
            
            var viewModel = CreateViewModel(url, sortValue, page);
            return viewModel;
        }
        
        private FeedViewModel CreateViewModel(string url, string sortValue, int page)
        {
            //ViewBag.Subscribed = IsSubscribed(url);
            var feedItems = GetSyndicationItems(url);
            var sorterer = new FeedSorterer();
            sorterer.SortFeed(sortValue, ref feedItems);
            var rssFeedViewModel = PreparingFeedViewModel(url, page, feedItems);
            rssFeedViewModel.SortValue = sortValue;
            return rssFeedViewModel;
        }
        
        private FeedViewModel PreparingFeedViewModel(string url, int page, List<SyndicationItem> items)
        {
            if (page < DefaultPage)
            {
                throw new ArgumentException(nameof(page));
            }
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }
            var pageInfo = new PageInfo { PageNumber = page, TotalItems = items.Count };
            var itemsPerPages =  items.Skip((page - 1) * pageInfo.PageSize).Take(pageInfo.PageSize);
            var rssFeed = _feedRepository.GetBy(url);
            var model = new FeedViewModel() { FeedValue = rssFeed, PageInfo = pageInfo, SyndicationItems = itemsPerPages.ToList() };
            return model;
        }
        private List<SyndicationItem> GetSyndicationItems(string url)
        {
            var reader = XmlReader.Create(url);
            var feed = SyndicationFeed.Load(reader);
            reader.Close();
            return feed.Items.ToList();
        }
        
        
    }
}