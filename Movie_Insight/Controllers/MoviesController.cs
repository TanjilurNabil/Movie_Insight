using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.Application.Models;
using Movies.Application.Repositories;
using Movies.Contracts.Requests;

namespace Movies.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;

        public MoviesController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }
        [HttpPost("movies")]
        public async Task<IActionResult> Create([FromBody] CreateMovieRequest movieRequest)
        {
            var movie = new Movie
            {
                Id = Guid.NewGuid(),
                Title = movieRequest.Title,
                YearOfRelease = movieRequest.YearOfRelease,
                Genres = movieRequest.Genres.ToList()
            };
             await _movieRepository.CreateAsync(movie);
            return Created($"/api/movies/{movie.Id}", movie);
        }
    }
}
