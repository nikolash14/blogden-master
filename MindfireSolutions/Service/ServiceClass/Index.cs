using MindfireSolutions.Service.ServiceInterface;
using System.Linq;
using MindfireSolutions.ViewModel;
using MindfireSolutions.DataAccess;
using System.Collections.Generic;
using MindfireSolutions.Models;

namespace MindfireSolutions.Service.ServiceClass
{
    public class Index : IIndex
    {
        public VMIndex Fetch()
        {
            DAL db = new DAL();
            var justPublished = db.Blogs.Where(m => m.BlogStatus == 1).Take(6).OrderByDescending(m => m.CreationTime).ToList();
            var hotTopicBlogId = db.GetBlogStatusCount.OrderByDescending(m => m.CommentsCount).Select(m => m.BlogId).First();
            var hotTopic = db.Blogs.FirstOrDefault(m => m.BlogId == hotTopicBlogId);
            var trendingBlogId = db.GetBlogStatusCount.OrderByDescending(m => m.LikesCount).Select(m => m.BlogId).Take(5).ToList();
            List<Blog> trending = new List<Blog>();
            foreach (var item in trendingBlogId)
            {
                var topic = db.Blogs.FirstOrDefault(m => m.BlogId == item && m.BlogStatus == 1);
                trending.Add(topic);
            }
            var data = new VMIndex()
            {
                LatestTopic = justPublished,
                HotTopic = hotTopic,
                TrendingTopic = trending
            };
            return data;
        }
    }
}