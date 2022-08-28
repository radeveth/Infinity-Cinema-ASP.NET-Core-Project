namespace InfinityCinema.Services.Data.MoviesService
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using InfinityCinema.Data.Models;
    using InfinityCinema.Services.Data.MoviesService.Enums;
    using InfinityCinema.Services.Data.MoviesService.Models;

    public interface IMovieService
    {
        // Create
        Task<Movie> CreateAsync(MovieFormModel movieFormModel, int dealerId, int countryId, string userId);

        Task<string> CreateMovieAsync(CreateMovieServiceModel movieModel, ClaimsPrincipal user);

        // Read
        MovieDetailsViewModel Details(int id);

        Task<MovieFormModel> GetMovieById(int id);

        AllMoviesQueryModel All(string searchName, MovieSorting sorting, int currentPage, int moviesPerPage, string searchGenre);

        // Update
        Task<bool> Edit(EditMovieServiceModel movieModel, int movieId);

        // Delete
    }
}
