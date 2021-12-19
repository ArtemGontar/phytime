using System.Collections.Generic;
using Phytime.Models.Test;

namespace Phytime.ViewModels.TestModels
{
    public class TestModel
    {
        public TestModel()
        {
            Answers = new Dictionary<string, string>();
            RightAnswers = new Dictionary<string, string>();
            Questions = new Dictionary<string, Question>();
        }
        public Dictionary<string, string> RightAnswers { get; set; }
        public Dictionary<string, string> Answers { get; set; }
        public Dictionary<string, Question> Questions { get; set; }
        public TestModel Add(string key, Question value)
        {
            Questions.Add(key, value);
            return this;
        }
    }
}