using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoAuthentication.Models
{
    public class Account
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(200)")]
        public string? Email { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string? Password { get; set; }
        [Column(TypeName = "varchar(20)")]
        public string? Role { get; set; }   // admin, user
    }
}
