namespace InfinityCinema.Services.Data.ApplicationUsersService
{
    using System.Linq;

    using InfinityCinema.Data;
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
    }
}
