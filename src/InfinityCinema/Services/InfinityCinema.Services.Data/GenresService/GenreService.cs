namespace InfinityCinema.Services.Data.GenresService
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using InfinityCinema.Data;
    using InfinityCinema.Data.Models;
    using InfinityCinema.Services.Data.GenresService.Models;

    public class GenreService : IGenreService
    {
        private readonly InfinityCinemaDbContext dbContext;

        public GenreService(InfinityCinemaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Genre> CreateAsync(GenreFormModel genreFormModel)
        {
            Genre genre = new Genre()
            {
                Name = genreFormModel.Name,
                ImageUrl = genreFormModel.ImaqgeUrl,
            };

            await this.dbContext.Genres.AddAsync(genre);
            await this.dbContext.SaveChangesAsync();

            return genre;
        }

        public IEnumerable<GenreFormModel> GetMovieGenres()
            => this.dbContext
                .Genres
                .Select(g => new GenreFormModel()
                {
                    Id = g.Id,
                    Name = g.Name,
                })
                .ToList();

        public bool IsGenresExists(IEnumerable<int> ids)
        {
            bool isAllGenresExist = true;

            foreach (var genreId in ids)
            {
                if (!this.dbContext.Genres.Any(g => g.Id == genreId))
                {
                    isAllGenresExist = false;
                    return isAllGenresExist;
                }
            }

            return isAllGenresExist;
        }

        public int GetGenreIdByGivenName(string genreName)
        {
            genreName = genreName.ToLower();
            Genre genre = this.dbContext.Genres.FirstOrDefault(g => g.Name.ToLower() == genreName);

            return genre.Id;
        }

        public async Task DeleteGenresForParticularMovie(int movieId)
        {
            IQueryable<MovieGenre> movieGenres = this.dbContext.MovieGenres.Where(m => m.MovieId == movieId);

            this.dbContext.MovieGenres.RemoveRange(movieGenres);
            await this.dbContext.SaveChangesAsync();
        }

        public IEnumerable<string> GetGenresForParticularMovie(int movieId)
        {
            IQueryable<MovieGenre> movieGenres = this.dbContext.MovieGenres.Where(m => m.MovieId == movieId);

            return movieGenres.Select(m => m.Genre.Name);
        }
    }
}
