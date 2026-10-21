using Microsoft.AspNetCore.Identity;

namespace Project.NET.Repository.UserRepository
{
    public interface IUserRepository
    {
        //Creates a new user and hashes the password before saving to AspNetUsers
        Task<IdentityResult> CreateUser(IdentityUser user, string password);
        //Checks user credentials and signs the user in (creates auth cookie)
        Task<SignInResult> PasswordSignIn(string email, string password, bool rememberMe);
        Task SignIn(IdentityUser user);
        Task SignOut();


    }
}
