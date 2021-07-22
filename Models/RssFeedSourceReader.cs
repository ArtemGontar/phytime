using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace Phytime.Models
{
    public static class RssFeedSourceReader
    {
        const string SourceUrl = "https://psyjournals.ru/rss/";
        const string regularExpressionPatternTitles = @"<th.*?>(.*?)<\/th>";
        const string regularExpressionPatternLinks = @"\bhttps://[^<]*";
        const string containsForTitle = "</th>";
        const string containsForUrl = ".rss</a>";

        public static List<string> GetTitles()
        {
            return GetList(regularExpressionPatternTitles, containsForTitle,
                (regex, line, list) =>
                {
                    MatchCollection collection = regex.Matches(line);
                    Match m = collection[0];
                    list.Add(m.Groups[1].Value);
                });
        }

        public static List<string> GetUrls()
        {
            return GetList(regularExpressionPatternLinks, containsForUrl,
                (regex, line, list) =>
                {
                    Match match = regex.Match(line);
                    list.Add(match.Value);
                });
        }

        private static List<string> GetList(string regularExpressionPattern, string containsFor,
            Action<Regex, string, List<string>> addToList)
        {
            var list = new List<string>();

            Regex regex = new Regex(regularExpressionPattern, RegexOptions.Singleline);
            WebRequest request = WebRequest.Create(SourceUrl);
            WebResponse response = request.GetResponse();
            using (StreamReader stream = new StreamReader(response.GetResponseStream()))
            {
                string line = "";
                while ((line = stream.ReadLine()) != null)
                {
                    if (line.Contains(containsFor))
                    {
                        addToList(regex, line, list);
                    }
                }
            }
            return list;
        }
    }
}
