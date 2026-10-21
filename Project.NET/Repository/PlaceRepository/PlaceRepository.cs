using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Project.NET.Data;
using Project.NET.Models;

namespace Project.NET.Repository.PlaceRepository
{
    public class PlaceRepository : IPlaceRepository
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly string _connectionString;
        public PlaceRepository(ApplicationDBContext dbContext, IConfiguration config)
        {
            _dbContext = dbContext;
            _connectionString = config.GetConnectionString("DefaultConnection");
        }
        public async Task AddPlace(PlaceModel place)
        {
            try
            {
                _dbContext.Places.Add(place);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<List<CategoryModel>> GetAllCategories()
        {
            return await _dbContext.Categories.ToListAsync();
        }
        public async Task<List<PlaceModel>> GetAllPlaces()
        {
            return await _dbContext.Places
                .Include(p => p.Category)
                .Include(p => p.Images)
                .Include (p => p.User)
                .ToListAsync();
        }

        public async Task<PlaceModel> GetPlaceById(int id)
        {
            return await _dbContext.Places
                .Include(p => p.Category)
                .Include(p => p.Images)
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        //ADO.NET Search + Filter
        public List<PlaceModel> SearchPlaces(string search, int? categoryId)
        {
            var places = new List<PlaceModel>();

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand();
            command.Connection = connection;

            var sql = @"
                    SELECT p.Id, p.Name, p.City, p.Country, p.FeaturesSerialized, p.UserId,
                           c.Name AS CategoryName, pi.ImageUrl
                    FROM Places p
                    JOIN Categories c ON p.CategoryId = c.Id
                    LEFT JOIN (
                        SELECT PlaceId, ImageUrl FROM PlaceImages WHERE SortOrder = 1
                    ) pi ON pi.PlaceId = p.Id
                    WHERE 1=1
                    ";


            // Add filters
            if (!string.IsNullOrWhiteSpace(search))
            {
                sql += " AND (p.Name LIKE @Search OR p.City LIKE @Search)";
                command.Parameters.AddWithValue("@Search", "%" + search + "%");
            }

            if (categoryId.HasValue)
            {
                sql += " AND p.CategoryId = @CategoryId";
                command.Parameters.AddWithValue("@CategoryId", categoryId.Value);
            }
            command.CommandText = sql;
            connection.Open();

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                places.Add(new PlaceModel
                {
                    Id = (int)reader["Id"],
                    Name = reader["Name"].ToString(),
                    City = reader["City"].ToString(),
                    Country = reader["Country"].ToString(),
                    UserId = reader["UserId"]?.ToString(),
                    FeaturesSerialized = reader["FeaturesSerialized"]?.ToString() ?? "",
                    Category = new CategoryModel
                    {
                        Name = reader["CategoryName"].ToString()
                    },
                    Images = new List<PlaceImageModel>
            {
                new PlaceImageModel
                {
                    ImageUrl = reader["ImageUrl"]?.ToString() ?? "/images/no-image.png"
                }
            }
                });
            }

            return places;
        }
        public async Task UpdatePlace(PlaceModel place)
        {
            _dbContext.Places.Update(place);
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeletePlace(int id)
        {
            // Load place with images
            var place = await _dbContext.Places
                .Include(p => p.Images)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (place == null)
                return;

            // Delete image files from disk
            foreach (var image in place.Images)
            {
                if (!string.IsNullOrEmpty(image.ImageUrl))
                {
                    // Convert URL to physical path
                    var filePath = Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "wwwroot",
                        image.ImageUrl.TrimStart('/')
                    );

                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                }
            }

            // Remove place (DB cascade deletes PlaceImages)
            _dbContext.Places.Remove(place);
            await _dbContext.SaveChangesAsync();
        }




    }
}
