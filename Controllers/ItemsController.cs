using Microsoft.AspNetCore.Mvc;
<<<<<<< HEAD
=======
using Microsoft.EntityFrameworkCore;
>>>>>>> 03436020527dea8a234f10a1cff9c93daa27112e
using Phytime.Models;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
<<<<<<< HEAD
using System.Xml;
=======
using System.Threading.Tasks;
using System.Xml;
using Phytime.Services;

//***********************************Testing angular here**************
>>>>>>> 03436020527dea8a234f10a1cff9c93daa27112e

namespace Phytime.Controllers
{
    [ApiController]
<<<<<<< HEAD
    [Route("api/[controller]/[action]")]
    public class ItemsController : Controller
    {
        private readonly PhytimeContext _context;
        private const string SessionListItemsKey = "items";
        public ItemsController(PhytimeContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Item> Get()
        {
            return HttpContext.Session.GetComplexData<List<Item>>(SessionListItemsKey);
        }

        [HttpGet("{id:int}")]
        public IActionResult ShowAngular(int id)
        {
            var list = GetItems(id);
            HttpContext.Session.Remove(SessionListItemsKey);
            HttpContext.Session.SetComplexData(SessionListItemsKey, list);
            return Redirect("/angular");
        }

        private List<Item> GetItems(int id)
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

        private List<Item> CreateItemsList(List<SyndicationItem> list)
        {
            var itemsList = new List<Item>();
            foreach(var item in list)
            {
                itemsList.Add(new Item { Title = item.Title.Text, 
                    Summary = item.Summary.Text, Publishdate = item.PublishDate.ToString("D") });
            }
            return itemsList;
        }
=======
    [Route("api/items")]
    public class ItemsController : Controller
    {
        [HttpGet]
        public IEnumerable<Item> Get()
        {
            var list = new List<Item>();
            list.Add(new Item() { Title = "title1", PublishDate = "date1", Summary = "summary1" });
            list.Add(new Item() { Title = "title2", PublishDate = "date2", Summary = "summary2" });
            return list;
        }

        [HttpGet]
        public IEnumerable<Item> GetSome()
        {
            var list = new List<Item>();
            list.Add(new Item() { Title = "title3", PublishDate = "date3", Summary = "summary3" });
            list.Add(new Item() { Title = "title4", PublishDate = "date4", Summary = "summary4" });
            return list;
        }
    }

    
}

namespace Phytime.Models
{
    public class Item
    {
        public string Title { get; set; }
        public string PublishDate { get; set; }
        public string Summary { get; set; }
>>>>>>> 03436020527dea8a234f10a1cff9c93daa27112e
    }
}
