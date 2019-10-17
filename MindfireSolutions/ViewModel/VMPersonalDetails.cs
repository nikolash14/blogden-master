using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MindfireSolutions.ViewModel
{
    public class VMPersonalDetails
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string UserImage { get; set; }
        public DateTime DOB { get; set; }
        public string Gender { get; set; }
        public string Description { get; set; }
        public string[] Company { get; set; }
        public string[] Skills { get; set; }
    }
}