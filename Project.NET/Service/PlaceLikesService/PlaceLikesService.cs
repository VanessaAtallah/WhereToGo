using Project.NET.Models;
using Project.NET.Repository.PlaceLikesRepository;
using Project.NET.Service.PlaceLikesService.PlaceLikesService;

namespace Project.NET.Service.PlaceLikesServ
{
    public class PlaceLikesService : IPlaceLikesService
    {
        private readonly IPlaceLikesRepository _placeLikesRepository;
        public PlaceLikesService(IPlaceLikesRepository placeLikesRepository)
        {
            _placeLikesRepository = placeLikesRepository;
        }
        public async Task AddLike(PlaceLikesModel like)
        {
            await _placeLikesRepository.AddLike(like);
        }
        public async Task RemoveLike(int placeId, string userId)
        {
            await _placeLikesRepository.RemoveLike(placeId, userId);
        }
        public async Task<bool> IsLiked(int placeId, string userId)
        {
            return await _placeLikesRepository.IsLiked(placeId, userId);
        }
        public async Task<List<int>> GetLikedPlaceIds(string userId)
        {
            return await _placeLikesRepository.GetLikedPlaceIds(userId);
        }
        public async Task<int> GetLikesCount(int placeId)
        {
            return await _placeLikesRepository.GetLikesCount(placeId);
        }
        public async Task<Dictionary<int, int>> GetLikesCountsForPlaces(List<int> placeIds)
        {
            var dict = new Dictionary<int, int>();
            foreach (var placeId in placeIds)
            {
                dict[placeId] = await _placeLikesRepository.GetLikesCount(placeId);
            }
            return dict;
        }

    }
}
