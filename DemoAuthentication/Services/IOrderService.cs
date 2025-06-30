using DemoAuthentication.Dto;

namespace DemoAuthentication.Services
{
    public interface IOrderService
    {
        Task CreateOrder(List<CartItemDto> items);
    }
}
