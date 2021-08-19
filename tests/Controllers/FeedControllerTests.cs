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
        public void GetSyndicationItems_ItemsCountSeven_SevenReturned()
        {
            var configurationMock = new Mock<PhytimeContext>();
            var feedRepositoryMock = new Mock<IRepository<Feed>>();
            var userRepositoryMock = new Mock<IRepository<User>>();
            FeedController controller = new FeedController(configurationMock.Object, 
                feedRepositoryMock.Object, userRepositoryMock.Object);
            int result = controller.GetSyndicationItems(SourceUrl).Count;
            Assert.Equal(SourceUrlItemsCount, result);
        }

        [Fact]
        public void FormFeed_FeedPropertiesNotEmpty_True()
        {
            var configurationMock = new Mock<PhytimeContext>();
            var feedRepositoryMock = new Mock<IRepository<Feed>>();
            var userRepositoryMock = new Mock<IRepository<User>>();
            var url = SourceUrl;
            var page = 1;
            var syndicationItems = new List<SyndicationItem>()
            {
                new SyndicationItem(),
                new SyndicationItem(),
                new SyndicationItem()
            };
            feedRepositoryMock.Setup(repo => repo.GetBy(url)).Returns(GetTestFeed(""));
            FeedController controller = new FeedController(configurationMock.Object,
                feedRepositoryMock.Object, userRepositoryMock.Object);

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
