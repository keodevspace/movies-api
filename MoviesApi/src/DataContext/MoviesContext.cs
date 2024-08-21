using Microsoft.EntityFrameworkCore;
using MoviesApi.Models;

namespace MoviesApi.Data
    {
    public class MoviesContext : DbContext
        {
        public MoviesContext(DbContextOptions<MoviesContext> options) : base(options) { }

        public DbSet<Movie?> Movies { get; set; }

        }
    }

