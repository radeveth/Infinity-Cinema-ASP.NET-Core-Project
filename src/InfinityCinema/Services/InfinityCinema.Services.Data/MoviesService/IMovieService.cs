namespace InfinityCinema.Services.Data.MoviesService
{
    using System.Threading.Tasks;

    public interface IMovieService
    {
        Task<string> CreateMovie(CreateMovieFormModel movie);
    }
}
