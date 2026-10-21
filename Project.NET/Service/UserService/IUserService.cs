using Microsoft.AspNetCore.Identity;

namespace Project.NET.Service.UserService.UserService
{
    public interface IUserService
    {
        //Registers a new user using email and password
        // Returns IdentityResult to indicate success or failure
        Task<IdentityResult> RegisterUser(string email, string password);
        // Logs in a user using email and password
        Task<SignInResult> LoginUser(string email, string password, bool rememberMe);
        Task LogoutUser();
    }

}

