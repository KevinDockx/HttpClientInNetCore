using AutoMapper;
using System.Collections.Generic;

namespace Movies.API
{
    /// <summary>
    /// AutoMapper profile for working with Movie objects
    /// </summary>
    public class MoviesProfile : Profile
    {
        public MoviesProfile()
        {
            CreateMap<Entities.Movie, Models.Movie>()
                .ForMember(dest => dest.Director, opt => opt.MapFrom(src =>
                   $"{src.Director.FirstName} {src.Director.LastName}"));

            CreateMap<Models.MovieForCreation, Entities.Movie>();

            CreateMap<Models.MovieForUpdate, Entities.Movie>().ReverseMap(); 
        }
    }
}
