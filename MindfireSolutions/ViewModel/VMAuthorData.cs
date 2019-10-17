using MindfireSolutions.Models;
using System.Collections.Generic;

namespace MindfireSolutions.ViewModel
{
    public class VMAuthorData
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string UserImage { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public List<Blog> Blog { get; set; }
        public int PublishedPostCount { get; set; }
        public int LikeCount { get; set; }
        public int DislikeCount { get; set; }
        public int CommentCount { get; set; }
        public int SpamCount { get; set; }
        public List<Blog> Archived { get; set; }
        public List<Blog> Drafted { get; set; }
    }
}