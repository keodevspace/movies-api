using MoviesApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace MoviesApi.Services
    {
    public class MovieService
        {
        private readonly List<Movie> movies = new List<Movie>();
        private static int id = 0;

        public IEnumerable<Movie> GetMovies(int pageNumber, int pageSize)
            {
            return movies.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            }

        public Movie? GetMovieById(int id)
            {
            return movies.FirstOrDefault(movie => movie.Id == id);
            }

        public void AddMovie(Movie movie)
            {
            movie.Id = id++;
            movies.Add(movie);
            }

        public void AddMovies(IEnumerable<Movie> moviesToAdd)
            {
            foreach (var movie in moviesToAdd)
                {
                movie.Id = id++;
                movies.Add(movie);
                }
            }
        }
    }
