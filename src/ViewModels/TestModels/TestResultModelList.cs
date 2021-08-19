using System.Collections;
using System.Collections.Generic;

namespace Phytime.ViewModels
{
    public class TestResultModelList
    {
        public TestResultModelList()
        {
            List = new List<TestResultModel>();
        }

        public List<TestResultModel> List { get; private set; }
        public int Points { get; private set; }

        public void Add(TestResultModel model)
        {
            if(model.Answer.Equals(model.RightAnswer))
            {
                Points++;
            }
            List.Add(model);
        }
    }
}
