using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Movies.API.Models
{
    public class PosterForCreation
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required]
        public byte[] Bytes { get; set; }
    }
}
