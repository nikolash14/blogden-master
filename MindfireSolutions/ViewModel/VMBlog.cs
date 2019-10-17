using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace MindfireSolutions.ViewModel
{
    public class VMBlog
    {
        [Required(ErrorMessage ="*Please Enter Blog Title")]
        [MaxLength]
        [MinLength(2, ErrorMessage = "*Minimum length should be 2")]
        public string Title { get; set; }
        public string Tag { get; set; }
        [DataType(DataType.Upload)]
        public HttpPostedFileBase BlogImageUpload { get; set; }

        public string UserImage { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; }
        public HttpPostedFileBase Image { get; set; }
        public int SubTopic { get; set; }

    }
}