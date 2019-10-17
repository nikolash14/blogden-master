using MindfireSolutions.Models;
using System.Collections.Generic;

namespace MindfireSolutions.ViewModel
{
    public class VMBloggerDetails
    {
        public List<Blog> Blogs { get; set; }
        public User User { get; set; }
        public BlogStatusCount Stats { get; set; }
    }
}