namespace InfinityCinema.Services.Data.MovieUserCommentsService
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using InfinityCinema.Data;
    using InfinityCinema.Data.Models;
    using InfinityCinema.Services.Data.ApplicationUsersService.Models;
    using InfinityCinema.Services.Data.MovieCommentsService.Models;
    using InfinityCinema.Services.Mapping;

    public class MovieUserCommentService : IMovieUserCommentService
    {
        private readonly InfinityCinemaDbContext dbContext;

        public MovieUserCommentService(InfinityCinemaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // Create
        public async Task CreateAsync(int movieId, int commentId)
        {
            await this.dbContext.MovieUserComments.AddAsync(new MovieUserComment()
            {
                MovieId = movieId,
                CommentId = commentId,
            });

            await this.dbContext.SaveChangesAsync();
        }

        // Read
        // MovieCommentViewModel
        public IEnumerable<T> GetCommentsForGivenMovie<T>(int movieId)
            => this.dbContext
                    .MovieUserComments
                    .Where(m => m.MovieId == movieId)
                    .Select(m => m.MovieComment)
                    .AsQueryable()
                    .To<T>();
    }
}
