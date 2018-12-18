using Movies.API.InternalModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.API.Services
{
    public interface ITrailersRepository
    {
        Task<Trailer> GetTrailerAsync(Guid movieId, Guid trailerId);

        Task<Trailer> AddTrailer(Guid movieId, Trailer trailerToAdd);
    }
}
