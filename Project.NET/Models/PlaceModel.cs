using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Project.NET.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class PlaceModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Place name is required")]
    [StringLength(100, ErrorMessage = "Place name cannot exceed 100 characters")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Country is required")]
    [StringLength(50, ErrorMessage = "Country cannot exceed 50 characters")]
    public string Country { get; set; }

    [Required(ErrorMessage = "City is required")]
    [StringLength(50, ErrorMessage = "City cannot exceed 50 characters")]
    public string City { get; set; }

    [Required(ErrorMessage = "Description is required")]
    [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
    public string Description { get; set; }

    [Required(ErrorMessage = "Category is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Please select a valid category")]
    public int? CategoryId { get; set; } 

    [ValidateNever]
    public CategoryModel Category { get; set; }

    [ValidateNever]
    public string UserId { get; set; }
    [ValidateNever]
    public IdentityUser User { get; set; }

    public ICollection<PlaceImageModel> Images { get; set; } = new List<PlaceImageModel>();

    
    [Url(ErrorMessage = "Please enter a valid Instagram URL")]
    [StringLength(200)]
    public string? Instagram { get; set; }

    
    [Url(ErrorMessage = "Please enter a valid Facebook URL")]
    [StringLength(200)]
    public string? Facebook { get; set; }

    
    [Url(ErrorMessage = "Please enter a valid website URL")]
    [StringLength(200)]
    public string? Website { get; set; }

    [Required(ErrorMessage = "Price range is required")]
    public string PriceRange { get; set; }

    [ValidateNever]
    public string FeaturesSerialized { get; set; }

    public bool FreeEntry { get; set; }

    public bool OpenAllYear { get; set; }

    [ValidateNever]
    public string OpeningDaysSerialized { get; set; }

    [Required(ErrorMessage = "Contact information is required")]
    [StringLength(20, ErrorMessage = "Contact info cannot exceed 20 characters")]
    [RegularExpression(@"^\+?[0-9]{7,15}$", ErrorMessage = "Enter a valid phone number")]
    public string Contact { get; set; }


    [Required(ErrorMessage = "Map location is required")]
    [StringLength(200, ErrorMessage = "Map location cannot exceed 200 characters")]
    public string MapLocation { get; set; }
   
    public ICollection<PlaceLikesModel> Likes { get; set; } = new List<PlaceLikesModel>();
    public ICollection<SavedPlacesModel> SavedByUsers { get; set; } = new List<SavedPlacesModel>();
}
