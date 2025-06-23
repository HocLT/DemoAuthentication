using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoAuthentication.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(200)")]
        public string? Name { get; set; }
        public string? Description { get; set; }
        [Range(0, int.MaxValue)]
        public int Price { get; set; }
        [Column(TypeName = "nvarchar(200)")]
        public string? Image {  get; set; }
    }
}
