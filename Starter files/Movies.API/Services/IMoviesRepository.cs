using Movies.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.API.Services
{
    public interface IMoviesRepository
    {
        Task<Movie> GetMovieAsync(Guid movieId);

        Task<IEnumerable<Movie>> GetMoviesAsync();

        void UpdateMovie(Movie movieToUpdate);

        void AddMovie(Movie movieToAdd);

        void DeleteMovie(Movie movieToDelete);

        Task<bool> SaveChangesAsync();
    }
}
