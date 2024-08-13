using Microsoft.AspNetCore.Mvc;
using MoviesApi.Models;

namespace MoviesApi.Controllers;

[ApiController]
[Route("[controller]")] // https://localhost:7284/Movie
public class MovieController : ControllerBase
{

    private static List<Movie> movies = new List<Movie>();
    private static int id = 1;

    [HttpPost("v1/add")]
    public void AddMovie([FromBody] Movie movie)
    {
        movie.Id = id++;
        movies.Add(movie);
        Console.WriteLine(movie.Title);
        Console.WriteLine(movie.Year);
    }

    [HttpGet("v1/list")]
    public IEnumerable<Movie> GetMoviesList()
    {
        return movies;
    }

    [HttpGet("v1/{id}")]
    public Movie? GetMovieById(int id)
    {
        return movies.FirstOrDefault(movie => movie.Id == id);
    }
}
   