using Project.NET.Models;

namespace Project.NET.Service.PlaceLikesService.PlaceLikesService
{
    public interface IPlaceLikesService
    {
        Task AddLike(PlaceLikesModel like);
        Task RemoveLike(int placeId, string userId);
        Task<bool> IsLiked(int placeId, string userId);
        Task<List<int>> GetLikedPlaceIds(string userId);
        Task<int> GetLikesCount(int placeId);
        Task<Dictionary<int, int>> GetLikesCountsForPlaces(List<int> placeIds);


    }
}
