using MindfireSolutions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MindfireSolutions.ViewModel
{
    public class VMEditBlog
    {
    
        public int BlogId { get; set; }
        public string BlogTitle { get; set; }
        public string BlogDescription { get; set; }
        public int TagId { get; set; }
        public string TagTitle { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string UserImage { get; set; }
        public string Email { get; set; }
    }
}