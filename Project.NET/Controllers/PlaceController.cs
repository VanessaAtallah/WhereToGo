using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project.NET.Models;
using Project.NET.Service.PlaceService.PlaceService;

namespace Project.NET.Controllers
{
    
    public class PlaceController : Controller
    {
        private readonly IPlaceService _placeService;
        private readonly UserManager<IdentityUser> _userManager;

        public PlaceController(IPlaceService placeService, UserManager<IdentityUser> userManager)
        {
            _placeService = placeService;
            _userManager = userManager;
        }
        private List<string> GetFeatures() => new()
        {
            "Family-Friendly","Romantic","Quiet","Wi-Fi","Parking",
            "Pets Allowed","Guided Tours","Good for studying",
            "Outdoor seating","Wheelchair accessible"
        };

        private List<string> GetDaysOfWeek() => new()
        {
            "Monday","Tuesday","Wednesday","Thursday","Friday","Saturday","Sunday"
        };
        // GET: Create: display Add Place form
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _placeService.GetAllCategories();
            ViewBag.Features = GetFeatures();
            ViewBag.DaysOfWeek = GetDaysOfWeek();

            return View("AddNewPlaceForm", new PlaceModel());
        }

        // POST: Create: Handle form submission
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
                PlaceModel place,
                string[] Features,
                string[] OpeningDays,
                List<IFormFile> Images) 
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return RedirectToAction("Login", "Account");

            place.UserId = user.Id;
            place.FeaturesSerialized = string.Join(",", Features ?? Array.Empty<string>());
            place.OpeningDaysSerialized = string.Join(",", OpeningDays ?? Array.Empty<string>());
            
            //Validate the model
            if (!ModelState.IsValid)
            {
                // Reload dropdowns and checkboxes in case of validation errors
                ViewBag.Categories = await _placeService.GetAllCategories();
                ViewBag.Features = GetFeatures();
                ViewBag.DaysOfWeek = GetDaysOfWeek();
                return View("AddNewPlaceForm", place);
            }

            // Handle uploaded images
            if (Images != null && Images.Count > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "places");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                int sortOrder = 1; // 1st image is the cover image

                foreach (var file in Images)
                {
                    if (file.Length == 0) continue;
                    // Generate unique file name
                    var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                    var filePath = Path.Combine(uploadsFolder, fileName);
                    Console.WriteLine("Saving image to: " + filePath);
                    // Save the image to disk
                    using var stream = new FileStream(filePath, FileMode.Create);
                    await file.CopyToAsync(stream);
                    // Add image info to PlaceModel
                    place.Images.Add(new PlaceImageModel
                    {
                        ImageUrl = "/images/places/" + fileName,
                        IsCover = sortOrder == 1,
                        SortOrder = sortOrder
                    });

                    sortOrder++;
                }
            }

            await _placeService.AddPlace(place);

            return RedirectToAction("Index", "Home");
        }


        // GET: Edit: Display edit form
        public async Task<IActionResult> Edit(int id)
        {
            var place = await _placeService.GetPlaceById(id); //Get place from db
            if (place == null) return NotFound();

            var user = await _userManager.GetUserAsync(User);
            if (place.UserId != user.Id) return Forbid(); // Only owner can edit

            ViewBag.Categories = await _placeService.GetAllCategories();
            ViewBag.Features = GetFeatures();
            ViewBag.DaysOfWeek = GetDaysOfWeek();

            return View("EditPlace", place); 
        }

        // POST: Edit: handle form submission
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            PlaceModel model,
            string[] Features,
            string[] OpeningDays,
            List<IFormFile> Images) 
        {
            var place = await _placeService.GetPlaceById(model.Id); //load existing place
            if (place == null) return NotFound();

            var user = await _userManager.GetUserAsync(User);
            if (place.UserId != user.Id) return Forbid();

            // Update fields
            place.Name = model.Name;
            place.City = model.City;
            place.Country = model.Country;
            place.Description = model.Description;
            place.CategoryId = model.CategoryId;
            place.PriceRange = model.PriceRange;
            place.FreeEntry = model.FreeEntry;
            place.OpenAllYear = model.OpenAllYear;
            place.Contact = model.Contact;
            place.MapLocation = model.MapLocation;
            place.Website = model.Website;
            place.Instagram = model.Instagram;
            place.Facebook = model.Facebook;
            place.FeaturesSerialized = string.Join(",", Features ?? Array.Empty<string>());
            place.OpeningDaysSerialized = string.Join(",", OpeningDays ?? Array.Empty<string>());

            // Handle new uploaded images
            if (Images != null && Images.Count > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "places");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                int sortOrder = place.Images.Count + 1; //continue sorting after existing images

                foreach (var file in Images)
                {
                    if (file.Length == 0) continue;

                    var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                    var filePath = Path.Combine(uploadsFolder, fileName);

                    using var stream = new FileStream(filePath, FileMode.Create);
                    await file.CopyToAsync(stream);

                    place.Images.Add(new PlaceImageModel
                    {
                        ImageUrl = "/images/places/" + fileName,
                        IsCover = sortOrder == 1,
                        SortOrder = sortOrder
                    });

                    sortOrder++;
                }
            }

            await _placeService.UpdatePlace(place);
            return RedirectToAction("Index", "Home");
        }


        // DELETE: Delete Place
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var place = await _placeService.GetPlaceById(id);
            var user = await _userManager.GetUserAsync(User);
            if (place == null) return NotFound();
            if (place.UserId != user.Id) return Forbid();


            await _placeService.DeletePlace(id);
            return RedirectToAction("Index", "Home");
        }
        // GET: Details: show place details
        public async Task<IActionResult> Details(int id)
        {
            var place = await _placeService.GetPlaceById(id);
            if (place == null)
                return NotFound();

            return View(place);
        }


        
    }
}
