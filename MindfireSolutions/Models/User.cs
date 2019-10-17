using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MindfireSolutions.Models
{
    [Table("Users", Schema = "BlogDen")]
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(60)]
        public string Password { get; set; }
        [MaxLength(15)]
        public string Mobile { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreationTime { get; set; }
        
        public DateTime DateOfBirth { get; set; }
        [MaxLength]
        public string ImageUrl { get; set; }
        [MaxLength(15)]
        public string Gender { get; set; }
        [MaxLength(160)]
        public string Description { get; set; }
        public int Rank { get; set; }

    }
}