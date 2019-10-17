using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MindfireSolutions.Models
{
    /// <summary>
    /// Model Class BlogStatusCount for Database Table
    /// </summary>
    [Table("BlogStatusCounts", Schema = "BlogDen")]
    public class BlogStatusCount
    {    
        [Key]
        public int BlogCountId { get; set; }
        public int LikesCount { get; set; }
        public int DislikesCount { get; set; }
        public int SpamCount { get; set; }
        public int CommentsCount { get; set; }
        [ForeignKey("GetBlogs")]
        public int BlogId { get; set; }
        public Blog GetBlogs { get; set; }
        public int UserId { get; set; }

    }
}