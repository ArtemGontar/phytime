using Microsoft.EntityFrameworkCore;
using Moq;
using Phytime.Controllers;
using Phytime.Models;
using System;
using System.Collections.Generic;
using Xunit;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace UnitTestApp.Tests
{
    public class FeedControllerTests
    {
        [Fact]
        public void RssFeedTest()
        {
            var data = GetTestFeeds();
            var mockSet = new Mock<DbSet<Feed>>();
            mockSet.As<IQueryable<Feed>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Feed>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Feed>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Feed>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<PhytimeContext>();
            mockContext.Setup(c => c.Feeds).Returns(mockSet.Object);

            var mockConfig = new Mock<IConfiguration>();
            FeedController controller = new FeedController(mockConfig.Object);
            controller.SetContext(mockContext.Object);

            var actual = controller.IsSubscribed("https://psyjournals.ru/rss/psyedu.rss");

            Assert.False(actual);
        }

        [Fact]
        public void GetSyndicationItemsTest()
        {
            // Arrange
            var mock = new Mock<IConfiguration>();
            FeedController controller = new FeedController(mock.Object);

            // Act
            int result = controller.GetSyndicationItems("https://psyjournals.ru/rss/psyedu.rss").Count;

            // Assert
            Assert.Equal(7, result);
        }

        private IQueryable<Feed> GetTestFeeds()
        {
            var feeds = new List<Feed>
            {
                new Feed() { ItemsCount = 7, Url =  "https://psyjournals.ru/rss/psyedu.rss" },
                new Feed() { ItemsCount = 5, Url =  "https://psyjournals.ru/rss/psyedu.rss" },
            }.AsQueryable();
            return feeds;
        }
    }
}
