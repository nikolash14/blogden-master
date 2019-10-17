using System.ComponentModel.DataAnnotations;

namespace MindfireSolutions.ViewModel
{
    public class VMDraftBlog
    {
        [MinLength(5)]
        [MaxLength(100)]
        public string BlogTitle { get; set; }

        [MinLength(10)]
        [MaxLength]
        public string BlogContent { get; set; }
        public int UserId { get; set; }
        public int SubTopic { get; set; }
    }
}