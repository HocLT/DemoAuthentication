using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoAuthentication.Models
{
    public class OrderDetail
    {
        [Key]
        public int Id { get; set; }

        public int? OrderId { get; set; }
        public int? ProductId { get; set; }

        public int Price { get; set; }
        public int Quantity { get; set; }

        [ForeignKey(nameof(OrderId))]
        public Order? Order { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product? Product { get; set; }
    }
}
