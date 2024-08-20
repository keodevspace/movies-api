using Microsoft.AspNetCore.Mvc;
using MoviesApi.Models;
using MoviesApi.Services;

namespace MoviesApi.Controllers
    {
    [ApiController]
    [Route("[controller]")] //https://localhost:PORT/Movie
    public class MovieController : ControllerBase
        {
        private readonly MovieService _movieService;

        public MovieController(MovieService movieService)
            {
            _movieService = movieService;
            }

        [HttpPost("v1/add")]
        public IActionResult AddMovie([FromBody] Movie movie)
            {
            _movieService.AddMovie(movie);
            return Ok(movie);
            }

        [HttpPost("v1/add-multiple")]
        public IActionResult AddMultipleMovies([FromBody] IEnumerable<Movie> movies)
            {
            _movieService.AddMovies(movies);
            return Ok(movies);
            }

        [HttpGet("v1/list")] //https://localhost:PORT/Movie/v1/list?pageNumber=1&pageSize=10
        public IActionResult GetMoviesList([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)   {
            var movies = _movieService.GetMovies(pageNumber, pageSize);
            return Ok(movies);
            }

        [HttpGet("v1/{id}")]
        public IActionResult GetMovieById(int id)
            {
            var movie = _movieService.GetMovieById(id);
            if (movie == null)
                {
                return NotFound();
                }
            return Ok(movie);
            }
        }
    }
