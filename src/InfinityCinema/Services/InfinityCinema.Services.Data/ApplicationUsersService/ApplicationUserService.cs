﻿namespace InfinityCinema.Services.Data.ApplicationUsersService
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using InfinityCinema.Data;
    using InfinityCinema.Data.Models;

    public class ApplicationUserService : IApplicationUserService
    {
        private readonly InfinityCinemaDbContext dbContext;

        public ApplicationUserService(InfinityCinemaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> SaveMovieToWatchLaterAsync(int movieId, string userId)
        {
            ApplicationUser user = this.dbContext.Users.FirstOrDefault(u => u.Id == userId);
            Movie movie = this.dbContext.Movies.FirstOrDefault(m => m.Id == movieId);

            this.dbContext.ApplicationUserMovies.Add(new ApplicationUserMovie()
            {
                UserId = user.Id,
                MovieId = movie.Id,
            });

            await this.dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RemoveMovieFromWatchLaterAsync(int movieId, string userId)
        {
            ApplicationUser user = this.dbContext.Users.FirstOrDefault(u => u.Id == userId);
            Movie movie = this.dbContext.Movies.FirstOrDefault(m => m.Id == movieId);

            ApplicationUserMovie applicationUserMovie = this.dbContext
                .ApplicationUserMovies
                .FirstOrDefault(a => a.UserId == user.Id && a.MovieId == movie.Id);

            this.dbContext.ApplicationUserMovies.Remove(applicationUserMovie);

            await this.dbContext.SaveChangesAsync();

            return true;
        }

        public async Task RateMovie(int movieId, string userId, decimal rating)
        {
            this.dbContext.MovieUserStarRatings.Add(new MovieUserStarRating()
            {
                MovieId = movieId,
                UserId = userId,
                Rate = rating,
            });

            await this.dbContext.SaveChangesAsync();
        }

        public bool CheckIfUserIsAlreadyRatedThisMovie(int movieId, string userId)
            => this.dbContext.MovieUserStarRatings.Any(m => m.UserId == userId && m.MovieId == movieId);
    }
}