using Project.NET.Models;
using Project.NET.Repository.PlaceRepository;
using Project.NET.Service.PlaceService.PlaceService;

namespace Project.NET.Service.PlaceServ
{
    public class PlaceService : IPlaceService
    {
        private readonly IPlaceRepository _placeRepository;
        public PlaceService(IPlaceRepository placeRepository)
        {
            _placeRepository = placeRepository;
        }
        public async Task AddPlace(PlaceModel place)
        {
            await _placeRepository.AddPlace(place);
        }
        public async Task<List<CategoryModel>> GetAllCategories()
        {
            return await _placeRepository.GetAllCategories();
        }
        public List<PlaceModel> SearchPlaces(string search, int? categoryId)
        {
            return _placeRepository.SearchPlaces(search, categoryId);
        }

        public async Task<List<PlaceModel>> GetAllPlaces()
        {
            return await _placeRepository.GetAllPlaces();
        }
        public async Task<PlaceModel> GetPlaceById(int id)
        {
            return await _placeRepository.GetPlaceById(id);
        }
        public async Task UpdatePlace(PlaceModel place)
        {
            await _placeRepository.UpdatePlace(place);
        }
        public async Task DeletePlace(int id)
        {
            await _placeRepository.DeletePlace(id);

        }
    }
}
