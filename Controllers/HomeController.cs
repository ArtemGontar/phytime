using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Xml;
using System.ServiceModel;
using System.ServiceModel.Syndication;

namespace Phytime.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var list = GetUrls();
            return View(list);
        }

        public List<string> GetUrls()
        {
            var urlList = new List<string>();
            Regex regex = new Regex(@"\bhttps://[^<]*");
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
                    if(line.Contains(".rss</a>"))
                    {
                        Match match = regex.Match(line);
                        urlList.Add(match.Value);
                    }
            }
            return urlList;
        }

        [HttpPost]
        public IActionResult RssFeed(string url)
        {
            XmlReader reader = XmlReader.Create(url);
            SyndicationFeed feed = SyndicationFeed.Load(reader);
            reader.Close();
            return View(feed.Items);
        }
    }
}