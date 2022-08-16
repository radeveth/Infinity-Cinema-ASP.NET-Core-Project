namespace InfinityCinema.Services.Data.GenresService
{
    using System.Threading.Tasks;

    public interface IGenreService
    {
        Task<string> CreateAsync(CreateGenreFormModel genreFormModel);
    }
}
