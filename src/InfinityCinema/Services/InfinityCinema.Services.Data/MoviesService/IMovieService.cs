namespace InfinityCinema.Services.Data.MoviesService
{
    using System.Threading.Tasks;

    using InfinityCinema.Data.Models;

    public interface IMovieService
    {
        Task<string> CreateMovieAsync(CreateMovieServiceModel movieModel);

        Task<Movie> CreateAsync(MovieFormModel movieFormModel, int dealerId);
    }
}
