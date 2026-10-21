using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.NET.Data;
using Project.NET.Repository.PlaceLikesRepository;
using Project.NET.Repository.PlaceRepository;
using Project.NET.Repository.SavedPlacesRepository;
using Project.NET.Repository.UserRepository;
using Project.NET.Service.PlaceLikesServ;
using Project.NET.Service.PlaceLikesService.PlaceLikesService;
using Project.NET.Service.PlaceServ;
using Project.NET.Service.PlaceService.PlaceService;
using Project.NET.Service.SavedPlacesServ;
using Project.NET.Service.SavedPlacesService.SavedPlacesService;
using Project.NET.Service.UserServ;
using Project.NET.Service.UserService.UserService;

var builder = WebApplication.CreateBuilder(args);

// Validate the antiforgery token on every POST/PUT/DELETE automatically
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
});
builder.Services.AddHttpClient();
builder.Services.AddScoped<IPlaceService, PlaceService>();
builder.Services.AddScoped<IPlaceRepository, PlaceRepository>();
builder.Services.AddScoped<ISavedPlacesRepository, SavedPlacesRepository>();
builder.Services.AddScoped<ISavedPlacesService, SavedPlacesService>();
builder.Services.AddScoped<IPlaceLikesRepository, PlaceLikesRepository>();
builder.Services.AddScoped<IPlaceLikesService, PlaceLikesService>();

// Configure database
builder.Services.AddDbContext<ApplicationDBContext>(options
    => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    // Password rules (match the hint on the Register page)
    options.Password.RequiredLength = 8;
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = false;

    // Lockout after repeated failed logins
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.AllowedForNewUsers = true;

    // Unique emails
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<ApplicationDBContext>()
.AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";               // Redirect here if not logged in
    options.AccessDeniedPath = "/Account/AccessDenied"; // Redirect here if forbidden
    options.ExpireTimeSpan = TimeSpan.FromHours(2);     // Idle timeout
    options.SlidingExpiration = true;
});

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();