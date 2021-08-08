using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Phytime.Models;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Xml;
using Phytime.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;

namespace Phytime.Controllers
{
    public class FeedController : Controller
    {
        private readonly IConfiguration _config;
        private readonly IRepository _repository;

        public FeedController(IConfiguration config, IRepository repository = null)
        {
            _config = config;
            _repository = repository ?? new PhytimeRepository(config);
        }

        public ActionResult RssFeed(string url, int page)
        {
            ViewBag.Subscribed = IsSubscribed(url);
            var feedItems = GetSyndicationItems(url);
            if(Request.HasFormContentType)
            {
                var selectedSort = Request.Form["SortValue"].ToString();
                var sorterer = new FeedSorterer();
                sorterer.SortFeed(selectedSort, ref feedItems);
            }
            var rssFeed = FormFeed(url, page, feedItems);
            return View(rssFeed);
        }

        public Feed FormFeed(string url, int page, List<SyndicationItem> items)
        {
            int pageSize = int.Parse(_config.GetSection("FeedPageInfo:pageSize").Value);
            IEnumerable<SyndicationItem> itemsPerPages = items.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = items.Count };
            Feed rssFeed = _repository.GetFeedByUrl(url);
            rssFeed.PageInfo = pageInfo;
            rssFeed.SyndicationItems = itemsPerPages;
            return rssFeed;
        }

        public List<SyndicationItem> GetSyndicationItems(string url)
        {
            XmlReader reader = XmlReader.Create(url);
            SyndicationFeed feed = SyndicationFeed.Load(reader);
            reader.Close();
            return feed.Items.ToList();
        }

        public bool IsSubscribed(string url)
        {
            var feed = _repository.GetFeedByUrl(url);
            var user = _repository.GetUserByEmail(HttpContext.User.Identity.Name);
            return _repository.FeedContainsUser(feed, user);
        }

        public IActionResult Subscribe(string url)
        {
            var feed = _repository.GetFeedByUrl(url);
            var user = _repository.GetUserByEmail(HttpContext.User.Identity.Name);
            _repository.AddUserToFeed(feed, user);
            return RedirectToAction("RssFeed", new { url = url, page = 1});
        }

        public IActionResult UnSubscribe(string url)
        {
            var feed = _repository.GetFeedByUrl(url);
            var user = _repository.GetUserByEmail(HttpContext.User.Identity.Name);
            _repository.RemoveUserFromFeed(feed, user);
            return RedirectToAction("RssFeed", new { url = url, page = 1 });
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
            (_repository as PhytimeRepository).Dispose();
            base.Dispose(disposing);
        }
    }
}
