using System;
using System.Collections.Generic;
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
        }

        private void SortByOldest(ref List<SyndicationItem> list)
        {
            var comparer = new DateComparer<SyndicationItem>();
            list.Sort(comparer);
            list.Reverse();
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
                int result = DateTime.Compare(currentDate.AddDays(-_WeekDaysCount),
                    item.PublishDate.DateTime);
                if (result < 0)
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
                int result = DateTime.Compare(currentDate.AddDays(-_MonthDaysCount),
                    item.PublishDate.DateTime);
                if (result < 0)
                {
                    newList.Add(item);
                }
            }
            list = newList;
        }
    }
}
