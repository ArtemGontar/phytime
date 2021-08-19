using Microsoft.AspNetCore.Mvc;
using Phytime.Models;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Xml;
using Phytime.Services;
using System;
using Phytime.ViewModels;

namespace Phytime.Controllers
{
    public class FeedController : Controller
    {
        public const int DefaultPage = 1;
        public const string DefaultSortValue = "Newest";

        private readonly IRepository<Feed> _feedRepository;
        private readonly IRepository<User> _userRepository;

        public FeedController(PhytimeContext context, IRepository<Feed> feedRepository = null, IRepository<User> userRepository = null)
        {
            _feedRepository = feedRepository ?? new FeedRepository(context);
            _userRepository = userRepository ?? new UserRepository(context);
        }

        public IActionResult RssFeed(string url, int page = DefaultPage, string sortValue = DefaultSortValue)
        {
            var viewModel = CreateViewModel(url, sortValue, page);
            return View(viewModel);
        }

        public IActionResult Sort(FeedViewModel model)
        {
            return RedirectToAction("RssFeed", new { url = model.FeedValue.Url, 
                page = DefaultPage, model.SortValue});
        }

        public FeedViewModel CreateViewModel(string url, string sortValue, int page)
        {
            ViewBag.Subscribed = IsSubscribed(url);
            var feedItems = GetSyndicationItems(url);
            var sorterer = new FeedSorterer();
            sorterer.SortFeed(sortValue, ref feedItems);
            var rssFeedViewModel = FormFeed(url, page, feedItems);
            rssFeedViewModel.SortValue = sortValue;
            return rssFeedViewModel;
        }

        public FeedViewModel FormFeed(string url, int page, List<SyndicationItem> items)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException(nameof(url));
            }
            if (page < DefaultPage)
            {
                throw new ArgumentException(nameof(page));
            }
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }
            PageInfo pageInfo = new PageInfo { PageNumber = page, TotalItems = items.Count };
            var itemsPerPages =  items.Skip((page - 1) * pageInfo.PageSize).Take(pageInfo.PageSize);
            Feed rssFeed = _feedRepository.GetBy(url);
            var model = new FeedViewModel() { FeedValue = rssFeed, PageInfo = pageInfo, SyndicationItems = itemsPerPages.ToList() };
            return model;
        }

        public List<SyndicationItem> GetSyndicationItems(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException(nameof(url));
            }
            XmlReader reader = XmlReader.Create(url);
            SyndicationFeed feed = SyndicationFeed.Load(reader);
            reader.Close();
            return feed.Items.ToList();
        }

        public bool IsSubscribed(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException(nameof(url));
            }
            var feed = _feedRepository.GetBy(url);
            feed = _feedRepository.GetInclude(feed);
            foreach(var user in feed.Users)
            {
                if(user.Email.Equals(HttpContext.User.Identity.Name))
                {
                    return true;
                }
            }
            return false;
        }

        public IActionResult Subscribe(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException(nameof(url));
            }
            var user = _userRepository.GetBy(HttpContext.User.Identity.Name);
            _feedRepository.GetBy(url).Users.Add(user);
            _feedRepository.Save();
            return RedirectToAction("RssFeed", new { url = url, page = DefaultPage });
        }

        public IActionResult Unsubscribe(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException(nameof(url));
            }
            var feed = _feedRepository.GetBy(url);
            feed = _feedRepository.GetInclude(feed);
            var user = _userRepository.GetBy(HttpContext.User.Identity.Name);
            feed.Users.Remove(user);
            _feedRepository.Save();
            return RedirectToAction("RssFeed", new { url = url, page = DefaultPage });
        }

        public RedirectResult Logout()
        {
            return Redirect("/Account/Logout");
        }

        public IActionResult ShowAngular(int id)
        {
            return Redirect($"/angular/{id}");
        }

        protected override void Dispose(bool disposing)
        {
            _feedRepository.Dispose();
            base.Dispose(disposing);
        }
    }
}
