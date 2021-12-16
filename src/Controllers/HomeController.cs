using Microsoft.AspNetCore.Mvc;

namespace Phytime.Controllers
{
    public class HomeController : Controller
    {
        public RedirectResult Logout()
        {
            return Redirect("/Account/Logout");
        }
    }
}