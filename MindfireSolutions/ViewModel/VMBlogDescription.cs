using MindfireSolutions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MindfireSolutions.ViewModel
{
    public class VMBlogDescription
    {
        public Blog Blogs { get; set; }
        public User Users { get; set; }
        public int LikesCount { get; set; }
        public int DislikesCount { get; set; }
        public int SpamCount { get; set; }
        public List<BlogComment> Blogcomments { get; set; }
        public List<Response> Responses { get; set; }
        public UserImage BloggerImage { get; set; }
        public Status statuses { get; set; }
        public int BloggerRank { get; set; }
        public string BloggerDescription { get; set; }
        public int BloggeUserId { get; set; }
    }
}