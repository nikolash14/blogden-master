using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MindfireSolutions.Models
{
    [Table("UserSkills",Schema = "BlogDen")]
    public class UserSkill
    {
        [Key]
        public int UserSkillId { get; set; }
        [MaxLength(60)]
        public string Company { get; set; }
        [MaxLength(90)]
        public string skill { get; set; }
        [ForeignKey("GetUserSkills")]
        public int UserId { get; set; }
        public User GetUserSkills { get; set; }

    }
}