using DemoAuthentication.Models;

namespace DemoAuthentication.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetProducts();

        Task<Product?> GetProduct(int id);

        Task<List<Product>> SearchPrice(int min, int max);

        Task<bool> Create(Product p);

        Task<bool> Update(Product p);

        Task<bool> Delete(int id);
    }
}
