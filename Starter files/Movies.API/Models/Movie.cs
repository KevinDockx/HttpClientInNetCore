using System;
using System.ComponentModel.DataAnnotations;

namespace Movies.API.Models
{
    public class Movie
    {     
        public Guid Id { get; set; } 
        public string Title { get; set; }
        public string Description { get; set; }
        public string Genre { get; set; }
        public DateTimeOffset ReleaseDate { get; set; }
        public string Director { get; set; } 
    }
}
