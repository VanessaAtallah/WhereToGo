using Project.NET.Models;

namespace Project.NET.Service.SavedPlacesService.SavedPlacesService
{
    public interface ISavedPlacesService
    {
        Task AddToSavedPlaces(SavedPlacesModel savedPlaces);
        Task RemoveSavedPlace(int placeId, string userId);
        Task<bool> IsPlaceSaved(int placeId, string userId);
        Task<List<int>> GetSavedPlaceIds(string userId);


    }
}
