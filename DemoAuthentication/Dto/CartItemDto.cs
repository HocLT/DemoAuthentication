using DemoAuthentication.Models;

namespace DemoAuthentication.Dto
{
    public class CartItemDto
    {
        public Product? Product { get; set; }
        public int Quantity { get; set; }
    }
}
