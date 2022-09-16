namespace InfinityCinema.Services.Data.ApplicationUsersService
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using InfinityCinema.Data;
    using InfinityCinema.Data.Models;
    using InfinityCinema.Services.Mapping;

    public class ApplicationUserService : IApplicationUserService
    {
        private readonly InfinityCinemaDbContext dbContext;

        public ApplicationUserService(InfinityCinemaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public T GetViewModelById<T>(string id)
            => this.dbContext
                .Users
                .Where(u => u.Id == id)
                .To<T>()
                .FirstOrDefault();

        public IEnumerable<string> GetUsersIdsThatAreSaveGivenMovie(int movieId)
        {
            return (IEnumerable<string>)this.dbContext
                .ApplicationUserMovies
                .Where(m => m.MovieId == movieId)
                .Distinct()
                .Select(m => m.UserId);
        }
    }
}
