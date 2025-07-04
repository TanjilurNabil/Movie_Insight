using Movies.Application.Models;
using Movies.Contracts.Requests;
using Movies.Contracts.Responses;

namespace Movies.API.Mapping
{
    public static class ContractMapping
    {
        public static Movie MapToMovie(this CreateMovieRequest request) 
        {
            return new Movie
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                YearOfRelease = request.YearOfRelease,
                Genres = request.Genres.ToList()
            };
        }
        public static MovieResponse MapToResponse(this Movie response)
        {
            return new MovieResponse
            {
                Id = response.Id,
                Title = response.Title,
                YearOfRelease = response.YearOfRelease,
                Genres = response.Genres.ToList()
            };
        }
        public static MoviesResponse MapToResponse(this IEnumerable<Movie> movies)
        {
            return new MoviesResponse
            {
                Items = movies.Select(MapToResponse)
            };
            
        }
    }
}
