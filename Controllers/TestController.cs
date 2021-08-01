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

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Login = HttpContext.User.Identity.Name;
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
                testModel.Correct.Add($"question{i}", questions.results[i].correct_answer);
            }
            return View("Questions", testModel);
        }

        public IActionResult CheckTest(TestModel model)
        {
            int points = 0;
            foreach (var answer in model.Answers)
            {
                if (model.Answers[answer.Key] == model.Questions[answer.Key].correct_answer)
                {
                    points++;
                }
            }
            return Ok(points);
        }
    }
}
