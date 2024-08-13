﻿using System.ComponentModel.DataAnnotations;

namespace MoviesApi.Models;

public class Movie
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Movie title is required")]
    public string? Title { get; set; }
    [Required(ErrorMessage = "Movie genre is required")]
    [MaxLength(10, ErrorMessage = "Genre must have a maximum of 50 characters")]
    public string? Genero { get; set; }
    [Required]
    [Range(1985, 2024, ErrorMessage = "Movie year must be between 1985 and 2024")]
    public int Year { get; set; }
}