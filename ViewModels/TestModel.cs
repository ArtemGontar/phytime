using Phytime.Models;
using System.Collections.Generic;

namespace Phytime.ViewModels
{
    public class TestModel
    {
        public TestModel()
        {
            Answers = new Dictionary<string, string>();
            Questions = new Dictionary<string, Question>();
            Correct = new Dictionary<string, string>();
        }
        public Dictionary<string, string> Answers { get; set; }
        public Dictionary<string, string> Correct { get; set; }
        public Dictionary<string, Question> Questions { get; set; }
        public TestModel Add(string key, Question value)
        {
            Questions.Add(key, value);
            return this;
        }
    }
}