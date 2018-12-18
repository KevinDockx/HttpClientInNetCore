using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Movies.API.Entities;
using Movies.API.Services;

namespace Movies.API.Controllers
{
    [Route("api/movies")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMoviesRepository _moviesRepository;
        private readonly IMapper _mapper;

        public MoviesController(IMoviesRepository moviesRepository, 
            IMapper mapper)
        {
            _moviesRepository = moviesRepository ?? throw new ArgumentNullException(nameof(moviesRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Movie>>> GetMovies()
        {
            var movieEntities = await _moviesRepository.GetMoviesAsync();
            return Ok(_mapper.Map<IEnumerable<Models.Movie>>(movieEntities));
        }

        
        [HttpGet("{movieId}", Name = "GetMovie")]
        public async Task<ActionResult<Models.Movie>> GetMovie(Guid movieId)
        {
            var movieEntity = await _moviesRepository.GetMovieAsync(movieId);
            if (movieEntity == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<Models.Movie>(movieEntity));
        } 

        [HttpPost]
        public async Task<IActionResult> CreateMovie(
            [FromBody] Models.MovieForCreation movieForCreation)
        {
            // model validation 
            if (movieForCreation == null)
            {
                return BadRequest();
            } 

            if (!ModelState.IsValid)
            {
                // return 422 - Unprocessable Entity when validation fails
                return new UnprocessableEntityObjectResult(ModelState);
            }

            var movieEntity = _mapper.Map<Movie>(movieForCreation);
            _moviesRepository.AddMovie(movieEntity);
            
            // save the changes
            await _moviesRepository.SaveChangesAsync();

            // Fetch the movie from the data store so the director is included
            await _moviesRepository.GetMovieAsync(movieEntity.Id);

            return CreatedAtRoute("GetMovie",
                new { movieId = movieEntity.Id },
                _mapper.Map<Models.Movie>(movieEntity));
        }
         
        [HttpPut("{movieId}")]
        public async Task<IActionResult> UpdateMovie(Guid movieId, 
            [FromBody] Models.MovieForUpdate movieForUpdate)
        {
            // model validation 
            if (movieForUpdate == null)
            {
                //return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                // return 422 - Unprocessable Entity when validation fails
                return new UnprocessableEntityObjectResult(ModelState);
            }

            var movieEntity = await _moviesRepository.GetMovieAsync(movieId);
            if (movieEntity == null)
            {
                return NotFound();
            }

            // map the inputted object into the movie entity
            // this ensures properties will get updated
            _mapper.Map(movieForUpdate, movieEntity);

            // call into UpdateMovie even though in our implementation 
            // this doesn't contain code - doing this ensures the code stays
            // reliable when other repository implemenations (eg: a mock 
            // repository) are used.
            _moviesRepository.UpdateMovie(movieEntity);

            await _moviesRepository.SaveChangesAsync();

            // return the updated movie, after mapping it
            return Ok(_mapper.Map<Models.Movie>(movieEntity));
        }

        [HttpPatch("{movieId}")]
        public async Task<IActionResult> PartiallyUpdateMovie(Guid movieId, 
            [FromBody] JsonPatchDocument<Models.MovieForUpdate> patchDoc)
        {
            var movieEntity = await _moviesRepository.GetMovieAsync(movieId);
            if (movieEntity == null)
            {
                return NotFound();
            }

            // the patch is on a DTO, not on the movie entity
            var movieToPatch = Mapper.Map<Models.MovieForUpdate>(movieEntity);

            patchDoc.ApplyTo(movieToPatch, ModelState);
              
            if (!ModelState.IsValid)
            {
                return new UnprocessableEntityObjectResult(ModelState);
            }

            // map back to the entity, and save
            Mapper.Map(movieToPatch, movieEntity);

            // call into UpdateMovie even though in our implementation 
            // this doesn't contain code - doing this ensures the code stays
            // reliable when other repository implemenations (eg: a mock 
            // repository) are used.
            _moviesRepository.UpdateMovie(movieEntity);

            await _moviesRepository.SaveChangesAsync();

            // return the updated movie, after mapping it
            return Ok(_mapper.Map<Models.Movie>(movieEntity));
        }

        [HttpDelete("{movieid}")]
        public async Task<IActionResult> DeleteMovie(Guid movieId)
        {
            var movieEntity = await _moviesRepository.GetMovieAsync(movieId);
            if (movieEntity == null)
            {
                return NotFound();
            }

            _moviesRepository.DeleteMovie(movieEntity);
            await _moviesRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}
