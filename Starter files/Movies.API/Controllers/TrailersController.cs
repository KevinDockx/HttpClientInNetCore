using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Movies.API.InternalModels;
using Movies.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.API.Controllers
{
    [Route("api/movies/{movieId}/trailers")]
    [ApiController]
    public class TrailersController : ControllerBase
    {
        private readonly ITrailersRepository _trailersRepository;
        private readonly IMapper _mapper;

        public TrailersController(ITrailersRepository trailersRepository,
            IMapper mapper)
        {
            _trailersRepository = trailersRepository ?? throw new ArgumentNullException(nameof(trailersRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("{trailerId}", Name = "GetTrailer")]
        public async Task<ActionResult<Models.Trailer>> GetTrailer(Guid movieId, Guid trailerId)
        {
            var trailer = await _trailersRepository.GetTrailerAsync(movieId, trailerId);
            if (trailer == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<Models.Trailer>(trailer));
        }

        [HttpPost]
        public async Task<IActionResult> CreateTrailer(Guid movieId,
            [FromBody] Models.TrailerForCreation trailerForCreation)
        {
            // model validation 
            if (trailerForCreation == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                // return 422 - Unprocessable Entity when validation fails
                return new UnprocessableEntityObjectResult(ModelState);
            }

            var trailer = _mapper.Map<Trailer>(trailerForCreation);
            var createdTrailer = await _trailersRepository.AddTrailer(movieId, trailer);

            // no need to save, in this type of repo the trailer is
            // immediately persisted.  

            // map the trailer from the repository to a shared model trailer
            return CreatedAtRoute("GetTrailer",
                new { movieId, trailerId = createdTrailer.Id },
                _mapper.Map<Models.Trailer>(createdTrailer));
        }
    }
}
