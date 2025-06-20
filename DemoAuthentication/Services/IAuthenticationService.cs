using DemoAuthentication.Dto;
using DemoAuthentication.Models;

namespace DemoAuthentication.Services
{
    public interface IAuthenticationService
    {
        Task<bool> RegisterAccount(RegisterDto dto);

        Task<Account?> Login(LoginDto dto);
    }
}
