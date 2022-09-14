namespace InfinityCinema.Web.Areas.Administration.AdministartionsService
{
    using System.Linq;

    using InfinityCinema.Data;
    using InfinityCinema.Web.Areas.Administration.AdministartionsService.Models;

    public class AdministartionService : IAdministartionService
    {
        private readonly InfinityCinemaDbContext dbContext;

        public AdministartionService(InfinityCinemaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public AdministartionIndexViewModel ApplicationStatistics()
        {
            return new AdministartionIndexViewModel()
            {
                TotalMovies = this.dbContext.Movies.Count(),
                TotalGenres = this.dbContext.Genres.Count(),
                TotalUsers = this.dbContext.Users.Count(),
            };
        }
    }
}
