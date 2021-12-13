using Phytime.Services;
using System;
using Xunit;

namespace UnitTestApp.Tests.Services
{
    public class RssSourceTests
    {
        [Fact]
        public void GetSourcesLists_ReturnsUrlList_True()
        {
            var source = RssSource.getInstance();

            Assert.NotEmpty(source.Sources);
        }
    }
}
