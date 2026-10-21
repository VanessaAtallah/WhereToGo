using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Project.NET.Models
{
    public class PlaceLikesModel
    {
        public int Id { get; set; }

        public int PlaceId { get; set; }
        public PlaceModel Place { get; set; }

        public string UserId { get; set; }
        public IdentityUser User { get; set; }
    }

}
