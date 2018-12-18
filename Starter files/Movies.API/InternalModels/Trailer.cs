using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.API.InternalModels
{
    public class Trailer
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public Guid MovieId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        [Required]
        public byte[] Bytes { get; set; }
    }
}
