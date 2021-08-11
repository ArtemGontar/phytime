using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
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
        private readonly ConnectionStringsOptions _options;

        public TestController(IOptions<ConnectionStringsOptions> options)
        {
            _options = options.Value;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult StartTest(int count, string difficulty, string type)
        {
            if (count < 1)
            {
                throw new ArgumentException(nameof(count));
            }
            if(!difficulty.Equals("easy") && !difficulty.Equals("medium") && !difficulty.Equals("hard"))
            {
                throw new ArgumentException(nameof(difficulty));
            }
            if (!type.Equals("multiple") && !type.Equals("boolean"))
            {
                throw new ArgumentException(nameof(type));
            }
            Questions questions = null;
            string url = _options.TestsConnection +
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
            return View("Questions", testModel);
        }

        public IActionResult CheckTest(TestModel model)
        {
            if(model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            var resultModelList = new TestResultModelList();
            for (int i = 0; i < model.Answers.Count; i++)
            {
                var resultModel = new TestResultModel()
                {
                    Number = i + 1,
                    Answer = model.Answers[model.Answers.ElementAt(i).Key],
                    RightAnswer = model.RightAnswers[model.Answers.ElementAt(i).Key]
                };
                resultModelList.Add(resultModel);
            }
            return View("Result", resultModelList);
        }
    }
}
