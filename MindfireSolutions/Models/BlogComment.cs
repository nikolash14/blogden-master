using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MindfireSolutions.Models
{
    [Table("BlogComments", Schema = "BlogDen")]
    public class BlogComment
    {
        [Key]
        public int CommentId { get; set; }
        [MaxLength]
        public string Comments { get; set; }


        [MaxLength(40)]
        public string CreatedBy { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreationTime { get; set; }
        [MaxLength(40)]
        public string EditedBy { get; set; }

        [DataType(DataType.Date)]
        public DateTime LastEditTime { get; set; }
        public int? ParentId { get; set; }
        [ForeignKey("ParentId")]
        public virtual List<BlogComment> SubComment { get; set; }


        [ForeignKey("Blog")]
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
    }
}