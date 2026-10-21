using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.NET.Models;
using Project.NET.Service.UserService.UserService;

namespace Project.NET.Controllers
{


    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
        //Register
        [HttpGet]
        public IActionResult Register()
        {
            if(User.Identity?.IsAuthenticated == true)
                return RedirectToAction("Index",  "Home");
            return View(new RegisterViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var result = await _userService.RegisterUser(model.Email, model.Password);

            if (result.Succeeded)
                return RedirectToAction("Index", "Home");

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return View(model);
        }

        // Login 
        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            if (User.Identity?.IsAuthenticated == true)
                return RedirectToAction("Index", "Home");
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var result = await _userService.LoginUser(model.Email, model.Password, false);

            if (result.Succeeded)
            {
                // Only redirect to local URLs to prevent open-redirect attacks
                if(!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    return Redirect(model.ReturnUrl);
                return RedirectToAction("Index", "Home");
            }
            if (result.IsLockedOut)
            {
                ModelState.AddModelError("", "Too many failed attempts. Your account is locked, please try again later.");
                return View(model);
            }
            ModelState.AddModelError("", "Invalid login attempt");
            return View(model);
        }
        // Logout
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _userService.LogoutUser();
            return RedirectToAction("Login");

        }
        //Access Denied
        [HttpGet]
        public IActionResult AccessDenied() => View();

    }
}