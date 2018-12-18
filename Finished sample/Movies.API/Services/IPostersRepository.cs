using Movies.API.InternalModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.API.Services
{
    public interface IPostersRepository
    {
        Task<Poster> GetPosterAsync(Guid movieId, Guid posterId);          

        Task<Poster> AddPoster(Guid movieId, Poster posterToAdd); 
    }
}
