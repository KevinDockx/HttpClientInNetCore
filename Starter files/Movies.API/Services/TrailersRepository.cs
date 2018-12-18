using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Movies.API.Contexts;
using Movies.API.InternalModels;

namespace Movies.API.Services
{
    public class TrailersRepository : ITrailersRepository, IDisposable
    {
        private MoviesContext _context;

        public TrailersRepository(MoviesContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

        }

        public async Task<Trailer> GetTrailerAsync(Guid movieId, Guid trailerId)
        {
            // Generate the name from the movie title.
            var movie = await _context.Movies
             .FirstOrDefaultAsync(m => m.Id == movieId);

            if (movie == null)
            {
                throw new Exception($"Movie with id {movieId} not found.");
            }

            // generate a trailer (byte array) between 50 and 100MB
            var random = new Random();
            var generatedByteLength = random.Next(52428800, 104857600);
            var generatedBytes = new byte[generatedByteLength];
            random.NextBytes(generatedBytes);

            return new Trailer()
            {
                Bytes = generatedBytes,
                Id = trailerId,
                MovieId = movieId,
                Name = $"{movie.Title} trailer number {DateTime.UtcNow.Ticks}",
                Description = $"{movie.Title} trailer description {DateTime.UtcNow.Ticks}"
            };
        }

        public async Task<Trailer> AddTrailer(Guid movieId, Trailer trailerToAdd)
        {
            // don't do anything: we're just faking this.  Simply return the trailer
            // after setting the ids
            trailerToAdd.MovieId = movieId;
            trailerToAdd.Id = Guid.NewGuid();
            return await Task.FromResult(trailerToAdd);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                    _context = null;
                }
            }
        }
    }
}
