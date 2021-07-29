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
    [Route("api/products")]
    public class ItemsController : Controller
    {
        public static List<Product> list;
        public ItemsController()
        {
            //list.Add(new Product { Id = 1, Name = "iPhone X", Company = "Apple", Price = 79900 });
            //list.Add(new Product { Id = 2, Name = "Galaxy S8", Company = "Samsung", Price = 49900 });
            //list.Add(new Product { Id = 3, Name = "Pixel 2", Company = "Google", Price = 52900 });
        }
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            //list.Add(new Product { Id = 1, Name = "iPhone X", Company = "Apple", Price = 79900 });
            //list.Add(new Product { Id = 2, Name = "Galaxy S8", Company = "Samsung", Price = 49900 });
            //list.Add(new Product { Id = 3, Name = "Pixel 2", Company = "Google", Price = 52900 });
            return list;
        }

        [HttpGet("{id}")]
        public Product Get(int id)
        {
            Product product = list.FirstOrDefault(x => x.Id == id);
            return product;
        }

        //[HttpPost]
        //public IActionResult Post(Product product)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        list.Add(product);
        //        return Ok(product);
        //    }
        //    return BadRequest(ModelState);
        //}

        [HttpPut]
        public IActionResult Put(Product product)
        {
            if (ModelState.IsValid)
            {
                return Ok(product);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Product product = list.FirstOrDefault(x => x.Id == id);
            if (product != null)
            {
                list.Remove(product);
            }
            return Ok(product);
        }

        [HttpPost]
        public IActionResult ShowAngular(string urlSource)
        {
            //list = GetItems(urlSource);
            return Ok(urlSource);
            //return Redirect("/angular");
        }

        private List<Product> GetItems(string url)
        {
            var list = GetSyndicationItems(url);
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

    //[ApiController]
    //[Route("api/items")]
    //public class ItemsController : Controller
    //{
    //    [HttpGet]
    //    public IEnumerable<Item> Get()
    //    {
    //        var list = new List<Item>();
    //        list.Add(new Item() { Title = "title1", PublishDate = "date1", Summary = "summary1" });
    //        list.Add(new Item() { Title = "title2", PublishDate = "date2", Summary = "summary2" });
    //        return list;
    //    }

    //    //[HttpGet]
    //    //public IEnumerable<Item> GetSome()
    //    //{
    //    //    var list = new List<Item>();
    //    //    list.Add(new Item() { Title = "title3", PublishDate = "date3", Summary = "summary3" });
    //    //    list.Add(new Item() { Title = "title4", PublishDate = "date4", Summary = "summary4" });
    //    //    return list;
    //    //}
    //}


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
