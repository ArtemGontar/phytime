using Microsoft.AspNetCore.Mvc;
using Phytime.Models;
using System;
using System.Collections.Generic;

namespace Phytime.Controllers
{
    //[ApiController]
    //[Route("api/feeds")]
    public class FeedController
    {
        [HttpGet]
        public IEnumerable<Feed> Get()
        {
            var list = new List<Feed>();
            list.Add(new Feed() { Title = "title1", PublishDate = "date1", Summary = "summary1" });
            list.Add(new Feed() { Title = "title2", PublishDate = "date2", Summary = "summary2" });
            return list;
        }
    }
}
