using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;

namespace Phytime.Services
{
    public class FeedSorterer
    {
        private void SortByNewest(ref List<SyndicationItem> list)
        {
            var comparer = new DateComparer<SyndicationItem>();
            list.Sort(comparer);
            //return list;
        }

        private void SortByOldest(ref List<SyndicationItem> list)
        {
            var comparer = new DateComparer<SyndicationItem>();
            list.Sort(comparer);
            list.Reverse();
            //return list;
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
                if (currentDate.AddDays(-7) <= item.PublishDate)
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
                if (currentDate.AddDays(-31) >= item.PublishDate)
                {
                    newList.Add(item);
                }
            }
            list = newList;
        }
    }
}
