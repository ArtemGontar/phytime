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
<<<<<<< HEAD
=======
            ViewBag.Login = HttpContext.User.Identity.Name;
>>>>>>> 03436020527dea8a234f10a1cff9c93daa27112e
            return View(_rssSource);
        }

        public RedirectResult Logout()
        {
            return Redirect("/Account/Logout");
        }
    }
}