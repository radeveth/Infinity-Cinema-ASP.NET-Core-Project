﻿namespace InfinityCinema.Services.Data.GenresService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using InfinityCinema.Data;
    using InfinityCinema.Data.Models;
    using InfinityCinema.Services.Data.GenresService.Models;
    using InfinityCinema.Services.Mapping;

    public class GenreService : IGenreService
    {
        private readonly InfinityCinemaDbContext dbContext;

        public GenreService(InfinityCinemaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // Create
        public async Task<T> CreateAsync<T>(GenreFormModel genreFormModel)
        {
            Genre genre = new Genre()
            {
                Name = genreFormModel.Name,
                ImageUrl = genreFormModel.ImageUrl,
                Description = genreFormModel.Description,
            };

            await this.dbContext.Genres.AddAsync(genre);
            await this.dbContext.SaveChangesAsync();

            return this.GetViewModelById<T>(genre.Id);
        }

        // Read
        public IEnumerable<T> All<T>(string searchName = null)
        {
            IQueryable<Genre> genres = this.dbContext.Genres;

            if (searchName != null)
            {
                genres = genres.Where(g => g.Name.ToLower().Contains(searchName.ToLower()));
            }

            return genres.To<T>();
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

        public T GetViewModelById<T>(int id)
            => this.dbContext
                .Genres
                .Where(d => d.Id == id)
                .To<T>()
                .FirstOrDefault();

        // Update

        // Delete
        public async Task DeleteAsync(int id)
        {
            Genre genre = await this.dbContext.Genres.FindAsync(id);

            if (genre == null)
            {
                throw new NullReferenceException();
            }

            genre.IsDeleted = true;
            genre.DeletedOn = DateTime.UtcNow;

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
