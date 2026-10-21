using Project.NET.Models;

namespace Project.NET.Repository.PlaceLikesRepository
{
    public interface IPlaceLikesRepository
    {
        Task AddLike(PlaceLikesModel like);
        Task RemoveLike(int placeId, string userId);
        Task<bool> IsLiked(int placeId, string userId);
        Task<List<int>> GetLikedPlaceIds(string userId);
        Task<int> GetLikesCount(int placeId);
    }
}
