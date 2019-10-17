using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MindfireSolutions.Models
{
    [Table("UserImages", Schema = "BlogDen")]
    public class UserImage
    {
        [Key]
        public int UserImageId { get; set; }
        [MaxLength(60)]
        public string Title { get; set; }

        [MaxLength]
        public string ImageURL { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreationTime { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}