using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Phytime.Models;
using System;
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
        private const int FirstValidId = 1;
        private readonly IRepository<Feed, User> _feedRepository;

        public ItemsController(IRepository<Feed, User> repository = null)
        {
            _feedRepository = repository ?? new FeedRepository();
        }

        [HttpGet("{id:int}")]
        public IEnumerable<Item> Get(int id)
        {
            if (id < FirstValidId)
            {
                throw new ArgumentException(nameof(id));
            }
            return GetItems(id);
        }

        private List<Item> GetItems(int id)
        {
            if (id < FirstValidId)
            {
                throw new ArgumentException(nameof(id));
            }
            var feed = _feedRepository.Get(id);
            var list = GetSyndicationItems(feed.Url);
            return CreateItemsList(list);
        }

        public List<SyndicationItem> GetSyndicationItems(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException(nameof(url));
            }
            XmlReader reader = XmlReader.Create(url);
            SyndicationFeed feed = SyndicationFeed.Load(reader);
            reader.Close();
            return feed.Items.ToList();
        }

        public List<Item> CreateItemsList(List<SyndicationItem> list)
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }
            var itemsList = new List<Item>();
            foreach(var item in list)
            {
                itemsList.Add(new Item { Title = item.Title.Text, 
                    Summary = item.Summary.Text, Publishdate = item.PublishDate.ToString("D") });
            }
            return itemsList;
        }

        protected override void Dispose(bool disposing)
        {
            _feedRepository.Dispose();
            base.Dispose(disposing);
        }
    }
}
