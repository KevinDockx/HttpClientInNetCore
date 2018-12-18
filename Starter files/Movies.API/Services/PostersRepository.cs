using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Movies.API.Contexts;
using Movies.API.InternalModels;

namespace Movies.API.Services
{
    public class PostersRepository : IPostersRepository, IDisposable
    {
        private MoviesContext _context;

        public PostersRepository(MoviesContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

        }

        public async Task<Poster> GetPosterAsync(Guid movieId, Guid posterId)
        {
            // Generate the name from the movie title.
            var movie = await _context.Movies
             .FirstOrDefaultAsync(m => m.Id == movieId);

            if (movie == null)
            {
                throw new Exception($"Movie with id {movieId} not found.");
            }

            // generate a movie poster of 500KB
            var random = new Random();
            var generatedBytes = new byte[524288];
            random.NextBytes(generatedBytes);

            return new Poster()
            {
                Bytes = generatedBytes,
                Id = posterId,
                MovieId = movieId,
                Name = $"{movie.Title} poster number {DateTime.UtcNow.Ticks}"
            };
        }

        public async Task<Poster> AddPoster(Guid movieId, Poster posterToAdd)
        {
            // don't do anything: we're just faking this.  Simply return the poster
            // after setting the ids
            posterToAdd.MovieId = movieId;
            posterToAdd.Id = Guid.NewGuid();
            return await Task.FromResult(posterToAdd);
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
