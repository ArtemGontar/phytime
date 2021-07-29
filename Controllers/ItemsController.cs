using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Phytime.Models;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Xml;
using Phytime.Services;
using Microsoft.AspNetCore.Http;

//***********************************Testing angular here**************

namespace Phytime.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ItemsController : Controller
    {
        private readonly PhytimeContext _context;
        public static List<Product> list /*= new List<Product>()*/;
        public ItemsController(PhytimeContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return list;
        }

        [HttpGet("{id:int}")]
        public IActionResult ShowAngular(int id)
        {
            list = GetItems(id);
            return Redirect("/angular");
        }

        private List<Product> GetItems(int id)
        {
            var feed = _context.Feeds.Find(id);
            var list = GetSyndicationItems(feed.Url);
            return CreateItemsList(list);
        }

        private List<SyndicationItem> GetSyndicationItems(string url)
        {
            XmlReader reader = XmlReader.Create(url);
            SyndicationFeed feed = SyndicationFeed.Load(reader);
            reader.Close();
            return feed.Items.ToList();
        }

        private List<Product> CreateItemsList(List<SyndicationItem> list)
        {
            var itemsList = new List<Product>();
            foreach(var item in list)
            {
                itemsList.Add(new Product { Id = 1, Name = item.Title.Text, Company = item.Summary.Text, Price = 1 });
            }
            return itemsList;
        }
    }
}

namespace Phytime.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public decimal Price { get; set; }
    }

    //public class Item
    //{
    //    public string Title { get; set; }
    //    public string PublishDate { get; set; }
    //    public string Summary { get; set; }
    //}
}
