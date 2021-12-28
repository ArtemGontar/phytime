using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Xml;
using Phytime.Models;
using Phytime.Models.Feed;
using Phytime.Repository;
using Phytime.ViewModels;

namespace Phytime.Services
{
    public interface IRssService
    {
        IEnumerable<Source> GetSources();

        Task<FeedViewModel> GetSourceAsync(int id, string sortValue, int page);
    }
    
    public class RssService : IRssService
    {
        public const int DefaultPage = 1;
        public const string DefaultSortValue = "Newest";
        
        private readonly RssSource _rssSource;
        private readonly IFeedRepository _feedRepository;

        public RssService(IFeedRepository feedRepository)
        {
            _feedRepository = feedRepository ?? throw new ArgumentNullException(nameof(feedRepository));
            _rssSource = RssSource.getInstance();
        }

        public IEnumerable<Source> GetSources()
        {
            return _rssSource.Sources;
        }

        public async Task<FeedViewModel> GetSourceAsync(int id, string sortValue, int page)
        {
            if (id == default)
            {
                throw new ArgumentNullException(nameof(id));
            }
            
            var viewModel = await CreateViewModelAsync(id, sortValue, page);
            return viewModel;
        }
        
        private async Task<FeedViewModel> CreateViewModelAsync(int id, string sortValue, int page)
        {
            //ViewBag.Subscribed = IsSubscribed(url);
            return await PreparingFeedViewModelAsync(id, page, sortValue);
        }
        
        private async Task<FeedViewModel> PreparingFeedViewModelAsync(int id, int page, string sortValue)
        {
            if (page < DefaultPage)
            {
                throw new ArgumentException(nameof(page));
            }
            var sorterer = new FeedSorterer();
            var rssFeed = await _feedRepository.GetAsync(id);
            var feedItems = GetSyndicationItems(rssFeed.Url);
            sorterer.SortFeed(sortValue, ref feedItems);
            var pageInfo = new PageInfo { PageNumber = page, TotalItems = feedItems.Count };
            var itemsPerPages =  feedItems.Skip((page - 1) * pageInfo.PageSize).Take(pageInfo.PageSize);
            var model = new FeedViewModel() { PageInfo = pageInfo, SyndicationItems = itemsPerPages.ToList() };
            model.SortValue = sortValue;
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