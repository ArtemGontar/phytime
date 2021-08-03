using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;

namespace Phytime.Services
{
    public class FeedSorterer
    {
        private const int _MonthDaysCount = 31;
        private const int _WeekDaysCount = 7;

        private void SortByNewest(ref List<SyndicationItem> list)
        {
            var comparer = new DateComparer<SyndicationItem>();
            list.Sort(comparer);
<<<<<<< HEAD
=======
            //return list;
>>>>>>> 03436020527dea8a234f10a1cff9c93daa27112e
        }

        private void SortByOldest(ref List<SyndicationItem> list)
        {
            var comparer = new DateComparer<SyndicationItem>();
            list.Sort(comparer);
            list.Reverse();
<<<<<<< HEAD
=======
            //return list;
>>>>>>> 03436020527dea8a234f10a1cff9c93daa27112e
        }

        public void SortFeed(string sortType, ref List<SyndicationItem> list)
        {
            if (sortType.Equals("Newest"))
            {
                SortByNewest(ref list);
            }
            else if (sortType.Equals("Oldest"))
            {
                SortByOldest(ref list);
            }
            else if (sortType.Equals("Last week"))
            {
                GetLastWeekPosts(ref list);
            }
            else if(sortType.Equals("Last month"))
            {
                GetLastMonthPosts(ref list);
            }
        }

        private void GetLastWeekPosts(ref List<SyndicationItem> list)
        {
            var newList = new List<SyndicationItem>();
            DateTime currentDate = DateTime.Now;
            foreach (var item in list)
            {
                if (currentDate.AddDays(-_WeekDaysCount) <= item.PublishDate)
                {
                    newList.Add(item);
                }
            }
            list = newList;
        }

        private void GetLastMonthPosts(ref List<SyndicationItem> list)
        {
            var newList = new List<SyndicationItem>();
            DateTime currentDate = DateTime.Now;
            foreach (var item in list)
            {
                if (currentDate.AddDays(-_MonthDaysCount) >= item.PublishDate)
                {
                    newList.Add(item);
                }
            }
            list = newList;
        }
    }
}
