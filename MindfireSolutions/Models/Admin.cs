using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MindfireSolutions.Models
{
    [Table("Admin", Schema = "BlogDen")]
    public class Admin
    { 
        public int Id { get; set; }
        [MaxLength(25)]
        public string Username { get; set; }
        [MaxLength(60)]
        public string Password { get; set; }
    }
}