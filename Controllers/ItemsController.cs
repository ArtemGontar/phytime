using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Phytime.Models;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Xml;
using Phytime.Services;

//***********************************Testing angular here**************

namespace Phytime.Controllers
{
    [ApiController]
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
    }
}
