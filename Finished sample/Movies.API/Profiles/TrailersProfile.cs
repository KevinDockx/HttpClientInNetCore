using AutoMapper;

namespace Movies.API.Profiles
{
    /// <summary>
    /// AutoMapper profile for working with Trailer objects
    /// </summary>
    public class TrailersProfile : Profile
    {
        public TrailersProfile()
        {
            CreateMap<InternalModels.Trailer, Models.Trailer>();
            CreateMap<Models.TrailerForCreation, InternalModels.Trailer>();
        }
    }
}
