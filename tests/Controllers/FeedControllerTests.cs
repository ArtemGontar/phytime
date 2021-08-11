using Moq;
using Phytime.Controllers;
using Phytime.Models;
using System.Collections.Generic;
using Xunit;
using Microsoft.Extensions.Configuration;
using System.ServiceModel.Syndication;
using Microsoft.Extensions.Options;

namespace UnitTestApp.Tests.Controllers
{
    public class FeedControllerTests
    {
        private const string SourceUrl = "https://psyjournals.ru/rss/psyedu.rss";
        private const int SourceUrlItemsCount = 7;

        [Fact]
        public void GetSyndicationItems_ItemsCount7_7Returned()
        {
            var configurationMock = new Mock<IOptions<ConnectionStringsOptions>>();
            var repositoryMock = new Mock<IRepository<Feed, User>>();
            FeedController controller = new FeedController(configurationMock.Object, repositoryMock.Object);
            int result = controller.GetSyndicationItems(SourceUrl).Count;
            Assert.Equal(SourceUrlItemsCount, result);
        }

        [Fact]
        public void FormFeed_FeedPropertiesNotEmpty_True()
        {
            var configurationMock = new Mock<IOptions<ConnectionStringsOptions>>();
            var repositoryMock = new Mock<IRepository<Feed, User>>();
            var url = SourceUrl;
            var page = 1;
            var syndicationItems = new List<SyndicationItem>()
            {
                new SyndicationItem(),
                new SyndicationItem(),
                new SyndicationItem()
            };
            repositoryMock.Setup(repo => repo.GetBy(url)).Returns(GetTestFeed(""));
            FeedController controller = new FeedController(configurationMock.Object, repositoryMock.Object);

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
