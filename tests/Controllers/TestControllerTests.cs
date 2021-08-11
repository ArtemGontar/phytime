using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Moq;
using Phytime.Controllers;
using Phytime.Models;
using Phytime.ViewModels;
using Xunit;

namespace UnitTestApp.Tests.Controllers
{
    public class TestControllerTests
    {
        [Fact]
        public void StartTest_Returns5Questions_Returned5()
        {
            var option = Options.Create(new ConnectionStringsOptions() { TestsConnection = "https://opentdb.com/api.php?" });
            var controller = new TestController(option);
            int count = 5;
            string difficulty = "hard";
            string type = "boolean";

            var view = controller.StartTest(count, difficulty, type);

            var viewResult = Assert.IsType<ViewResult>(view);
            var model = Assert.IsAssignableFrom<TestModel>(
                viewResult.Model);
            Assert.Equal(count, model.Questions.Count);
        }

        [Fact]
        public void CheckTest_Returns1Point_1Point()
        {
            var configurationMock = new Mock<IOptions<ConnectionStringsOptions>> ();
            var controller = new TestController(configurationMock.Object);
            var testModel = new TestModel();
            testModel.Answers.Add("key1", "rightAnswer");
            testModel.Answers.Add("key2", "wrongAnswer");
            testModel.RightAnswers.Add("key1", "rightAnswer");
            testModel.RightAnswers.Add("key2", "rightAnswer");
            int resultPoints = 1;

            var view = controller.CheckTest(testModel);

            var viewResult = Assert.IsType<ViewResult>(view);
            var model = Assert.IsAssignableFrom<TestResultModelList>(
                viewResult.Model);
            Assert.Equal(resultPoints, model.Points);
        }
    }
}
