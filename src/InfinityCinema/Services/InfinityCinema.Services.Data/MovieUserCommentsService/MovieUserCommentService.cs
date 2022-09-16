namespace InfinityCinema.Services.Data.MovieUserCommentsService
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using InfinityCinema.Data;
    using InfinityCinema.Data.Models;
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
        public IEnumerable<T> GetCommentsForGivenMovie<T>(int movieId)
            => this.dbContext
                    .MovieUserComments
                    .Where(m => m.MovieId == movieId)
                    .Select(m => m.MovieComment)
                    .AsQueryable()
                    .To<T>();

        public IEnumerable<string> GetUsersIdsThatAreCommentInGivenMovie(int movieId)
        {
            IQueryable<MovieUserComment> movieUserComments = this.dbContext.MovieUserComments.Where(m => m.MovieId == movieId).Distinct();
            IQueryable<MovieComment> movieComments = movieUserComments.Select(m => m.MovieComment);
            return (IEnumerable<string>)movieComments.Select(m => m.UserId);
        }
    }
}
