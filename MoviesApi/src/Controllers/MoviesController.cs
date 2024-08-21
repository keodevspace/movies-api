using Microsoft.AspNetCore.Mvc;
using MoviesApi.Models;
using MoviesApi.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoviesApi.Controllers
    {
    [ApiController]
    [Route("[controller]")]
    public class MoviesController : ControllerBase
        {
        private readonly MovieService _movieService;

        public MoviesController(MovieService movieService)
            {
            _movieService = movieService;
            }

        [HttpGet("v1/list")]
        public async Task<ActionResult<List<Movie>>> GetMovies(int pageNumber = 1, int pageSize = 10)
            {
            var movies = await _movieService.GetMoviesAsync(pageNumber, pageSize);
            return Ok(movies);
            }

        [HttpGet("v1/{id}")]
        public ActionResult<Movie> GetMovieById(int id)
            {
            var movie = _movieService.GetMovieById(id);
            if (movie == null)
                {
                return NotFound();
                }
            return Ok(movie);
            }

        [HttpPost("v1/add-single")]
        public ActionResult AddMovie(Movie movie)
            {
            _movieService.AddMovie(movie);
            return Ok();
            }

        [HttpPost("v1/add-multiple")]
        public ActionResult AddMultipleMovies(IEnumerable<Movie> movies)
            {
            _movieService.AddMovies(movies);
            return Ok();
            }

        [HttpPut("v1/update/{id}")]
        public IActionResult UpdateMovie(int id, Movie movie)
            {
            var existingMovie = _movieService.GetMovieById(id);
            if (existingMovie == null)
                {
                return NotFound();
                }
            _movieService.UpdateMovie(id, movie);
            return Ok();
            }

        [HttpDelete("v1/delete/{id}")]
        public IActionResult DeleteMovieById(int id)
            {
            _movieService.DeleteMovieById(id);
            return Ok();
            }

        [HttpDelete("v1/delete-multiple")]
        public IActionResult DeleteMultipleMovies()
            {
            _movieService.DeleteMultipleMovies();
            return Ok();
            }
        }
    }
