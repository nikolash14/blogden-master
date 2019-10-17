using MindfireSolutions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MindfireSolutions.ViewModel
{
    public class VMAllBlog
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string UserImage { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public List<Blog> Blogs { get; set; }
    }
}