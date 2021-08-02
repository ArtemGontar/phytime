using Microsoft.AspNetCore.Mvc;
using Phytime.Models;
using Phytime.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Phytime.Controllers
{
    public class TestController : Controller
    {
        private const int QuestionsCategory = 9;
        private const string SessionTestModelKey = "testModel";

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult StartTest(int count, string difficulty, string type)
        {
            Questions questions = null;
            string url = $"https://opentdb.com/api.php?" +
                $"amount={count}&category={QuestionsCategory}&difficulty={difficulty}&type={type}";
            using (WebClient wc = new WebClient())
            {
                var json = wc.DownloadString(url);
                questions = JsonSerializer.Deserialize<Questions>(json);
            }
            var testModel = new TestModel();
            for (int i = 0; i < questions.results.Count; i++)
            {
                testModel.Add($"question{i}", questions.results[i]);
            }
            HttpContext.Session.SetComplexData(SessionTestModelKey, testModel);
            return View("Questions", testModel);
        }

        public IActionResult CheckTest(TestModel model)
        {
            var originalTestmodel = HttpContext.Session.GetComplexData<TestModel>(SessionTestModelKey);
            var resultModelList = new TestResultModelList();
            for(int i = 0; i < model.Answers.Count; i++)
            {
                var resultModel = new TestResultModel()
                {
                    Number = i + 1,
                    Answer = model.Answers[model.Answers.ElementAt(i).Key],
                    RightAnswer = originalTestmodel.Questions[model.Answers.ElementAt(i).Key].correct_answer,

                };
                resultModelList.Add(resultModel);
            }
            return View("Result", resultModelList);
        }
    }
}
