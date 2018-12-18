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
    [Route("api/movies/{movieId}/posters")]
    [ApiController]
    public class PostersController : ControllerBase
    {
        private readonly IPostersRepository _postersRepository;
        private readonly IMapper _mapper;

        public PostersController(IPostersRepository postersRepository,
            IMapper mapper)
        {
            _postersRepository = postersRepository ?? throw new ArgumentNullException(nameof(postersRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("{posterId}", Name = "GetPoster")]
        public async Task<ActionResult<Models.Poster>> GetPoster(Guid movieId, Guid posterId)
        {
            var poster = await _postersRepository.GetPosterAsync(movieId, posterId);
            if (poster == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<Models.Poster>(poster));
        }

        [HttpPost]
        public async Task<IActionResult> CreatePoster(Guid movieId, 
            [FromBody] Models.PosterForCreation posterForCreation)
        {
            // model validation 
            if (posterForCreation == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                // return 422 - Unprocessable Entity when validation fails
                return new UnprocessableEntityObjectResult(ModelState);
            }

            var poster = _mapper.Map<Poster>(posterForCreation);
            var createdPoster = await _postersRepository.AddPoster(movieId, poster);

            // no need to save, in this type of repo the poster is
            // immediately persisted.  

            // map the poster from the repository to a shared model poster
            return CreatedAtRoute("GetPoster",
                new { movieId, posterId = createdPoster.Id },
                _mapper.Map<Models.Poster>(createdPoster));
        }
    }
}
