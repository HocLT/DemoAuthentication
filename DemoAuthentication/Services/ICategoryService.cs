using DemoAuthentication.Models;

namespace DemoAuthentication.Services
{
    public interface ICategoryService
    {
        Task<List<Category>> GetCategories();

        Task<Category?> GetCategory(int id);
    }
}
