using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.Data.SqlClient;
using Project.NET.Models;

namespace Project.NET.Repository.PlaceLikesRepository
{
    public class PlaceLikesRepository : IPlaceLikesRepository
    {
        private readonly string _connectionString;
        public PlaceLikesRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");

        }
        // Adds a like for a place by a specific user
        public async Task AddLike(PlaceLikesModel like)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            var command = new SqlCommand("INSERT INTO PlaceLikes(PlaceId, UserId) VALUES (@PlaceId, @UserId)", connection);
            command.Parameters.AddWithValue("@PlaceId", like.PlaceId);
            command.Parameters.AddWithValue("@UserId", like.UserId);
            await command.ExecuteNonQueryAsync();
        }
        // Removes a user's like from a specific place
        public async Task RemoveLike(int placeId, string userId)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            var command = new SqlCommand("DELETE FROM PlaceLikes WHERE PlaceId=@PlaceId AND UserId=@UserId", connection);
            command.Parameters.AddWithValue("@PlaceId", placeId);
            command.Parameters.AddWithValue("@UserId", userId);
            await command.ExecuteNonQueryAsync();
        }
        // Checks if a user has already liked a specific place
        public async Task<bool> IsLiked(int placeId, string userId)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            var command = new SqlCommand("SELECT COUNT(*) FROM PlaceLikes WHERE PlaceId=@PlaceId AND UserId=@UserId", connection);
            command.Parameters.AddWithValue("@PlaceId", placeId);
            command.Parameters.AddWithValue("@UserId", userId);
            var count = (int)await command.ExecuteScalarAsync();
            return count > 0;
        }
        // Gets all place IDs that the user has liked
        public async Task<List<int>> GetLikedPlaceIds(string userId)
        {
            var list = new List<int>();
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            var command = new SqlCommand("SELECT PlaceId FROM PlaceLikes WHERE UserId=@UserId", connection);
            command.Parameters.AddWithValue("@UserId", userId);
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync()) 
            {
                list.Add(reader.GetInt32(0));
            }
            return list; 
        }
        // Returns the total number of likes for a specific place
        public async Task<int> GetLikesCount(int placeId)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            var command = new SqlCommand("SELECT COUNT(*) FROM PlaceLikes WHERE PlaceId=@PlaceId", connection);
            command.Parameters.AddWithValue("@PlaceId", placeId);
            var count = (int)await command.ExecuteScalarAsync();
            return count;
        }


    }
}
