using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.NET.Models
{
    public class PlaceImageModel
    {
        public int Id { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public bool IsCover { get; set; } = false;

        public int SortOrder { get; set; } = 1;

        // Foreign key to Place
        public int PlaceId { get; set; }
        public PlaceModel Place { get; set; }
    }
}
