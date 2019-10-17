using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MindfireSolutions.ViewModel
{
    public class VMPostBlog
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string[] Tag { get; set; }
        public HttpPostedFileBase Image { get; set; }
        public int UserId { get; set; }
        public int SubTopic { get; set; }
    }
}