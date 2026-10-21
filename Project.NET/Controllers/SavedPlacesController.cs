using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.NET.Data;
using Project.NET.Models;
using Project.NET.Service.SavedPlacesService.SavedPlacesService;


namespace Project.NET.Controllers
{
 
    public class SavedPlacesController : Controller
    {
        private readonly ISavedPlacesService _savedPlacesService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDBContext _dbContext;
        public SavedPlacesController(
            ISavedPlacesService savedPlacesService,
            UserManager<IdentityUser> userManager,
            ApplicationDBContext dbContext)
        {
            _savedPlacesService = savedPlacesService;
            _userManager = userManager;
            _dbContext = dbContext;
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Toggle(int placeId)
        {
            var userId = _userManager.GetUserId(User);

            if (await _savedPlacesService.IsPlaceSaved(placeId, userId))
            {
                // If it's already saved, remove it (unsave)
                await _savedPlacesService.RemoveSavedPlace(placeId, userId);
            }
            else
            {
                // If it's not saved, add it (save)
                await _savedPlacesService.AddToSavedPlaces(new SavedPlacesModel
                {
                    PlaceId = placeId,
                    UserId = userId
                });
            }

            // Redirect back to Home page
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public async Task<IActionResult> MySavedPlaces()
        {
            var userId = _userManager.GetUserId(User);
            // Get a list of Place IDs that the user has saved
            var savedPlaceIds = await _savedPlacesService.GetSavedPlaceIds(userId);
            // Load the full PlaceModel objects for those saved ids
            var savedPlaces = await _dbContext.Places
                .Where(p => savedPlaceIds.Contains(p.Id))
                .Include(p => p.Images)
                .Include(p => p.Category)
                .ToListAsync();
            return View(savedPlaces);
        }



    }
}