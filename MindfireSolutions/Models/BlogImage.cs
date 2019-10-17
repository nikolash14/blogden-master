using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MindfireSolutions.Models
{
    [Table("BlogImages", Schema = "BlogDen")]
    public class BlogImage
    {
        [Key]
        public int BlogImageId { get; set; }
        [MaxLength]
        public string ImgageUrl { get; set; }
        [ForeignKey("Blog")]
        public int BlogId { get; set; }
        public Blog Blog { get; set; }

    }
}