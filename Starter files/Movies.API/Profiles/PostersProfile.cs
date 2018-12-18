using AutoMapper;

namespace Movies.API.Profiles
{
    /// <summary>
    /// AutoMapper profile for working with Poster objects
    /// </summary>
    public class PostersProfile : Profile
    {
        public PostersProfile()
        {
            CreateMap<InternalModels.Poster, Models.Poster>();
            CreateMap<Models.PosterForCreation, InternalModels.Poster>();
        }
    }
}
