using Microsoft.AspNetCore.Mvc;
using Phytime.Controllers;
using Phytime.Services;
using Xunit;

namespace UnitTestApp.Tests.Controllers
{
    public class HomeControllerTests
    {
        [Fact]
        public void Index_ModelItemsNotEmpty_True()
        {
            var controller = new HomeController();

            var result = controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<RssSource>(
                viewResult.Model);
            Assert.NotEmpty(model.Sources);
        }
    }
}
