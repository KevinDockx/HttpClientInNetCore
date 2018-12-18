using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Movies.API.Models
{
    public class MovieForUpdate
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [MaxLength(2000)]
        public string Description { get; set; }

        [MaxLength(200)]
        public string Genre { get; set; }

        [Required]
        public DateTimeOffset ReleaseDate { get; set; }

        [Required]
        public Guid DirectorId { get; set; }
    }
}
