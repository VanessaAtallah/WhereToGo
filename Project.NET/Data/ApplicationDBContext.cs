using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project.NET.Models;

namespace Project.NET.Data
{
    public class ApplicationDBContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
            : base(options)
        {
        }

        public DbSet<PlaceModel> Places { get; set; }
        public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<PlaceImageModel> PlaceImages { get; set; }
        public DbSet<SavedPlacesModel> SavedPlaces { get; set; }
        public DbSet<PlaceLikesModel> PlaceLikes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Place - Category: 
           // Each Place has One Category - Each Category can have Many Places
            builder.Entity<PlaceModel>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Places)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            // Place - User: 
            // Each User can create Many Places
            builder.Entity<PlaceModel>()
                .HasOne(p => p.User) 
                .WithMany() 
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict); 


            // PlaceImage - Place:
            //Each PlaceImage belongs to One Place
            // Each Place can have Many PlaceImages
            builder.Entity<PlaceImageModel>()
                .HasOne(pi => pi.Place)
                .WithMany(p => p.Images)
                .HasForeignKey(pi => pi.PlaceId) 
                .OnDelete(DeleteBehavior.Cascade);

            // Seed initial Category data
            builder.Entity<CategoryModel>().HasData(
                new CategoryModel { Id = 1, Name = "Museum" },
                new CategoryModel { Id = 2, Name = "Park" },
                new CategoryModel { Id = 3, Name = "Restaurant" },
                new CategoryModel { Id = 4, Name = "Historical Site" },
                new CategoryModel { Id = 5, Name = "Cafe" },
                new CategoryModel { Id = 6, Name = "Theater" },
                new CategoryModel { Id = 7, Name = "Art Gallery" },
                new CategoryModel { Id = 8, Name = "Zoo" },
                new CategoryModel { Id = 9, Name = "Beach" },
                new CategoryModel { Id = 10, Name = "Shopping Center" },
                new CategoryModel { Id = 11, Name = "Hiking Trail" },
                new CategoryModel { Id = 12, Name = "Waterfall" },
                new CategoryModel { Id = 13, Name = "Nature Reserve" },
                new CategoryModel { Id = 14, Name = "Mountain" },
                new CategoryModel { Id = 15, Name = "Picnic Area" },
                new CategoryModel { Id = 16, Name = "Hotel" },
                new CategoryModel { Id = 17, Name = "Other" }
);


            // SavedPlaces
            // Each SavedPlaces belongs to One Place
            // Each Place can be saved by Many Users
            builder.Entity<SavedPlacesModel>()
                .HasOne(sp => sp.Place)
                .WithMany(p => p.SavedByUsers)
                .HasForeignKey(sp => sp.PlaceId)
                .OnDelete(DeleteBehavior.Cascade);
            //Each SavedPlaces belongs to One User
            //Each User can save Many Places
            builder.Entity<SavedPlacesModel>()
                .HasOne(sp => sp.User)
                .WithMany()
                .HasForeignKey(sp => sp.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            // Ensure a user can save a place Only once
            builder.Entity<SavedPlacesModel>()
                .HasIndex(sp => new { sp.PlaceId, sp.UserId })
                .IsUnique();

            // PlaceLikes
            // Each Like belongs to One Place
            // Each Place can have many likes
            builder.Entity<PlaceLikesModel>()
                .HasOne(pl => pl.Place)
                .WithMany(p => p.Likes)
                .HasForeignKey(pl => pl.PlaceId)
                .OnDelete(DeleteBehavior.Cascade);
            // Each Like belongs to one user
            //Each User can like many places
            builder.Entity<PlaceLikesModel>()
                .HasOne(pl => pl.User)
                .WithMany()
                .HasForeignKey(pl => pl.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            // Ensure a user can like a place only once
            builder.Entity<PlaceLikesModel>()
                .HasIndex(pl => new { pl.PlaceId, pl.UserId })
                .IsUnique();


        }
    }
}
