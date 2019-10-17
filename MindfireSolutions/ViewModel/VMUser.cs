using MindfireSolutions.CustomAttribute;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace MindfireSolutions.ViewModel
{
    public class VMUser
    {
        [Required(ErrorMessage = "*Please Enter your Name")]
        [MinLength(2, ErrorMessage = "*Minimum length should be 2")]
        [MaxLength(30, ErrorMessage = "*Maxlength should be 30")]
        [RegularExpression("^[a-zA-Z ]+$", ErrorMessage = "*Only Alphabets are allowed")]
        public string Name { get; set; }

        [Required(ErrorMessage = "*Please Enter your EmailId")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "*Please Enter your Password ")]
        [RegularExpression("^(?=.*[0-9])(?=.*[A-Z])(?=.*[!@#$%^&*])[a-zA-Z0-9!@#$%^&*]{8,}$", ErrorMessage = "Password should contain atleast one uppercase+lowercase+digit+special character respectively")]
        public string Password { get; set; }

        [Required(ErrorMessage = "*Please Enter your Password ")]
        [Compare("Password", ErrorMessage = "*Passwords do not match.")]
        public string ConfirmPassword { get; set; }

        [FileFormat]
        [FileSize(512000)]
        public HttpPostedFileBase ImageUpload { get; set; }

        [MaxLength(15, ErrorMessage = "*Maxlength should be 15")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Not a valid phone number")]
        public string Mobile { get; set; }

    }
}