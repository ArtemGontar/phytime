using System;
using System.Collections.Generic;

namespace Phytime.Models.Test
{
    public class Question
    {
        private static Random rng = new Random();

        public string category { get; set; }
        public string type { get; set; }
        public string difficulty { get; set; }
        public string question { get; set; }
        public string correct_answer { get; set; }
        public List<string> incorrect_answers { get; set; }

        public List<string> GetShuffledAnswers()
        {
            var suffleList = new List<string>(incorrect_answers);
            suffleList.Add(correct_answer);
            int n = suffleList.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                string value = suffleList[k];
                suffleList[k] = suffleList[n];
                suffleList[n] = value;
            }
            return suffleList;
        }
    }
}
