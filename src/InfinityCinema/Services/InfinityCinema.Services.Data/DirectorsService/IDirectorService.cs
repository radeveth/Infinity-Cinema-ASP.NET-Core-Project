namespace InfinityCinema.Services.Data.DirectorsService
{
    using System.Threading.Tasks;

    using InfinityCinema.Data.Models;

    public interface IDirectorService
    {
        Task<Director> CreateAsync(DirectorFormModel directorFormModel);

        Task<int> GetDirectorIdAsync(DirectorFormModel directorFormModel);
    }
}
