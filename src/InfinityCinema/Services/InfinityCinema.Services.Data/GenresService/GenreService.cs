namespace InfinityCinema.Services.Data.GenresService
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using InfinityCinema.Data;
    using InfinityCinema.Data.Models;

    public class GenreService : IGenreService
    {
        private readonly InfinityCinemaDbContext dbContext;

        public GenreService(InfinityCinemaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<Genre> CreateAsync(GenreFormModel genreFormModel)
        {
            throw new System.NotImplementedException();
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

        public bool IsGenresExists(ICollection<int> ids)
            => ids.Any(id => this.dbContext.Genres
                .Any(g => g.Id == id));
    }
}
