using MindfireSolutions.DataModel;
using MindfireSolutions.Models;
using MindfireSolutions.ViewModel;
using System.Collections.Generic;

namespace MindfireSolutions.Service.ServiceInterface
{
    public interface IBlogManager
    {
        bool Create(VMBlog blog);
        bool Edit(VMBlog blog);
        bool Delete(int blogId);
        bool SoftDelete(int blogId);
        bool Hide(int blogId);
        List<BlogTopic> Topics();
        List<BlogTopic> SubTopics(int id);
        bool AddTopic(AddTopic topic);
        List<Tag> FetchTags(int topicId);
        List<Blog> RenderPost(int topicId);
        VMBlogDescription FetchBlogDescription(int blogId);
        void SaveBlogsComments(VMSaveBlogComments details, string name);
        bool DeleteBlogComments(int commentId, string email);
        VMSearchedBlog Search(string tag);
        bool Likes(string email, int blogId);
        bool DisLike(string email, int blogId);
        bool Spam(string email, int blogId);
        VMBlog RenderBlog(string email);
    }
}