using MindfireSolutions.Service.ServiceInterface;
using System;
using MindfireSolutions.ViewModel;
using MindfireSolutions.DataAccess;
using MindfireSolutions.Models;
using System.Collections.Generic;
using System.Linq;
using MindfireSolutions.DataModel;
using System.Data.Entity;
using System.Web.Security;

namespace MindfireSolutions.Service.ServiceClass
{
    public class BlogManager : IBlogManager
    {
        private DAL dbReference = new DAL();
        public bool Create(VMBlog blog)
        {
            if (blog.Title != null && blog.SubTopic != 0)
            {
                var blogPost = new Blog()
                {
                    Title = blog.Title,
                    Description = blog.Description,
                    CreationTime = DateTime.Now,
                    UserId = blog.UserId,
                    TopicId = blog.SubTopic,
                    CreatedBy = blog.Email,
                    LastEditTime = DateTime.Now,
                    BlogStatus = 1

                };
                dbReference.Blogs.Add(blogPost);
                var blogCount = new BlogStatusCount()
                {
                    LikesCount = 0,
                    DislikesCount = 0,
                    SpamCount = 0,
                    CommentsCount = 0,
                    GetBlogs = blogPost,
                    UserId = blog.UserId
                };
                dbReference.GetBlogStatusCount.Add(blogCount);
                var tag = new Tag()
                {
                    TagTitle = blog.Tag,
                    Blog = blogPost
                };
                dbReference.Tags.Add(tag);
                dbReference.SaveChanges();
                return true;
            }
            return false;

        }

        public bool Delete(int blogId)
        {
            throw new NotImplementedException();
        }

        public bool Edit(VMBlog blog)
        {
            throw new NotImplementedException();
        }

        public bool Hide(int blogId)
        {
            throw new NotImplementedException();
        }

        public bool SoftDelete(int blogId)
        {
            throw new NotImplementedException();
        }

        public List<BlogTopic> Topics()
        {
            var data = dbReference.BlogTopics.Where(m => m.ParentId == null).ToList();
            return data;
        }
        public List<BlogTopic> SubTopics(int id)
        {
            var data = dbReference.BlogTopics.Where(m => m.ParentId == id).ToList();
            return data;
        }
        public List<Blog> RenderPost(int topicId)
        {
            var blogResult = dbReference.Blogs.Where(m => m.TopicId == topicId && m.BlogStatus == 1).Include(m => m.BlogTopic).Include(m => m.User).ToList();
            if (blogResult != null)
            {
                return blogResult;
            }
            return null;
        }


        public bool AddTopic(AddTopic topic)
        {
            if (topic.SubTopicName != null)
            {
                var dbTopic = dbReference.BlogTopics.FirstOrDefault(m => m.TopicName == topic.TopicName);
                if (dbTopic == null)
                {
                    var parentTopic = new BlogTopic()
                    {
                        TopicName = topic.TopicName
                    };
                    dbReference.BlogTopics.Add(parentTopic);
                    dbReference.SaveChanges();
                    var childTopic = new BlogTopic()
                    {
                        ParentId = parentTopic.TopicId,
                        TopicName = topic.SubTopicName
                    };
                    dbReference.BlogTopics.Add(childTopic);
                    dbReference.SaveChanges();
                }
                else
                {
                    var childTopic = new BlogTopic()
                    {
                        ParentId = dbTopic.TopicId,
                        TopicName = topic.SubTopicName
                    };
                    dbReference.BlogTopics.Add(childTopic);
                    dbReference.SaveChanges();
                }
                return true;
            }
            return false;

        }

        public List<Tag> FetchTags(int topicId)
        {
            if (topicId != 0)
            {
                var data = dbReference.Tags.Where(m => m.TopicId == topicId).ToList();
                return data;
            }
            return null;
        }

        public VMBlogDescription FetchBlogDescription(int blogId)
        {
            var blogger = dbReference.Blogs.FirstOrDefault(m => m.BlogId == blogId);
            var bloggerImage = dbReference.UserImages.FirstOrDefault(m => m.UserId == blogger.UserId);
            var blogResponse = dbReference.GetBlogStatusCount.FirstOrDefault(m => m.BlogId == blogId);
            var user = dbReference.Users.FirstOrDefault(m => m.UserId == blogger.UserId);
            var details = new VMBlogDescription()
            {
                Blogcomments = dbReference.BlogComments.Where(m => m.BlogId == blogId).ToList(),
                Blogs = blogger,
                BloggerImage = bloggerImage,
                Responses = dbReference.Responses.Where(m => m.BlogId == blogId).ToList(),
                Users = dbReference.Users.FirstOrDefault(m => m.UserId == blogger.UserId),
                LikesCount = blogResponse.LikesCount,
                DislikesCount = blogResponse.DislikesCount,
                SpamCount = blogResponse.SpamCount,
                BloggerDescription=user.Description,
                BloggerRank= Convert.ToInt32(user.Rank),
                BloggeUserId=user.UserId
            };
            return details;
        }

        public void SaveBlogsComments(VMSaveBlogComments details, string email)
        {
            var result = new BlogComment()
            {
                Comments = details.Comments,
                BlogId = details.BlogId,
                ParentId = details.ParentId,
                CreatedBy = email,
                CreationTime = DateTime.Now,
                LastEditTime = DateTime.Now
            };
            var blogResponse = dbReference.GetBlogStatusCount.FirstOrDefault(m => m.BlogId == details.BlogId);
            blogResponse.CommentsCount++;
            dbReference.BlogComments.Add(result);
            dbReference.SaveChanges();
        }
        public bool DeleteBlogComments(int commentId, string email)
        {
            if (commentId != 0)
            {
                var details = dbReference.BlogComments.FirstOrDefault(m => m.CommentId == commentId);
                var results = dbReference.BlogComments.Where(m => m.ParentId == commentId);
                dbReference.BlogComments.Remove(details);
                dbReference.BlogComments.RemoveRange(results);
                int blogId = dbReference.BlogComments.FirstOrDefault(m => m.CommentId == commentId).BlogId;
                var blogResponse = dbReference.GetBlogStatusCount.FirstOrDefault(m => m.BlogId == blogId);
                blogResponse.CommentsCount--;
                dbReference.SaveChanges();
                return true;
            }
            return false;
        }
        public VMSearchedBlog Search(string tag)
        {
            if (tag != string.Empty)
            {
                var localData = (from c in dbReference.Blogs where c.Title.Contains(tag) where c.BlogStatus == 1 select c).ToList();

                var data = new VMSearchedBlog()
                {
                    Blogs = localData
                };
                return data;
            }
            return null;
        }

        public bool Likes(string email, int blogId)
        {
            var data = dbReference.Users.FirstOrDefault(m => m.Email == email);
            var response = dbReference.Responses.FirstOrDefault(m => m.UserId == data.UserId && m.BlogId == blogId && m.StatusId == 1);
            var likeCount = dbReference.GetBlogStatusCount.FirstOrDefault(m => m.BlogId == blogId);
            if (response != null)
            {
                if (response.StatusId == 1)
                {
                    dbReference.Responses.Remove(response);
                    if (likeCount != null)
                    {
                        likeCount.LikesCount--;
                    }
                    dbReference.SaveChanges();
                    return false;
                }
            }

            likeCount.LikesCount++;
            var userResponse = new Response()
            {
                UserId = data.UserId,
                StatusId = 1,
                BlogId = blogId,

            };
            dbReference.Responses.Add(userResponse);
            dbReference.SaveChanges();
            return true;
        }

        public bool DisLike(string email, int blogId)
        {
            var data = dbReference.Users.FirstOrDefault(m => m.Email == email);
            var response = dbReference.Responses.FirstOrDefault(m => m.UserId == data.UserId && m.BlogId == blogId && m.StatusId == 2);
            var likeCount = dbReference.GetBlogStatusCount.FirstOrDefault(m => m.BlogId == blogId);
            if (response != null)
            {
                if (response.StatusId == 2)
                {
                    dbReference.Responses.Remove(response);
                    if (likeCount != null)
                    {
                        likeCount.DislikesCount--;
                    }
                    dbReference.SaveChanges();
                    return false;
                }
            }

            likeCount.DislikesCount++;
            var userResponse = new Response()
            {
                UserId = data.UserId,
                StatusId = 2,
                BlogId = blogId,

            };
            dbReference.Responses.Add(userResponse);
            dbReference.SaveChanges();
            return true;
        }

        public bool Spam(string email, int blogId)
        {
            var data = dbReference.Users.FirstOrDefault(m => m.Email == email);
            var response = dbReference.Responses.FirstOrDefault(m => m.UserId == data.UserId && m.BlogId == blogId && m.StatusId == 3);
            var likeCount = dbReference.GetBlogStatusCount.FirstOrDefault(m => m.BlogId == blogId);
            if (response != null)
            {
                if (response.StatusId == 3)
                {
                    dbReference.Responses.Remove(response);
                    if (likeCount != null)
                    {
                        likeCount.SpamCount--;
                    }
                    dbReference.SaveChanges();
                    return false;
                }
            }

            likeCount.SpamCount++;
            var userResponse = new Response()
            {
                UserId = data.UserId,
                StatusId = 3,
                BlogId = blogId,

            };
            dbReference.Responses.Add(userResponse);
            dbReference.SaveChanges();
            return true;
        }

        public VMBlog RenderBlog(string email)
        {
            var userData=dbReference.Users.FirstOrDefault(m=>m.Email==email);
            var data = new VMBlog()
            {
                UserId = userData.UserId,
                UserImage = userData.ImageUrl,
                Name = userData.Name,
                Email = userData.Email
            };
            return data;
        }
    }
}