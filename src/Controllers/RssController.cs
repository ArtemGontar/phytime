using System;
using Microsoft.AspNetCore.Mvc;
using Phytime.Models;
using Phytime.Repository;
using Phytime.Services;

namespace Phytime.Controllers
{
    [ApiController]
    [Route("api/rss")]
    public class RssController : ControllerBase
    {
        public const int DefaultPage = 1;
        public const string DefaultSortValue = "Newest";

        private readonly RssSource _rssSource;
        private IRssService _rssService;
        private readonly IRepository<User> _userRepository;

        public RssController(IRssService rssService, 
            IRepository<User> userRepository = null)
        {
            _rssService = rssService;
            _rssSource = RssSource.getInstance();
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        [HttpGet]
        public IActionResult GetRssList()
        {
            return Ok(_rssService.GetSources());
        }
        
        [HttpGet("{id}")]
        public IActionResult GetRss(int id, int page = DefaultPage, string sortValue = DefaultSortValue)
        {
            var viewModel = _rssService.GetSource(id, sortValue, page);
            return Ok(viewModel);
        }
        
        // public IActionResult Sort(FeedViewModel model)
        // {
        //     return RedirectToAction("RssFeed", new { url = model.FeedValue.Url, 
        //         page = DefaultPage, model.SortValue});
        // }

        // public IActionResult Subscribe(string url)
        // {
        //     if (string.IsNullOrEmpty(url))
        //     {
        //         throw new ArgumentNullException(nameof(url));
        //     }
        //     var user = _userRepository.GetBy(HttpContext.User.Identity.Name);
        //     _feedRepository.GetBy(url).Users.Add(user);
        //     _feedRepository.Save();
        //     return RedirectToAction("RssFeed", new { url = url, page = DefaultPage });
        // }
        //
        // public IActionResult Unsubscribe(string url)
        // {
        //     if (string.IsNullOrEmpty(url))
        //     {
        //         throw new ArgumentNullException(nameof(url));
        //     }
        //     var feed = _feedRepository.GetBy(url);
        //     feed = _feedRepository.GetInclude(feed);
        //     var user = _userRepository.GetBy(HttpContext.User.Identity.Name);
        //     feed.Users.Remove(user);
        //     _feedRepository.Save();
        //     return RedirectToAction("RssFeed", new { url = url, page = DefaultPage });
        // }
        
        // public bool IsSubscribed(string url)
        // {
        //     if (string.IsNullOrEmpty(url))
        //     {
        //         throw new ArgumentNullException(nameof(url));
        //     }
        //     var feed = _feedRepository.GetBy(url);
        //     feed = _feedRepository.GetInclude(feed);
        //     foreach(var user in feed.Users)
        //     {
        //         if(user.Email.Equals(HttpContext.User.Identity.Name))
        //         {
        //             return true;
        //         }
        //     }
        //     return false;
        // }
    }
}
