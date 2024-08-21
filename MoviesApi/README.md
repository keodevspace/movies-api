# Movies Api
This is a simple RESTful API that allows you to manage movies and their categories.

## Swagger Endpoint
The API is available at the following endpoint:
https://localhost:5001/swagger/MoviesApi/swagger.json

## Installation
1. Clone the repository
2. Run 'dotnet build' in the root directory
3. Run 'dotnet run' in the root directory
4. The API will be available at swagger endpoint
5. You can use the Postman collection to test the API
6. Install SQLite extension in IDE to view the database movies.db

## Technologies
- .NET 8
- ASP.NET Core
- Entity Framework Core
- Dependency Injection
- SOLID Principles
- SQLite
- Swagger
- xUnit
- Moq

## Endpoints
- GET /api/movies/list
- GET /api/movies/{id}
- POST /api/movies/add
- POST /api/movies/add-multiple
- PUT /api/movies/{id}
- DELETE /api/movies/{id}
- DELETE /api/movies/delete-multiple

## Code Structure
- MoviesApi
  - Program.cs
  - README.md
  - src
	- Controllers
		-  MovieController.cs
	- Database
		- movie.db
		- movieslist.txt
		- movies.db-shm
		- movies.db-wal
	- DataContext
		- MoviesContext.cs 
	- Migrations
	- Models
		- ApiResponse.cs
		- Movie.cs
	- Services
		- MovieServices.cs

## Database
The database is stored in movies.db file. It contains a single table called Movies with the following columns:
- Id (int)
- Title (string)
- Genre (string)
- Year (int)

## Credits
This project was created by [Keo Coelho](https://github.com/keodevspace) for the purpose of learning and practicing .NET Core.