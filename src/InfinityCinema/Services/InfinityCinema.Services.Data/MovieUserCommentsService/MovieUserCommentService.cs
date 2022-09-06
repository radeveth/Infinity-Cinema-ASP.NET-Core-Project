namespace InfinityCinema.Services.Data.MovieUserCommentsService
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using InfinityCinema.Data;
    using InfinityCinema.Data.Models;
    using InfinityCinema.Services.Data.ApplicationUsersService.Models;
    using InfinityCinema.Services.Data.MovieCommentsService.Models;

    public class MovieUserCommentService : IMovieUserCommentService
    {
        private readonly InfinityCinemaDbContext dbContext;

        public MovieUserCommentService(InfinityCinemaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateAsync(int movieId, int commentId)
        {
            await this.dbContext.MovieUserComments.AddAsync(new MovieUserComment()
            {
                MovieId = movieId,
                CommentId = commentId,
            });

            await this.dbContext.SaveChangesAsync();
        }

        public IEnumerable<MovieCommentViewModel> GetCommentsForGivenMovie(int movieId)
        {
            return this.dbContext
                    .MovieUserComments
                    .Where(m => m.MovieId == movieId)
                    .Select(m => m.MovieComment)
                    .AsQueryable()
                    .Select(m => new MovieCommentViewModel()
                    {
                        Id = m.Id,
                        Content = m.Content,
                        Likes = m.Likes,
                        Dislikes = m.Dislikes,
                        User = new ApplicationUserViewModel()
                        {
                            Id = m.UserId,
                            FullName = m.User.FullName,
                        },
                    });
        }
    }
}
