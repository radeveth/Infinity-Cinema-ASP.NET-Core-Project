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

        // Create
        public async Task<GenreViewModel> CreateAsync(GenreFormModel genreFormModel)
        {
            Genre genre = new Genre()
            {
                Name = genreFormModel.Name,
                ImageUrl = genreFormModel.ImageUrl,
            };

            await this.dbContext.Genres.AddAsync(genre);
            await this.dbContext.SaveChangesAsync();

            return new GenreViewModel()
            {
                Id = genre.Id,
                Name = genre.Name,
                ImageUrl = genre.ImageUrl,
            };
        }

        // Read
        public IEnumerable<GenreViewModel> All()
        {
            IEnumerable<GenreViewModel> genres = this.dbContext
                .Genres
                .Select(g => new GenreViewModel()
                {
                    Id = g.Id,
                    Name = g.Name,
                    ImageUrl = g.ImageUrl,
                    Description = g.Description,
                });

            return genres;
        }

        public int GetGenreIdByGivenName(string genreName)
        {
            genreName = genreName.ToLower();
            Genre genre = this.dbContext.Genres.FirstOrDefault(g => g.Name.ToLower() == genreName);

            return genre.Id;
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

        public IEnumerable<string> AllApplicationMovieGenres()
            => this.dbContext.Genres.Select(g => g.Name);

        public IEnumerable<GenreViewModel> GetGenresForParticularMovie(int movieId)
            => this.dbContext
                .MovieGenres
                .Where(m => m.MovieId == movieId)
                .Select(m => new GenreViewModel()
                {
                    Id = m.GenreId,
                    Name = m.Genre.Name,
                    ImageUrl = m.Genre.ImageUrl,
                });

        // Update

        // Delete
        public async Task DeleteGenresForParticularMovie(int movieId)
        {
            IQueryable<MovieGenre> movieGenres = this.dbContext.MovieGenres.Where(m => m.MovieId == movieId);

            this.dbContext.MovieGenres.RemoveRange(movieGenres);
            await this.dbContext.SaveChangesAsync();
        }

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
    }
}
