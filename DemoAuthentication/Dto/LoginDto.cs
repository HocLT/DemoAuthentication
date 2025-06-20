using System.ComponentModel.DataAnnotations;

namespace DemoAuthentication.Dto
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        [MinLength(3)]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
