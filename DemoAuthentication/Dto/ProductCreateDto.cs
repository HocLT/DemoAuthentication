using System.ComponentModel.DataAnnotations;

namespace DemoAuthentication.Dto
{
    public class ProductCreateDto
    {
        [Required]
        [MinLength(3)]
        public string? Name { get; set; }
        [DataType(DataType.MultilineText)]
        public string? Description { get; set; }
        [Range(0, int.MaxValue)]
        public int Price { get; set; }
        public IFormFile? Photo { get; set; }
    }
}
