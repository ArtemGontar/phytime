using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Xml;
using System.ServiceModel;
using System.ServiceModel.Syndication;
using System.Linq;
using Phytime.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Phytime.Services;
using System.Threading.Tasks;

namespace Phytime.Controllers
{
    public class HomeController : Controller
    {
        private PhytimeContext _context;
        private static RssSource _rssSource;

        public HomeController(PhytimeContext context)
        {
            _context = context;
        }

        [Authorize]
        public IActionResult Index()
        {
            if(_rssSource == null)
            {
                _rssSource = new RssSource();
                _rssSource.Titles = RssFeedSourceReader.GetTitles();
                _rssSource.Urls = RssFeedSourceReader.GetUrls();
            }

            //this method checks if there are any new items in rss feeds and send email to subscribed users
            //planning to create special service with this method and special timer and start this service in Startup class
            //CheckUrls(_rssSource);

            ViewBag.Login = HttpContext.User.Identity.Name;
            return View(_rssSource);
        }

        public RedirectResult Logout()
        {
            return Redirect("/Account/Logout");
        }
    }
}