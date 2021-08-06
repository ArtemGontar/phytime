using Microsoft.AspNetCore.Mvc;
using Phytime.Models;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Xml;

namespace Phytime.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController : Controller
    {
        private readonly PhytimeContext _context;
        private const string SessionListItemsKey = "items";
        public ItemsController(PhytimeContext context)
        {
            _context = context;
        }

        [HttpGet("{id:int}")]
        public IEnumerable<Item> Get(int id)
        {
            return GetItems(id);
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
    }
}
