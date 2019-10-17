using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MindfireSolutions.ViewModel
{
    public class VMEditBlogData
    {
        public int UserId { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public string TagTitle { get; set; }
        public int BlogId { get; set; }
        public int TagId { get; set; }
    }
}