using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MindfireSolutions.Models
{
    [Table("Responses", Schema = "BlogDen")]
    public class Response
    {
        [Key]
        public int ResponseId { get; set; }

        [ForeignKey("Status")]
        public int StatusId { get; set; }
        public Status Status { get; set; }
        [ForeignKey("Blog")]
        public int? BlogId { get; set; }
        public virtual Blog Blog { get; set; }
        [ForeignKey("User")]
        public int? UserId { get; set; }
        public virtual User User { get; set; }
    }
}