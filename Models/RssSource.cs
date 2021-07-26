using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace Phytime.Models
{
    public class RssSource
    {
        const string SourceUrl = "https://psyjournals.ru/rss/";
        const string regularExpressionPatternTitles = @"<th.*?>(.*?)<\/th>";
        const string regularExpressionPatternLinks = @"\bhttps://[^<]*";
        const string containsForTitle = "</th>";
        const string containsForUrl = ".rss</a>";

        public List<string> Titles { get; private set; }
        public List<string> Urls { get; private set; }

        private static RssSource instance;

        private RssSource()
        {
            Titles = GetTitles();
            Urls = GetUrls();
        }

        public static RssSource getInstance()
        {
            if (instance == null)
                instance = new RssSource();
            return instance;
        }

        private List<string> GetTitles()
        {
            return GetList(regularExpressionPatternTitles, containsForTitle,
                (regex, line, list) =>
                {
                    MatchCollection collection = regex.Matches(line);
                    Match m = collection[0];
                    list.Add(m.Groups[1].Value);
                });
        }

        private List<string> GetUrls()
        {
            return GetList(regularExpressionPatternLinks, containsForUrl,
                (regex, line, list) =>
                {
                    Match match = regex.Match(line);
                    list.Add(match.Value);
                });
        }

        private List<string> GetList(string regularExpressionPattern, string containsFor,
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
