using Moq;
using Phytime.Controllers;
using Phytime.Models;
using System.Collections.Generic;
using Xunit;
using Microsoft.Extensions.Configuration;
using System.ServiceModel.Syndication;

namespace UnitTestApp.Tests.Controllers
{
    public class FeedControllerTests
    {
        private const string SourceUrl = "https://psyjournals.ru/rss/psyedu.rss";
        private const int SourceUrlItemsCount = 7;

        [Fact]
        public void GetSyndicationItemsTest()
        {
            var mock = new Mock<IConfiguration>();
            var mockRep = new Mock<PhytimeRepository>();
            FeedController controller = new FeedController(mock.Object, mockRep.Object);
            int result = controller.GetSyndicationItems(SourceUrl).Count;
            Assert.Equal(SourceUrlItemsCount, result);
        }

        [Fact]
        public void FormFeedTest()
        {
            var mockConf = new Mock<IConfiguration>();
            var mockRep = new Mock<PhytimeRepository>();
            var url = SourceUrl;
            var page = 1;
            var syndicationItems = new List<SyndicationItem>()
            {
                new SyndicationItem(),
                new SyndicationItem(),
                new SyndicationItem()
            };
            mockRep.Setup(repo => repo.GetFeedByUrl(url)).Returns(GetTestFeed(""));
            mockConf.Setup(conf => conf.GetSection("FeedPageInfo:pageSize").Value).Returns("5");
            FeedController controller = new FeedController(mockConf.Object, mockRep.Object);

            var feed = controller.FormFeed(url, page, syndicationItems);

            Assert.NotNull(feed.PageInfo);
            Assert.NotNull(feed.SyndicationItems);
        }

        private Feed GetTestFeed(string s)
        {
            return new Feed() { ItemsCount = SourceUrlItemsCount, Url = SourceUrl };
        }
    }
}
