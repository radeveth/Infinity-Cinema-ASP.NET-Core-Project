namespace InfinityCinema.Services.Data.MovieCommentsService
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using InfinityCinema.Data;
    using InfinityCinema.Data.Models;
    using InfinityCinema.Services.Data.ApplicationUsersService;
    using InfinityCinema.Services.Data.ApplicationUsersService.Models;
    using InfinityCinema.Services.Data.MovieCommentsService.Models;

    public class MovieCommentService : IMovieCommentService
    {
        private readonly InfinityCinemaDbContext dbContext;
        private readonly IApplicationUserService userService;

        public MovieCommentService(InfinityCinemaDbContext dbContext, IApplicationUserService userService)
        {
            this.dbContext = dbContext;
            this.userService = userService;
        }

        // Create
        public async Task<MovieCommentViewModel> CreateAsync(MovieCommentFormModel commentFormModel)
        {
            MovieComment comment = new MovieComment()
            {
                Content = commentFormModel.Content,
                MovieId = commentFormModel.MovieId,
                UserId = commentFormModel.UserId,
            };

            await this.dbContext.MovieComments.AddAsync(comment);
            await this.dbContext.SaveChangesAsync();

            return new MovieCommentViewModel()
            {
                Id = comment.Id,
                Content = comment.Content,
                MovieId = comment.MovieId,
                User = new ApplicationUserViewModel()
                {
                    Id = comment.UserId,
                    FullName = comment.User.FullName,
                },
            };
        }

        // Read
        public IEnumerable<MovieCommentViewModel> GetCommentsForGivenMovie(int movieId)
        {
            return this.dbContext
                .MovieComments
                .Where(m => m.MovieId == movieId)
                .Select(m => new MovieCommentViewModel()
                {
                    Id = m.Id,
                    Content = m.Content,
                    User = new ApplicationUserViewModel()
                    {
                        Id = m.UserId,
                        FullName = m.User.FullName,
                    },
                    MovieId = m.MovieId,
                });
        }
    }
}
