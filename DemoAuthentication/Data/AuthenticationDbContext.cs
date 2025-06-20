using DemoAuthentication.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoAuthentication.Data
{
    public class AuthenticationDbContext : DbContext
    {
        public AuthenticationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }
    }
}
