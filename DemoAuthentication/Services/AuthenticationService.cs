using DemoAuthentication.Data;
using DemoAuthentication.Dto;
using DemoAuthentication.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoAuthentication.Services
{
    public class AuthenticationService: IAuthenticationService
    {
        readonly AuthenticationDbContext ctx;

        public AuthenticationService(AuthenticationDbContext ctx)
        {
            this.ctx = ctx;
        }

        public async Task<bool> RegisterAccount(RegisterDto dto)
        {
            Account newAccount = new Account { Email = dto.Email, Password = dto.Password, Role = "user" };
            await ctx.Accounts.AddAsync(newAccount);
            return await ctx.SaveChangesAsync() > 0;
        }

        public async Task<Account?> Login(LoginDto dto)
        {
            Account? result = null;
            Account? existedAccount = await ctx.Accounts.SingleOrDefaultAsync(o=>o.Email==dto.Email);
            if (existedAccount != null)
            {
                if (existedAccount.Password == dto.Password)
                {
                    result = existedAccount;
                }
            }
            return result;
        }
    }
}
