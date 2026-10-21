using Project.NET.Models;

namespace Project.NET.Service.PlaceService.PlaceService
{
    public interface IPlaceService
    {
        Task<List<PlaceModel>> GetAllPlaces();
        Task<PlaceModel> GetPlaceById(int id);
        Task AddPlace(PlaceModel place);
        Task<List<CategoryModel>> GetAllCategories();
        List<PlaceModel> SearchPlaces(string search, int? categoryId);
        Task UpdatePlace(PlaceModel place);
        Task DeletePlace(int id);


    }
}
