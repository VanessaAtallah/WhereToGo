# WhereToGo
WhereToGo is a web application that allows users to share places they have visited, enabling others to discover, like, and save them for future visits. The platform focuses on easy exploration through search and filtering.

## Features

### User Authentication
Users can sign up and sign in securely to access personalized features.

### Browse Places
View all added places on the Home page, including basic details and the number of likes for each place.

### Search & Filter
Search places by name or city and filter them by category. Both can be applied at the same time.

### Place Details
View detailed information about a place on a dedicated page.

### Add, Edit & Delete Places
Users can add new places with validated input. Only the user who added a place can edit or delete it.

### Like & Save Places
Users can like places and save them for future visits.

### My Saved Places
Users can view all places they have saved in a dedicated Saved page.

## Authentication
Authentication is implemented using ASP.NET Core Identity with cookie-based authentication.

- Users can register and log in using email and password.
- Passwords are securely hashed and managed by ASP.NET Core Identity.
- User accounts are stored in the AspNetUsers table in the database.

## Add New Place – Validation
The Add New Place feature uses Data Annotations to enforce server-side validation and ensure data integrity.

## Database Tables & Relationships
The project uses SQL Server with ASP.NET Core Identity. Main tables:

- **AspNetUsers:** Stores all registered users.
- **Categories:** Stores place categories (e.g., Museum, Park, Restaurant). Pre-filled using seed data.
- **Places:** Stores places added by users, including details, social links, contact info, and map location.
- **PlaceImages:** Stores images for each place. Each place can have multiple images.
- **SavedPlaces:** Tracks which users saved which places to visit later.
- **PlaceLikes:** Tracks which users liked which places.

### Relationships Overview
- Each Place belongs to one Category.
- Each Place is added by one User.
- Each Place can have multiple images, likes, and saved entries.

## Technologies Used
- **Backend:** ASP.NET MVC
- **Authentication:** ASP.NET Core Identity (Cookie-based authentication)
- **Data Access:** Entity Framework Core and ADO.NET
- **Database:** SQL Server
- **IDE:** Visual Studio

## Project Structure
- **Controllers/:** Handle HTTP requests and routing
- **Models/:** Application data models
- **Views/:** Razor views (UI)
- **Services/:** Business logic layer
- **Repository/:** Data access layer (EF Core & ADO.NET)
- **ApplicationDbContext.cs:** Entity Framework Core database context

## Getting Started

### Prerequisites
- Visual Studio
- SQL Server

### Running the Project
1. Clone the repository.
2. Open the solution in Visual Studio.
3. Ensure the SQL Server connection string is correctly configured in appsettings.json.
4. Run the project using HTTP.

## Additional Features (Planned)
- Add feedback/comments for places and display them to other users.
- Rate places and filter places by rating.
- Mark places as visited.

## Notes
- This project uses both Entity Framework Core and ADO.NET for data access.
- The application follows a layered architecture for better maintainability and scalability.
