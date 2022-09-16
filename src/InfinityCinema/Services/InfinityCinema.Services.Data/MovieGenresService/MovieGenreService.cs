namespace InfinityCinema.Services.Data.MovieGenresService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using InfinityCinema.Data;
    using InfinityCinema.Data.Models;
    using InfinityCinema.Services.Mapping;

    public class MovieGenreService : IMovieGenreService
    {
        private readonly InfinityCinemaDbContext dbContext;

        public MovieGenreService(InfinityCinemaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<T> GetGenresForParticularMovie<T>(int movieId)
            => this.dbContext
                .MovieGenres
                .Where(m => m.MovieId == movieId)
                .Select(m => m.Genre)
                .To<T>();

        public async Task DeleteGenresForParticularMovie(int movieId)
        {
            IQueryable<MovieGenre> movieGenres = this.dbContext.MovieGenres.Where(m => m.MovieId == movieId);

            foreach (var movieGenre in movieGenres)
            {
                movieGenre.IsDeleted = true;
                movieGenre.DeletedOn = DateTime.UtcNow;
            }

            await this.dbContext.SaveChangesAsync();
        }
    }
}
