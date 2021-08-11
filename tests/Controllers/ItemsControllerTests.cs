using Microsoft.Extensions.Configuration;
using Moq;
using Phytime.Controllers;
using Phytime.Models;
using System.Collections.Generic;
using System;
using System.ServiceModel.Syndication;
using Xunit;
using Microsoft.Extensions.Options;

namespace UnitTestApp.Tests.Controllers
{
    public class ItemsControllerTests
    {
        [Fact]
        public void GetSyndicationItems_ItemsListNotEmpty_True()
        {
            var configurationMock = new Mock<IOptions<ConnectionStringsOptions>>();
            var repositoryMock = new Mock<IRepository<Feed, User>>();
            var controller = new ItemsController(configurationMock.Object, repositoryMock.Object);

            var result = controller.GetSyndicationItems("https://psyjournals.ru/rss/psyedu.rss");

            Assert.NotEmpty(result);
        }

        [Fact]
        public void CreateItemsList_ListNotEmpty_True()
        {
            var configurationMock = new Mock<IOptions<ConnectionStringsOptions>>();
            var repositoryMock = new Mock<IRepository<Feed, User>>();
            var controller = new ItemsController(configurationMock.Object, repositoryMock.Object);

            var result = controller.CreateItemsList(GetTestSyndicationItems());

            Assert.NotEmpty(result);
        }

        public List<SyndicationItem> GetTestSyndicationItems()
        {
            var list = new List<SyndicationItem>()
            {
                new SyndicationItem { Title = new TextSyndicationContent("title1"), 
                    Summary = new TextSyndicationContent("content1"),
                    PublishDate = DateTime.Now},
                new SyndicationItem { Title = new TextSyndicationContent("title2"),
                    Summary = new TextSyndicationContent("content2"),
                    PublishDate = DateTime.Now},
            };
            return list;
        }
    }
}
