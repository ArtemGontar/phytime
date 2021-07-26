using Microsoft.AspNetCore.Mvc;
using Phytime.Models;
using Microsoft.AspNetCore.Authorization;

namespace Phytime.Controllers
{
    public class HomeController : Controller
    {
        private readonly RssSource _rssSource;

        public HomeController()
        {
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