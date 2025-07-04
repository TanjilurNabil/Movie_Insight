using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.API.Mapping;
using Movies.Application.Models;
using Movies.Application.Repositories;
using Movies.Contracts.Requests;

namespace Movies.API.Controllers
{

    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;

        public MoviesController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }
        [HttpPost(ApiEndpoints.Movies.Create)]
        public async Task<IActionResult> Create([FromBody] CreateMovieRequest movieRequest)
        {
            var movie = movieRequest.MapToMovie();
            await _movieRepository.CreateAsync(movie);
            return CreatedAtAction(nameof(Get), new { id = movie.Id }, movie);
            
        }
        [HttpGet(ApiEndpoints.Movies.Get)]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var movie  = await _movieRepository.GetByIdAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return Ok(movie.MapToResponse());
        }
        [HttpGet(ApiEndpoints.Movies.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            var movies = await _movieRepository.GetAllAsync();
            if(movies is null)
            {
                return NotFound();
            }
            var response = movies.MapToResponse();
            return Ok(response);
        }
    }
}
