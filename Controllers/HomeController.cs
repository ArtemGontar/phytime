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
        private RssSource _rssSource;

        public HomeController(PhytimeContext context)
        {
            _context = context;
            _rssSource = RssSource.getInstance();
        }

        [Authorize]
        public IActionResult Index()
        {
            ViewBag.Login = HttpContext.User.Identity.Name;
            return View(_rssSource);
        }

        public RedirectResult Logout()
        {
            return Redirect("/Account/Logout");
        }
    }
}