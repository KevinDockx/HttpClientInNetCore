using Microsoft.EntityFrameworkCore;
using Movies.API.Contexts;
using Movies.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.API.Services
{
    public class MoviesRepository : IMoviesRepository, IDisposable
    {
        private MoviesContext _context;

        public MoviesRepository(MoviesContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Movie> GetMovieAsync(Guid movieId)
        {
            return await _context.Movies.Include(m => m.Director)
                .FirstOrDefaultAsync(m => m.Id == movieId);
        } 

        public async Task<IEnumerable<Movie>> GetMoviesAsync()
        {
            return await _context.Movies.Include(m => m.Director).ToListAsync();
        }

        public void UpdateMovie(Movie movieToUpdate)
        {
            // no code required, entity tracked by context.  Including 
            // this is best practice to ensure other implementations of the 
            // contract (eg a mock version) can execute code on update
            // when needed.
        }

        public void AddMovie(Movie movieToAdd)
        {
            if (movieToAdd == null)
            {
                throw new ArgumentNullException(nameof(movieToAdd));
            }

            _context.Add(movieToAdd);
        }

        public void DeleteMovie(Movie movieToDelete)
        {
            if (movieToDelete == null)
            {
                throw new ArgumentNullException(nameof(movieToDelete));
            }

            _context.Remove(movieToDelete);
        }

        public async Task<bool> SaveChangesAsync()
        {
            // return true if 1 or more entities were changed
            return (await _context.SaveChangesAsync() > 0);
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
