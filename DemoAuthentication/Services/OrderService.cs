using DemoAuthentication.Data;
using DemoAuthentication.Dto;
using DemoAuthentication.Models;

namespace DemoAuthentication.Services
{
    public class OrderService: IOrderService
    {
        readonly AuthenticationDbContext ctx;

        public OrderService(AuthenticationDbContext ctx)
        {
            this.ctx = ctx;
        }

        public async Task CreateOrder(List<CartItemDto> items)
        {
            Order ord = new Order();
            ord.AccountId = 1;
            ord.OrderAt = DateTime.Now;
            List<OrderDetail> details = new List<OrderDetail>();
            foreach (var item in items)
            {
                OrderDetail detail = new OrderDetail { Order = ord, Product = item.Product, Price = item.Product!.Price, Quantity = item.Quantity };
                details.Add(detail);
            }
            ord.OrderDetails = details;
            await ctx.Orders.AddAsync(ord);
            await ctx.SaveChangesAsync();
        }
    }
}
