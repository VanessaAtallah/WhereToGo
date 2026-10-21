using Project.NET.Models;

namespace Project.NET.Repository.SavedPlacesRepository
{
    public interface ISavedPlacesRepository
    {
        Task AddToSavedPlaces(SavedPlacesModel savedPlaces);
        Task RemoveSavedPlace(int placeId, string userId);
        Task<bool> IsPlaceSaved(int  placeId, string userId);
        Task<List<int>> GetSavedPlaceIds(string userId);
    }
}
