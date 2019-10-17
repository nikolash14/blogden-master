namespace MindfireSolutions.Migrations
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MindfireSolutions.DataAccess.DAL>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MindfireSolutions.DataAccess.DAL context)
        {
            List<BlogTopic> addContact = new List<BlogTopic>();

            addContact.Add(new BlogTopic() { TopicName = "Web Development" });
            addContact.Add(new BlogTopic() { TopicName = "Programming Language" });
            addContact.Add(new BlogTopic() { TopicName = "Food" });
            addContact.Add(new BlogTopic() { TopicName = "Travel" });
            context.BlogTopics.AddRange(addContact);

            List<Status> addStatus = new List<Status>();
            addStatus.Add(new Status() { StatusName = "Likes" });
            addStatus.Add(new Status() { StatusName = "Dislikes" });
            addStatus.Add(new Status() { StatusName = "Spam" });
            addStatus.Add(new Status() { StatusName = "Comment" });
            context.Status.AddRange(addStatus);

            List<Tag> addTopicTag = new List<Tag>();
            addTopicTag.Add(new Tag() { TagTitle = "#Asp.Net", TopicId = 1 });
            addTopicTag.Add(new Tag() { TagTitle = "#Django", TopicId = 1 });
            addTopicTag.Add(new Tag() { TagTitle = "#C-Sharp", TopicId = 2 });
            addTopicTag.Add(new Tag() { TagTitle = "#Python", TopicId = 2 });
            addTopicTag.Add(new Tag() { TagTitle = "#Italian", TopicId = 3 });
            addTopicTag.Add(new Tag() { TagTitle = "#Indian", TopicId = 3 });
            addTopicTag.Add(new Tag() { TagTitle = "#Hilly", TopicId = 4 });
            addTopicTag.Add(new Tag() { TagTitle = "#Beach", TopicId = 4 });
            context.Tags.AddRange(addTopicTag);
        }
    }
}
