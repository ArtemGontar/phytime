using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using Phytime.Controllers;
using Phytime.ViewModels;
using Xunit;

namespace UnitTestApp.Tests.Controllers
{
    public class TestControllerTests
    {
        [Fact]
        public void StartTestReturnsViewWithQuestions()
        {
            var mock = new Mock<IConfiguration>();
            mock.Setup(conf => conf.GetSection("Links:test").Value).Returns("https://opentdb.com/api.php?");
            var controller = new TestController(mock.Object);
            int count = 5;
            string difficulty = "easy";
            string type = "boolean";

            var view = controller.StartTest(count, difficulty, type);

            var viewResult = Assert.IsType<ViewResult>(view);
            var model = Assert.IsAssignableFrom<TestModel>(
                viewResult.Model);
            Assert.Equal(count, model.Questions.Count);
        }

        [Fact]
        public void CheckTestReturnsViewWithResultModelList()
        {
            var mock = new Mock<IConfiguration>();
            var controller = new TestController(mock.Object);
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
