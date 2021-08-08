using Microsoft.AspNetCore.Mvc;
using Phytime.Controllers;
using Phytime.Models;
using Xunit;

namespace UnitTestApp.Tests.Controllers
{
    public class HomeControllerTests
    {
        [Fact]
        public void IndexReturnsViewWithUrls()
        {
            var controller = new HomeController();

            var result = controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<RssSource>(
                viewResult.Model);
            Assert.NotEmpty(model.Titles);
            Assert.NotEmpty(model.Urls);
        }
    }
}
