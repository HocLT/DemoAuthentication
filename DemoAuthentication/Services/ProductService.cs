using DemoAuthentication.Data;
using DemoAuthentication.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoAuthentication.Services
{
    public class ProductService : IProductService
    {
        readonly AuthenticationDbContext ctx;

        public ProductService(AuthenticationDbContext ctx)
        {
            this.ctx = ctx;
        }

        public async Task<List<Product>> GetProducts()
        {
            return await ctx.Products.ToListAsync();
        }

        public async Task<Product?> GetProduct(int id)
        {
            return await ctx.Products.SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Product>> SearchPrice(int min, int max)
        {
            return await ctx.Products.Where(p=>p.Price >= min && p.Price <= max).ToListAsync();

            //var results = from p in ctx.Products
            //              where p.Price >= min && p.Price <= max
            //              select p;
            //return await results.ToListAsync();
        }

        public async Task<bool> Create(Product p)
        {
            await ctx.Products.AddAsync(p);
            return await ctx.SaveChangesAsync() > 0;
        }

        public async Task<bool> Update(Product p)
        {
            ctx.Attach(p).State = EntityState.Modified;
            return await ctx.SaveChangesAsync() > 0;
        }

        public async Task<bool> Delete(int id)
        {
            Product? p = await ctx.Products.SingleOrDefaultAsync(p => p.Id == id);
            if (p != null)
            {
                ctx.Products.Remove(p);
                return await ctx.SaveChangesAsync() > 0;
            }

            return false;
        }
    }
}
