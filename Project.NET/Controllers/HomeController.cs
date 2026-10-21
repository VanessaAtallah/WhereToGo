using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project.NET.Models;
using Project.NET.Service.PlaceLikesService.PlaceLikesService;
using Project.NET.Service.PlaceService.PlaceService;
using Project.NET.Service.SavedPlacesService.SavedPlacesService;
using System.Diagnostics;

namespace Project.NET.Controllers
{
  
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPlaceService _placeService;
        private readonly ISavedPlacesService _savedPlacesService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IPlaceLikesService _likedPlacesService;

        public HomeController(
            ILogger<HomeController> logger,
            IPlaceService placeService,
            ISavedPlacesService savedPlacesService,
            IPlaceLikesService likedPlacesService,
            UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _placeService = placeService;
            _savedPlacesService = savedPlacesService;
            _userManager = userManager;
            _likedPlacesService = likedPlacesService;
        }

        // Home page with search/filter
        public async Task<IActionResult> Index(string search, int? categoryId)
        {
            // Categories for filter dropdown
            ViewBag.Categories = await _placeService.GetAllCategories();

            // Get filtered places
            var places = _placeService.SearchPlaces(search, categoryId);

            // Current user ID
            var userId = _userManager.GetUserId(User);

            // Saved Places
            ViewBag.SavedPlaceIds = string.IsNullOrEmpty(userId)
                ? new List<int>()
                : await _savedPlacesService.GetSavedPlaceIds(userId);

            // Liked Places
            ViewBag.LikedPlaceIds = string.IsNullOrEmpty(userId)
                ? new List<int>()
                : await _likedPlacesService.GetLikedPlaceIds(userId);

            //Likes Count
            // Extract the IDs of the currently displayed places
            var placeIds = places.Select(p => p.Id).ToList();
            ViewBag.LikesCounts = await _likedPlacesService.GetLikesCountsForPlaces(placeIds);

            return View(places);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}
