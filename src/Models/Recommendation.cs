using System.Collections.Generic;

namespace Phytime.Models
{
    public class Recommendation
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string PublishDate { get; set; }
        public string Summary { get; set; }
        public string Author { get; set; }
        public string Link { get; set; }
        public List<RecommendationTag> Tags { get; set; }
        public string Level{ get; set; }
    }

    public class RecommendationTag
    {
        public int Id { get; set; }
        public string Tag { get; set; }
        public int RecommendationId {get; set;}
    }
}
