using Microsoft.AspNetCore.Identity;
using Project.NET.Repository.UserRepository;
using Project.NET.Service.UserService.UserService;

namespace Project.NET.Service.UserServ
{
    public class UserService : IUserService
    {
     
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            
        }
        // Registers a new user
        public async Task<IdentityResult> RegisterUser(string email, string password)
        {
            var user = new IdentityUser { UserName = email, Email = email };
            var result = await _userRepository.CreateUser(user, password);

            if (result.Succeeded)
            {
                await _userRepository.SignIn(user);
            }

            return result;
        }

        //Logs in an existing user
        public async Task<SignInResult> LoginUser(string email, string password, bool rememberMe)
        {
            return await _userRepository.PasswordSignIn(email, password, rememberMe);

        }
    
        public async Task LogoutUser()
        {
            await _userRepository.SignOut();
        }
    }
}
