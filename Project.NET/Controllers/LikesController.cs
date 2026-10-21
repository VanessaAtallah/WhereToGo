using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project.NET.Models;
using Project.NET.Service.PlaceLikesService.PlaceLikesService;

namespace Project.NET.Controllers
{
 
    public class LikesController : Controller
    {
        private readonly IPlaceLikesService _likedplaceservice;
        private readonly UserManager<IdentityUser> _userManager;
        public LikesController(IPlaceLikesService likeplaceservice, UserManager<IdentityUser> userManager)
        {
            _likedplaceservice = likeplaceservice;
            _userManager = userManager;
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Toggle(int placeId)
        {
            var userId = _userManager.GetUserId(User);
            if (await _likedplaceservice.IsLiked(placeId, userId))
            {
                await _likedplaceservice.RemoveLike(placeId, userId);   
            }
            else
            {
                await _likedplaceservice.AddLike(new PlaceLikesModel
                {
                    PlaceId = placeId,
                    UserId = userId
                });

            }
            return RedirectToAction("Index", "Home");
        }


    }
}
