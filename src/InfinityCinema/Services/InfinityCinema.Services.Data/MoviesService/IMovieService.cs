namespace InfinityCinema.Services.Data.MoviesService
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using InfinityCinema.Data.Models;
    using InfinityCinema.Services.Data.MoviesService.Enums;
    using InfinityCinema.Services.Data.MoviesService.Models;

    public interface IMovieService
    {
        Task<string> CreateMovieAsync(CreateMovieServiceModel movieModel, ClaimsPrincipal user);

        Task<Movie> CreateAsync(MovieFormModel movieFormModel, int dealerId, int countryId, string userId);

        AllMoviesQueryModel All(string searchName, MovieSorting sorting, int currentPage, int moviesPerPage, string searchGenre);

        MovieDetailsViewModel Details(int id);

        Task<bool> Edit(EditMovieServiceModel movieModel, int movieId);

        Task<MovieFormModel> GetMovieById(int id);
    }
}
