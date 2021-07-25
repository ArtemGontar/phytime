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

namespace Phytime.Controllers
{
    public class FeedController : Controller
    {
        private readonly PhytimeContext _context;
        private const int PageSize = 5;

        public FeedController(PhytimeContext context)
        {
            _context = context;
        }

        public ActionResult RssFeed(string url, int page)
        {
            ViewBag.Login = HttpContext.User.Identity.Name;
            ViewBag.Subscribed = IsSubscribed(url);
            var feedItems = GetSyndicationItems(url);
            if(Request.HasFormContentType)
            {
                var selectedSort = Request.Form["SortValue"].ToString();
                var sorterer = new FeedSorterer();
                sorterer.SortFeed(selectedSort, ref feedItems);
            }
            IEnumerable<SyndicationItem> itemsPerPages = feedItems.Skip((page - 1) * PageSize).Take(PageSize);
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = PageSize, TotalItems = feedItems.Count };
            Feed rssFeed = new Feed { Url = url, PageInfo = pageInfo, SyndicationItems = itemsPerPages };
            return View(rssFeed);
        }

        private List<SyndicationItem> GetSyndicationItems(string url)
        {
            XmlReader reader = XmlReader.Create(url);
            SyndicationFeed feed = SyndicationFeed.Load(reader);
            reader.Close();
            return feed.Items.ToList();
        }

        private bool IsSubscribed(string url)
        {
            var contains = _context.Feeds.Include(c => c.Users).FirstOrDefault(feed => feed.Url == url)
                .Users.Select(u => u.Email).Contains(HttpContext.User.Identity.Name);
            return contains;
        }

        public async Task<IActionResult> Subscribe(string url)
        {
            User user = await _context.Users.FirstOrDefaultAsync(u => u.Email == HttpContext.User.Identity.Name);
            var feed = new Feed() { Url = url };
            Feed dbFeed = await _context.Feeds.FirstOrDefaultAsync(f => f.Url == url);
            dbFeed.Users.Add(user);
            await _context.SaveChangesAsync();
            return RedirectToAction("RssFeed", new { url = url, page = 1});
        }

        public async Task<IActionResult> UnSubscribe(string url)
        {
            User user = _context.Users.Include(user => user.Feeds)
                .FirstOrDefault(user => user.Email == HttpContext.User.Identity.Name);
            Feed dbFeed = await _context.Feeds.FirstOrDefaultAsync(feed => feed.Url == url);
            user.Feeds.Remove(dbFeed);
            _context.SaveChanges();
            return RedirectToAction("RssFeed", new { url = url, page = 1 });
        }

        public RedirectResult Logout()
        {
            return Redirect("/Account/Logout");
        }

    }
}
