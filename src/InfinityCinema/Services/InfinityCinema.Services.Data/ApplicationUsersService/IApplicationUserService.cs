namespace InfinityCinema.Services.Data.ApplicationUsersService
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IApplicationUserService
    {
        T GetViewModelById<T>(string id);

        IEnumerable<string> GetUsersIdsThatAreSaveGivenMovie(int movieId);
    }
}
