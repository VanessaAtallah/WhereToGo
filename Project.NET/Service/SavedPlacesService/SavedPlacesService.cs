using Microsoft.EntityFrameworkCore;
using Project.NET.Data;
using Project.NET.Models;
using Project.NET.Repository.SavedPlacesRepository;
using Project.NET.Service.SavedPlacesService.SavedPlacesService;

namespace Project.NET.Service.SavedPlacesServ
{
    public class SavedPlacesService : ISavedPlacesService
    {
        private readonly ISavedPlacesRepository _savedPlacesRepository;
        public SavedPlacesService(ISavedPlacesRepository savedPlacesRepository)
        {
            _savedPlacesRepository = savedPlacesRepository;
          
        }
        public async Task AddToSavedPlaces(SavedPlacesModel savedPlaces)
        {
            await _savedPlacesRepository.AddToSavedPlaces(savedPlaces);
        }
        public async Task RemoveSavedPlace(int placeId, string userId)
        {
            await _savedPlacesRepository.RemoveSavedPlace(placeId, userId);
        }
        public async Task<bool> IsPlaceSaved(int placeId, string userId)
        {
            return await _savedPlacesRepository.IsPlaceSaved(placeId, userId);
        }
        public async Task<List<int>> GetSavedPlaceIds(string userId)
        {
            return await _savedPlacesRepository.GetSavedPlaceIds(userId);
        }


    }
}