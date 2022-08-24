namespace InfinityCinema.Services.Data.DirectorsService
{
    using System.Threading.Tasks;

    using InfinityCinema.Data.Models;
    using InfinityCinema.Services.Data.DirectorsService.Models;

    public interface IDirectorService
    {
        Task<Director> CreateAsync(DirectorFormModel directorFormModel);

        Task<int> GetDirectorIdAsync(DirectorFormModel directorFormModel);

        string GetDirectorFullNameById(int id);
    }
}
