using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MindfireSolutions.ViewModel
{
    public class VMAuthor
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string UserImage { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string DOB { get; set; }
        public string Gender { get; set; }
        public string Description { get; set; }
        public List<string> Company { get; set; }
        public List<string> Skills { get; set; }
        public int Rank { get; set; }

    }
}