using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Project.NET.Data;
using Project.NET.Models;

namespace Project.NET.Repository.SavedPlacesRepository
{
    public class SavedPlacesRepository : ISavedPlacesRepository

    {
        private readonly ApplicationDBContext _dbContext;
        public SavedPlacesRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddToSavedPlaces(SavedPlacesModel savedPlaces)
        {
            try
            {
                _dbContext.SavedPlaces.Add(savedPlaces);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public async Task RemoveSavedPlace(int placeId, string userId)
        {
            var saved = await _dbContext.SavedPlaces
                .FirstOrDefaultAsync(sp => sp.PlaceId == placeId && sp.UserId == userId);

            if (saved != null)
            {
                _dbContext.SavedPlaces.Remove(saved);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<bool> IsPlaceSaved(int placeId, string userId)
        {
            return await _dbContext.SavedPlaces
                .AnyAsync(sp => sp.PlaceId == placeId && sp.UserId == userId);
        }
        //Gets a list of place IDs that the specified user has saved
        public async Task<List<int>> GetSavedPlaceIds(string userId)
        {
            return await _dbContext.SavedPlaces
                .Where(sp => sp.UserId == userId)
                .Select(sp => sp.PlaceId)
                .ToListAsync();
        }


    }
}
