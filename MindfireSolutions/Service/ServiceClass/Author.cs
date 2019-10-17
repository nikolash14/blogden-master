using MindfireSolutions.Service.ServiceInterface;
using System;
using System.Linq;
using MindfireSolutions.ViewModel;
using MindfireSolutions.DataAccess;
using System.Collections.Generic;
using MindfireSolutions.Models;

namespace MindfireSolutions.Service.ServiceClass
{
    public class Author : IAuthor
    {
        /// <summary>
        /// Object for Database Access by Link
        /// </summary>
        private DAL db = new DAL();

        public VMAllBlog AllArchivedBlog(string email)
        {
            var user = db.Users.FirstOrDefault(m => m.Email == email);
            var blog = db.Blogs.Where(m => m.UserId == user.UserId && m.BlogStatus == 2).OrderByDescending(m => m.CreationTime).ToList();
            var data = new VMAllBlog()
            {
                UserId = user.UserId,
                Name = user.Name,
                UserImage = user.ImageUrl,
                Email = user.Email,
                Mobile = user.Mobile,
                Blogs = blog

            };
            return data;
        }

        /// <summary>
        /// Method gives all blogs by the Blogger
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Blog list to Controller</returns>
        public VMAllBlog AllBlogs(string email)
        {
            var user = db.Users.FirstOrDefault(m => m.Email == email);
            var blog = db.Blogs.Where(m => m.UserId == user.UserId).OrderByDescending(m => m.CreationTime).ToList();
            var data = new VMAllBlog()
            {
                UserId = user.UserId,
                Name = user.Name,
                UserImage = user.ImageUrl,
                Email = user.Email,
                Mobile = user.Mobile,
                Blogs = blog

            };
            return data;
        }

        public VMAllBlog AllDraftedContent(string email)
        {
            var user = db.Users.FirstOrDefault(m => m.Email == email);
            var blog = db.Blogs.Where(m => m.UserId == user.UserId && m.BlogStatus == 3).OrderByDescending(m => m.CreationTime).ToList();
            var data = new VMAllBlog()
            {
                UserId = user.UserId,
                Name = user.Name,
                UserImage = user.ImageUrl,
                Email = user.Email,
                Mobile = user.Mobile,
                Blogs = blog

            };
            return data;
        }

        public bool ArchiveBlog(int blogId)
        {
            var blog = db.Blogs.FirstOrDefault(m => m.BlogId == blogId);
            if (blog != null)
            {
                blog.BlogStatus = 2;
                db.SaveChanges();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Method gives Data for the Auther DashBoard
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Data to Controller</returns>
        public VMAuthorData Data(string email)
        {
            int like = 0, disLike = 0, spam = 0, comment = 0;
            if (email != string.Empty)
            {
                var user = db.Users.FirstOrDefault(m => m.Email == email);
                var blog = db.Blogs.Where(m => m.UserId == user.UserId && m.BlogStatus == 1).ToList();
                var blogConut = blog.Count();
                var blogLastEight = blog.OrderByDescending(m => m.CreationTime).Take(8).ToList();
                var blogResponse = db.GetBlogStatusCount.Where(m => m.UserId == user.UserId).ToList();
                foreach (var item in blogResponse)
                {
                    like = like + item.LikesCount;
                    disLike = disLike + item.DislikesCount;
                    spam = spam + item.SpamCount;
                    comment = comment + item.CommentsCount;
                }
                var archived = db.Blogs.Where(m => m.UserId == user.UserId && m.BlogStatus == 2).ToList();
                var drafted = db.Blogs.Where(m => m.UserId == user.UserId && m.BlogStatus == 3).ToList();
                var data = new VMAuthorData()
                {
                    UserId = user.UserId,
                    Name = user.Name,
                    UserImage = user.ImageUrl,
                    Email = user.Email,
                    Mobile = user.Mobile,
                    Blog = blogLastEight,
                    PublishedPostCount = blogConut,
                    LikeCount = like,
                    DislikeCount = disLike,
                    SpamCount = spam,
                    CommentCount = comment,
                    Archived = archived,
                    Drafted = drafted

                };
                if (data != null)
                {
                    return data;
                }
                return null;
            }
            return null;

        }

        /// <summary>
        /// Delete the Blogger Method
        /// </summary>
        /// <param name="email"></param>
        /// <returns>True of false if User is deleted from database</returns>
        public bool Delete(string email)
        {
            if (email != string.Empty)
            {
                var data = db.Users.FirstOrDefault(m => m.Email == email);
                var blogs = db.Blogs.Where(m => m.UserId == data.UserId).ToList();
                foreach (var blog in blogs)
                {
                    var tag = db.Tags.Where(m => m.BlogId == blog.BlogId);
                    db.Tags.RemoveRange(tag);
                }
                db.Blogs.RemoveRange(blogs);
                db.Users.Remove(data);
                db.SaveChanges();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Method to Delete a Blog
        /// </summary>
        /// <param name="blogId"></param>
        /// <returns>boolean if the blog is deleted</returns>
        public bool DeleteBlog(int blogId)
        {
            if (blogId != 0)
            {
                var data = db.Blogs.Where(m => m.BlogId == blogId);
                var response = db.Responses.Where(m => m.BlogId == blogId);
                var tag = db.Tags.Where(m => m.BlogId == blogId);
                db.Responses.RemoveRange(response);
                db.Blogs.RemoveRange(data);
                db.Tags.RemoveRange(tag);
                db.SaveChanges();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Method for fetching Profile data of Blogger
        /// </summary>
        /// <param name="email"></param>
        /// <returns>profile object to Controller</returns>
        public VMAuthor Details(string email)
        {
            var user = db.Users.FirstOrDefault(m => m.Email == email);
            var skills = db.UserSkills.Where(m => m.UserId == user.UserId).ToList();
            List<string> bloggerSkills = new List<string>();
            List<string> bloggerCompany = new List<string>();
            foreach (var item in skills)
            {
                if (item.Company == null)
                {
                    bloggerSkills.Add(item.skill);
                }
                bloggerCompany.Add(item.Company);
            }
            var auther = new VMAuthor()
            {
                UserId = user.UserId,
                Name = user.Name,
                UserImage = user.ImageUrl,
                Email = user.Email,
                Mobile = user.Mobile,
                Description = user.Description,
                DOB = user.DateOfBirth.ToShortDateString(),
                Gender = user.Gender,
                Skills = bloggerSkills,
                Company = bloggerCompany,
                Rank=user.Rank
            };
            return auther;
        }

        public bool Draft(VMDraftBlog data)
        {
            if (data != null)
            {
                var user = db.Users.FirstOrDefault(m => m.UserId == data.UserId);
                var blog = new Blog()
                {
                    BlogStatus = 3,
                    Title = data.BlogTitle,
                    Description = data.BlogContent,
                    CreationTime = DateTime.Now,
                    LastEditTime = DateTime.Now,
                    CreatedBy = user.Email,
                    EditedBy = user.Email,
                    User = user,
                    TopicId = data.SubTopic
                };
                db.Blogs.Add(blog);
                var blogCount = new BlogStatusCount()
                {
                    LikesCount = 0,
                    DislikesCount = 0,
                    SpamCount = 0,
                    CommentsCount = 0,
                    GetBlogs = blog,
                    UserId = blog.UserId
                };
                db.GetBlogStatusCount.Add(blogCount);
                var tag = new Tag()
                {
                    TagTitle = user.Name,
                    Blog = blog
                };
                db.Tags.Add(tag);
                db.SaveChanges();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Method for Editing The Blog.
        /// </summary>
        /// <param name="author"></param>
        /// <returns></returns>
        public bool Edit(VMAuthor author)
        {
            var data = db.Users.FirstOrDefault(m => m.UserId == author.UserId);
            if (data != null)
            {
                data.Name = author.Name;
                data.Mobile = author.Mobile;
                data.Description = author.Description;
                db.SaveChanges();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Method gives data for Editing the Blog
        /// </summary>
        /// <param name="blogId"></param>
        /// <returns>Object of VMEditBlog Model</returns>
        public VMEditBlog EditBlog(int blogId)
        {
            if (blogId != 0)
            {
                var blog = db.Blogs.FirstOrDefault(m => m.BlogId == blogId);
                var user = db.Users.FirstOrDefault(m => m.UserId == blog.UserId);
                var tag = db.Tags.FirstOrDefault(m => m.BlogId == blogId);
                var data = new VMEditBlog()
                {
                    UserId = user.UserId,
                    Name = user.Name,
                    UserImage = user.ImageUrl,
                    BlogDescription = blog.Description,
                    BlogId = blog.BlogId,
                    BlogTitle = blog.Title,
                    TagId = tag.TagId,
                    TagTitle = tag.TagTitle,
                    Email = user.Email,
                };
                if (data != null)
                {
                    return data;
                }
                return null;
            }
            return null;
        }

        public bool Extract(int blogId)
        {
            var blog = db.Blogs.FirstOrDefault(m => m.BlogId == blogId);
            if (blog != null)
            {
                blog.BlogStatus = 1;
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public VMPersonalDetails PersonalDetails(string email)
        {
            if (email != string.Empty)
            {
                var user = db.Users.FirstOrDefault(m => m.Email == email);
                var author = new VMPersonalDetails()
                {
                    UserImage = user.ImageUrl,
                    Name = user.Name,
                    UserId = user.UserId,
                    DOB = user.DateOfBirth,
                    Description = user.Description,
                    Gender = user.Gender
                };
                return author;
            }
            return null;

        }

        /// <summary>
        /// Method Saves the Edited Blog
        /// </summary>
        /// <param name="blog"></param>
        /// <returns>Boolean if Blog is saved</returns>
        public bool SaveEditedBlog(VMEditBlog blog)
        {
            if (blog != null)
            {
                var data = db.Blogs.FirstOrDefault(m => m.BlogId == blog.BlogId);
                if (data != null)
                {
                    data.Title = blog.BlogTitle;
                    data.Description = blog.BlogDescription;
                    data.EditedBy = blog.Email;
                    data.LastEditTime = DateTime.Now;
                    data.BlogStatus = 2;
                }
                var tag = db.Tags.FirstOrDefault(m => m.TagId == blog.TagId);
                if (tag != null)
                {
                    tag.TagTitle = blog.TagTitle;
                }
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public bool SavePersonalData(VMPersonalDetails data)
        {
            if (data != null)
            {
                var user = db.Users.FirstOrDefault(m => m.UserId == data.UserId);
                user.Gender = data.Gender;
                user.Description = data.Description;
                user.DateOfBirth = data.DOB;
                List<UserSkill> userSkills = new List<UserSkill>();
                foreach (var item in data.Company)
                {
                    if (item != string.Empty)
                    {
                        var skills = new UserSkill()
                        {
                            Company = item,
                            GetUserSkills = user
                        };
                        userSkills.Add(skills);
                    }


                }
                foreach (var item in data.Skills)
                {
                    if (item != string.Empty)
                    {
                        var skills = new UserSkill()
                        {
                            skill = item,
                            GetUserSkills = user
                        };
                        userSkills.Add(skills);
                    }

                }
                db.UserSkills.AddRange(userSkills);
                db.SaveChanges();
                return true;
            }
            return false;
        }
    }
}