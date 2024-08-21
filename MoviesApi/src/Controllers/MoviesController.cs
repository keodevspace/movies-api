using Microsoft.AspNetCore.Mvc;
using MoviesApi.Models;
using MoviesApi.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoviesApi.Controllers
    {
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
        {
        private readonly MovieService _movieService;

        public MoviesController(MovieService movieService)
            {
            _movieService = movieService;
            }

        [HttpGet]
        public async Task<ActionResult<List<Movie>>> GetMovies(int pageNumber = 1, int pageSize = 10)
            {
            var movies = await _movieService.GetMoviesAsync(pageNumber, pageSize);
            return Ok(movies);
            }

        [HttpPost]
        public ActionResult AddMovie(Movie movie)
            {
            _movieService.AddMovie(movie);
            return Ok();
            }

        [HttpPost("add-multiple")]
        public ActionResult AddMultipleMovies(IEnumerable<Movie> movies)
            {
            _movieService.AddMovies(movies);
            return Ok();
            }

        [HttpGet("{id}")]
        public ActionResult<Movie> GetMovieById(int id)
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
