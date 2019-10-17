using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MindfireSolutions.ViewModel
{
    public class VMUserLogin
    {
        [Required(ErrorMessage = "*Please Enter your Email-Id ")]
        public string EmailId { get; set; }

        [Required(ErrorMessage = "*Please Enter your Password ")]
        public string PasswordValue { get; set; }
    }
}