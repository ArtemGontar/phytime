using Phytime.Services;
using System;
using Xunit;

namespace UnitTestApp.Tests.Services
{
    public class RssSourceTests
    {
        [Fact]
        public void GetUrlLists_ReturnsUrlList_True()
        {
            var source = RssSource.getInstance();

            Assert.NotEmpty(source.Urls);
        }

        [Fact]
        public void GetTitleLists_ReturnsTitleList_True()
        {
            var source = RssSource.getInstance();

            Assert.NotEmpty(source.Titles);
        }

        [Fact]
        public void GetTitlesAndUrls_ListsCountEqual_True()
        {
            var source = RssSource.getInstance();

            Assert.Equal(source.Titles.Count, source.Urls.Count);
        }
    }
}
