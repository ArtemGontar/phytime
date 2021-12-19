using Microsoft.AspNetCore.Mvc;
using Phytime.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Xml;

namespace Phytime.Controllers
{
    [ApiController]
    [Route("api/items")]
    public class ItemsController : ControllerBase
    {
        private const int FirstValidId = 1;
        private readonly IRepository<Feed> _feedRepository;

        public ItemsController(IRepository<Feed> feedRepository)
        {
            _feedRepository = feedRepository ?? throw new ArgumentNullException(nameof(feedRepository));
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

        [HttpGet("{id:int}")]
        public IEnumerable<Item> Get(int id)
        {
            if (id < FirstValidId)
            {
                throw new ArgumentException(nameof(id));
            }
            return GetItems(id);
        }

        private List<SyndicationItem> GetSyndicationItems(string url)
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

        private List<Item> CreateItemsList(List<SyndicationItem> list)
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            var itemsList = new List<Item>();
            foreach (var item in list)
            {
                itemsList.Add(new Item
                {
                    Title = item.Title.Text,
                    Summary = item.Summary.Text, Publishdate = item.PublishDate.ToString("D")
                });
            }

            return itemsList;
        }
    }
}
