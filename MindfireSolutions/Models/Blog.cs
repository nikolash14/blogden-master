using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MindfireSolutions.Models
{
    /// <summary>
    /// Model Class Blog for Database Table
    /// </summary>
    [Table("Blogs", Schema = "BlogDen")]
    public class Blog
    {
        [Key]
        public int BlogId { get; set; }

        [MaxLength]
        public string Title { get; set; }

        [MaxLength]
        public string Description { get; set; }

        [MaxLength(40)]
        public string CreatedBy { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreationTime { get; set; }
        [MaxLength(40)]
        public string EditedBy { get; set; }

        [DataType(DataType.Date)]
        public DateTime LastEditTime { get; set; }
        public int BlogStatus { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        [ForeignKey("BlogTopic")]
        public int TopicId { get; set; }
        public BlogTopic BlogTopic { get; set; }

    }
}