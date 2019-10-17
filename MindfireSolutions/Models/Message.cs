using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MindfireSolutions.Models
{
    [Table("Messages", Schema = "BlogDen")]
    public class Message
    {
        [Key]
        public int MessageId { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        [MaxLength]
        public string Comment { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreationTime { get; set; }
    }
}