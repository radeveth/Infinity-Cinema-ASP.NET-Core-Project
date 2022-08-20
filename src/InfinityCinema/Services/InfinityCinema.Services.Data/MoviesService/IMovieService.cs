namespace InfinityCinema.Services.Data.MoviesService
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using InfinityCinema.Data.Models;

    public interface IMovieService
    {
        Task<string> CreateMovieAsync(CreateMovieServiceModel movieModel, ClaimsPrincipal user);

        Task<Movie> CreateAsync(MovieFormModel movieFormModel, int dealerId, string userId);
    }
}
