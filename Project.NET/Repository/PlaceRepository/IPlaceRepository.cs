using Project.NET.Models;

namespace Project.NET.Repository.PlaceRepository
{
    public interface IPlaceRepository
    {
        Task AddPlace(PlaceModel place);
        Task<List<PlaceModel>> GetAllPlaces();
        Task<PlaceModel> GetPlaceById(int id);
        Task<List<CategoryModel>> GetAllCategories();
        List<PlaceModel> SearchPlaces(string search, int? categoryId);
        Task UpdatePlace(PlaceModel place);
        Task DeletePlace(int id);
    }
}
