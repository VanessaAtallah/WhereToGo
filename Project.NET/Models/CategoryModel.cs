using System.ComponentModel.DataAnnotations;

namespace Project.NET.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public List<PlaceModel> Places { get; set; }
    }
}
