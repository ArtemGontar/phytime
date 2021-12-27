using System.Collections.Generic;
using System.ServiceModel.Syndication;

namespace Phytime.Models
{
    public class DateComparer<T> : IComparer<T> 
        where T : SyndicationItem
    {
        public int Compare(T x, T y)
        {
            if (x.PublishDate < y.PublishDate)
                return 1;
            if (x.PublishDate > y.PublishDate)
                return -1;
            else return 0;
        }
    }
}
