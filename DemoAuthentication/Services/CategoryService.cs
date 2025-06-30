using DemoAuthentication.Data;
using DemoAuthentication.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoAuthentication.Services
{
    public class CategoryService: ICategoryService
    {
        readonly AuthenticationDbContext ctx;

        public CategoryService(AuthenticationDbContext ctx)
        {
            this.ctx = ctx;
        }

        public async Task<List<Category>> GetCategories()
        {
            return await ctx.Categories.ToListAsync();
        }

        public async Task<Category?> GetCategory(int id)
        {
            return await ctx.Categories.SingleOrDefaultAsync(o=>o.Id==id);
        }
    }
}
