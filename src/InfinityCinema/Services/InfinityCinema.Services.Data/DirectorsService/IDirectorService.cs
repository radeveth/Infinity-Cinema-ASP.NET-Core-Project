namespace InfinityCinema.Services.Data.DirectorsService
{
    using System.Threading.Tasks;

    using InfinityCinema.Services.Data.DirectorsService.Models;

    public interface IDirectorService
    {
        // Create
        Task<T> CreateAsync<T>(DirectorFormModel directorFormModel);

        // Read
        T GetViewModelByGivenFullName<T>(string fullName);

        public T GetViewModelById<T>(int id);

        // Update
        Task<bool> EditDirectorAsync(int directorId, DirectorFormModel directorFormModel);

        // Delete
    }
}
