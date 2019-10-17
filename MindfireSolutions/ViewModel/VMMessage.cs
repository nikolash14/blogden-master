using System.ComponentModel.DataAnnotations;

namespace MindfireSolutions.ViewModel
{
    public class VMMessage
    {
        [Required(ErrorMessage ="*Please Enter your Name")]
        [MinLength(2, ErrorMessage = "*Minimum length should be 2")]
        [MaxLength(30,ErrorMessage ="Maximum Length should be 30")]
        [RegularExpression("^[a-zA-Z ]+$", ErrorMessage = "*Only Alphabets are allowed")]
        public string VisitorName { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "*Please Enter your EmailId")]
        public string VisitorEmail { get; set; }

        [Required(ErrorMessage = "*Please Enter your Comments")]
        [MaxLength]
        public string VisitorComment { get; set; }
    }
}