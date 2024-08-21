using Microsoft.EntityFrameworkCore;
using MoviesApi.Models;

namespace MoviesApi.DataContext
    {
    public class MoviesContext : DbContext
        {
        public MoviesContext(DbContextOptions<MoviesContext> options) : base(options) { }

        public DbSet<Movie> Movies { get; set; } = null!; 
        }
    }
