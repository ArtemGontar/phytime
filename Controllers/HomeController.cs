using Microsoft.AspNetCore.Mvc;
using Phytime.Models;
using Microsoft.AspNetCore.Authorization;

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