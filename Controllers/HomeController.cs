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

namespace Phytime.Controllers
{
    //[ApiController]
    //[Route("api/feeds")]
    public class HomeController : Controller
    {
        private static List<SyndicationItem> _feedItems;

        public IActionResult Index()
        {
            var list = GetUrls();
            return View(list);
        }

        [HttpGet]
        public IEnumerable<Feed> Get()
        {
            var list = new List<Feed>();
            list.Add(new Feed() { Title = "title1", PublishDate = "date1", Summary = "summary1" });
            list.Add(new Feed() { Title = "title2", PublishDate = "date2", Summary = "summary2" });
            return list;
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

        //[HttpPost]
        public ActionResult RssFeed(string url, int page)
        {
            if(_feedItems == null)
            {
                XmlReader reader = XmlReader.Create(url);
                SyndicationFeed feed = SyndicationFeed.Load(reader);
                reader.Close();
                _feedItems = feed.Items.ToList();
            }
            int pageSize = 5; // количество объектов на страницу
            IEnumerable<SyndicationItem> itemsPerPages = _feedItems.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = _feedItems.Count };
            RssFeedViewModel rssvm = new RssFeedViewModel { PageInfo = pageInfo, SyndicationItems = itemsPerPages };
            return View(rssvm);
        }

        public RedirectResult ChangeOrder()
        {
            _feedItems.Reverse();
            return Redirect("/Home/RssFeed");
        }
    }
}