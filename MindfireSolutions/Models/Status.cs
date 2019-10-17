using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MindfireSolutions.Models
{
    [Table("Status", Schema = "BlogDen")]
    public class Status
    {
        [Key]
        public int StausId { get; set; }
        [MaxLength(20)]
        public string StatusName { get; set; }
    }
}