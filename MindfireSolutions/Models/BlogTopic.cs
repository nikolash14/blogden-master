using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MindfireSolutions.Models
{
    [Table("BlogTopics", Schema = "BlogDen")]
    public class BlogTopic
    {
        [Key]
        public int TopicId { get; set; }
        [MaxLength]
        public string TopicName { get; set; }
        public int? ParentId { get; set; }
        [ForeignKey("ParentId")]
        public virtual List<BlogTopic> SubTopic { get; set; }
    }
}