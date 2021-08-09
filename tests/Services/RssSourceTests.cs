using Phytime.Services;
using System;
using Xunit;

namespace UnitTestApp.Tests.Services
{
    public class RssSourceTests
    {
        [Fact]
        public void GetUrlListsTest()
        {
            var source = RssSource.getInstance();

            Assert.NotEmpty(source.Urls);
        }

        [Fact]
        public void GetTitleListsTest()
        {
            var source = RssSource.getInstance();

            Assert.NotEmpty(source.Titles);
        }

        [Fact]
        public void TitlesAndUrlsCountEqualTest()
        {
            var source = RssSource.getInstance();

            Assert.Equal(source.Titles.Count, source.Urls.Count);
        }
    }
}
