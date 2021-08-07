using Microsoft.EntityFrameworkCore;
using Phytime.Controllers;
using Phytime.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace UnitTestApp.Tests
{
    public class FeedControllerTests
    {
        [Fact]
        public void GetSyndicationItemsTest()
        {
            // Arrange

            //В старт теста
            //string connection = "Server=(localdb)\\mssqllocaldb;Database=phytime2021db;Trusted_Connection=True;";
            //var optionsBuilder = new DbContextOptionsBuilder<PhytimeContext>();
            //var options = optionsBuilder
            //        .UseSqlServer(connection)
            //        .Options;
            //PhytimeContext db = new PhytimeContext(options);

            FeedController controller = new FeedController();

            // Act
            int result = controller.GetSyndicationItems("https://psyjournals.ru/rss/psyedu.rss").Count;

            // Assert
            Assert.Equal(7, result);
        }

        private List<Feed> GetTestFeeds()
        {
            var feeds = new List<Feed>
            {
                new Feed() { ItemsCount = 7, Url =  "https://psyjournals.ru/rss/psyedu.rss" },
                new Feed() { ItemsCount = 5, Url =  "https://psyjournals.ru/rss/psyedu.rss" },
            };
            return feeds;
        }
    }
}
