using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MoviesApi.Data;
using MoviesApi.Models;

namespace MoviesApi.Services
    {
    public class MovieService
        {
        private readonly MoviesContext _context;

        public MovieService(MoviesContext context)
            {
            _context = context;
            }

        public async Task<List<Movie>> GetMoviesAsync(int pageNumber, int pageSize)
            {
            return await _context.Movies
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            }

        public void AddMovie(Movie movie)
            {
            _context.Movies.Add(movie);
            _context.SaveChanges();
            }

        public void AddMovies(IEnumerable<Movie> movies)
            {
            _context.Movies.AddRange(movies);
            _context.SaveChanges();
            }

        public Movie? GetMovieById(int id)
            {
            return _context.Movies.Find(id);
            }
        }
    }
