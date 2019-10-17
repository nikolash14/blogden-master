using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MindfireSolutions.Models
{
    [Table("Tags", Schema = "BlogDen")]
    public class Tag
    {
        [Key]
        public int TagId { get; set; }
        [MaxLength]
        public string TagTitle { get; set; }

        [ForeignKey("Blog")]
        public int? BlogId { get; set; }
        public virtual Blog Blog { get; set; }

        [ForeignKey("BlogTopic")]
        public int? TopicId { get; set; }

        public virtual BlogTopic BlogTopic { get; set; }
    }
}


