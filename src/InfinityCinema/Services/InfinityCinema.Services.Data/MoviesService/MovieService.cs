namespace InfinityCinema.Services.Data.MoviesService
{
    using System;
    using System.Linq;

    using InfinityCinema.Data;
    using InfinityCinema.Data.Models;

    public class MovieService : IMovieService
    {
        private readonly InfinityCinemaDbContext dbContext;

        public MovieService(InfinityCinemaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public string CreateMovie(MovieFormModel movieModel)
        {
            // DirectorMovie movie = new DirectorMovie()
            // {
            //    Name = movieModel.Name,
            //    DateOfReleased = movieModel.DateOfReleased,
            //    Resolution = movieModel.Resolution,
            //    Description = movieModel.Description,
            //    TrailerPath = movieModel.TrailerPath,
            //    ImageUrl = movieModel.ImageUrl,
            //    Duration = TimeSpan.Parse(movieModel.Duration),
            // };

            // this.dbContext.Movies.Add(movie);
            // this.dbContext.SaveChanges();

            // return $"Move {movie.Name} was successfully added!";
            return null;
        }
    }
}
