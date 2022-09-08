namespace InfinityCinema.Services.Data.DirectorsService
{
    using System.Threading.Tasks;

    using InfinityCinema.Services.Data.DirectorsService.Models;

    public interface IDirectorService
    {
        // Create
        Task<T> CreateAsync<T>(DirectorFormModel directorFormModel);

        // Read
        string GetDirectorFullNameById(int id);

        int GetDirectorIdByGivenFullName(string fullName);

        DirectorViewModel GetDirectorForParticularMovie(int directorId);

        // Update
        Task<bool> EditDirectorAsync(int directorId, DirectorFormModel directorFormModel);

        // Delete
    }
}
