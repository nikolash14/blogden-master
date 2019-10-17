using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MindfireSolutions.ViewModel
{
    public class VMSaveBlogComments
    {
        public int BlogId { get; set; }
        public int? ParentId { get; set; }
        public string Comments { get; set; }
    }
}