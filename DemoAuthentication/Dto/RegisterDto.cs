using System.ComponentModel.DataAnnotations;

namespace DemoAuthentication.Dto
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        [MinLength(3)]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string? Confirm {  get; set; }
    }
}
