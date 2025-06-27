using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoAuthentication.Models
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public int? AccountId { get; set; }

        public DateTime OrderAt { get; set; }

        [ForeignKey(nameof(AccountId))]
        public Account? Account { get; set; }

        public List<OrderDetail>? OrderDetails { get; set; }
    }
}
