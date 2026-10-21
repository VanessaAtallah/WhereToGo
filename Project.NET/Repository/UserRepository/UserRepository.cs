using Microsoft.AspNetCore.Identity;

namespace Project.NET.Repository.UserRepository
{
    public class UserRepository : IUserRepository
        
    {
        //UserManager is responsible for creating and managing users
        private readonly UserManager<IdentityUser> _userManager;
        // SignInManager handles user authentication (sign in / sign out)
        private readonly SignInManager<IdentityUser> _signInManager;
        
        public UserRepository(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            
        }
        // Creates a new user with the given password
        public async Task<IdentityResult> CreateUser(IdentityUser user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }
        //Signs in a user using email and password
        //rememberMe: keeps the user logged in if true
        public async Task<SignInResult> PasswordSignIn(string email, string password,bool rememberMe)
        {
            return await _signInManager.PasswordSignInAsync(email, password, rememberMe, lockoutOnFailure: true); 
        }
        public async Task SignIn(IdentityUser user)
        {
            await _signInManager.SignInAsync(user, isPersistent: false);  // user stays logged in only for this session (no remember-me cookie)
        }
        public async Task SignOut()
        {
            await _signInManager.SignOutAsync();
        }


    }
}
