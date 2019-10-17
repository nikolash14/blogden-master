using MindfireSolutions.Service.ServiceInterface;
using System;
using MindfireSolutions.ViewModel;
using MindfireSolutions.DataAccess;
using MindfireSolutions.Models;
using System.Linq;

namespace MindfireSolutions.Service.ServiceClass
{
    public class ContactMessage : IContactMessage
    {
        DAL dbReference = new DAL();
        public bool CreateMessage(VMMessage message)
        {
            var messageData = new Message()
            {
                Name = message.VisitorName,
                Email = message.VisitorEmail,
                Comment = message.VisitorComment,
                CreationTime = DateTime.Now,
            };
            dbReference.Messages.Add(messageData);
            int returnValue = dbReference.SaveChanges();
            return returnValue > 0 ? true : false;
        }

        public VMBloggerDetails GetBloggerDetails(int id)
        {
            var user = dbReference.Users.FirstOrDefault(m => m.UserId == id);
            var blogs = dbReference.Blogs.Where(m => m.UserId == user.UserId && m.BlogStatus == 1).ToList();
            var stats = new BlogStatusCount();
            foreach (var item in blogs)
            {
                var dataFetched = dbReference.GetBlogStatusCount.FirstOrDefault(m => m.BlogId == item.BlogId);
                if (dataFetched != null)
                {
                    stats.CommentsCount = stats.CommentsCount + dataFetched.CommentsCount;
                    stats.LikesCount = stats.LikesCount + dataFetched.LikesCount;
                }

            }
            var data = new VMBloggerDetails()
            {
                Blogs = blogs,
                User = user,
                Stats = stats
            };
            return data;
        }
    }
}