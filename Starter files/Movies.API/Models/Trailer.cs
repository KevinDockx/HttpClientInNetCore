using System;
using System.Collections.Generic;
using System.Text;

namespace Movies.API.Models
{
    public class Trailer
    {
        public Guid Id { get; set; }
        public Guid MovieId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Bytes { get; set; }
    }
}
