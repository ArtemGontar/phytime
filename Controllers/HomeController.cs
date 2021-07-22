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
        private static List<string> _feedUrlList;

        public HomeController(PhytimeContext context)
        {
            _context = context;
        }

        [Authorize]
        public IActionResult Index()
        {
            if(_feedUrlList == null)
            {
                _feedUrlList = GetUrls();
            }
            CheckUrls(_feedUrlList);
            ViewBag.UrlList = _feedUrlList;
            ViewBag.Login = HttpContext.User.Identity.Name;
            return View();
        }

        public List<string> GetUrls()
        {
            var urlList = new List<string>();
            string regularExpressionPattern1 = @"<th.*?>(.*?)<\/th>";
            Regex titleRegex = new Regex(regularExpressionPattern1, RegexOptions.Singleline);
            Regex linkRegex = new Regex(@"\bhttps://[^<]*");
            // Создать объект запроса
            WebRequest request = WebRequest.Create("https://psyjournals.ru/rss/");

            // Получить ответ с сервера
            WebResponse response = request.GetResponse();

            // Получаем поток данных из ответа
            using (StreamReader stream = new StreamReader(response.GetResponseStream()))
            {
                // Выводим исходный код страницы
                string line = "";
                while ((line = stream.ReadLine()) != null)
                {
                    if (line.Contains("</th>"))
                    {
                        MatchCollection collection = titleRegex.Matches(line);
                        Match m = collection[0];
                        urlList.Add(m.Groups[1].Value);
                        continue;
                    }
                    if (line.Contains(".rss</a>"))
                    {
                        Match match = linkRegex.Match(line);
                        urlList.Add(match.Value);
                    }
                }
            }
            return urlList;
        }

        public RedirectResult Logout()
        {
            return Redirect("/Account/Logout");
        }

        public void CheckUrls(List<string> urlList)
        {
            for(int i = 1; i < urlList.Count; i+=2)
            {
                XmlReader reader = XmlReader.Create(urlList[i]);
                SyndicationFeed feed = SyndicationFeed.Load(reader);
                reader.Close();
                var rssFeed = _context.Feeds.FirstOrDefault(f => f.Url == urlList[i]);
                if (rssFeed != null)
                {
                    if(/*true*/rssFeed.ItemsCount != feed.Items.ToList().Count)
                    {
                        SendNotifications(urlList[i], urlList[i - 1]);
                    }
                }
                else
                {
                    _context.Feeds.Add(new Feed { Url = urlList[i], ItemsCount = feed.Items.ToList().Count });
                    _context.SaveChanges();
                }
            }
        }

        public void SendNotifications(string feedUrl, string feedTitle)
        {
            EmailService service = new EmailService();
            var rssfeed = _context.Feeds.Include(f => f.Users).FirstOrDefault(f => f.Url == feedUrl);
            foreach (var user in rssfeed.Users)
            {
                service.SendEmail(user.Email, "Обновление записей", feedTitle);
            }
        }
    }
}