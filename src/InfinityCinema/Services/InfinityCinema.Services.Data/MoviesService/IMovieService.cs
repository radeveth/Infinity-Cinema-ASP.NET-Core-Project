namespace InfinityCinema.Services.Data.MoviesService
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using InfinityCinema.Data.Models;
    using InfinityCinema.Services.Data.MoviesService.Enums;
    using InfinityCinema.Services.Data.MoviesService.Models;

    public interface IMovieService
    {
        // Create
        Task<T> CreateAsync<T>(MovieFormModel movieFormModel, int dealerId, int countryId, string userId);

        Task CreateMovieAsync(CreateMovieServiceModel movieModel, string userId);

        // Read
        MovieDetailsServiceModel Details(int id);

        T GetViewModelById<T>(int id);

        Movie GetMovieById(int id);

        AllMoviesQueryModel All(string searchName, MovieSorting sorting, int currentPage, int moviesPerPage, string searchGenre);

        List<MovieHomeViewModel> GetTopThreeRatedMovies();

        MovieFormModel GetMovieFormModel(int id);

        int GetMovieIdByName(string name);

        // Update
        Task<bool> EditAsync(MovieFormModel movieForm, int id);

        // Delete
        Task DeleteAsync(DeleteMovieServiceModel deleteMovieServiceModel);

        bool CheckIfMovieWithGivenIdExist(int id);
    }
}
