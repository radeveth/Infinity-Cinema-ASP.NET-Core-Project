namespace InfinityCinema.Services.Data.MoviesService
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using InfinityCinema.Data;
    using InfinityCinema.Data.Models;
    using InfinityCinema.Services.Data.DirectorsService;
    using InfinityCinema.Services.Data.ImagesService;

    public class MovieService : IMovieService
    {
        private readonly InfinityCinemaDbContext dbContext;
        private readonly IDirectorService directorService;
        private readonly IImageService imageService;

        public MovieService(InfinityCinemaDbContext dbContext, IDirectorService directorService, IImageService imageService)
        {
            this.dbContext = dbContext;
            this.directorService = directorService;
            this.imageService = imageService;
        }

        // public Task<string> CreateMovie(MovieFormModel movieModel)
        // {
        //    // DirectorMovie movie = new DirectorMovie()
        //    // {
        //    //    Name = movieModel.Name,
        //    //    DateOfReleased = movieModel.DateOfReleased,
        //    //    Resolution = movieModel.Resolution,
        //    //    Description = movieModel.Description,
        //    //    TrailerPath = movieModel.TrailerPath,
        //    //    ImageUrl = movieModel.ImageUrl,
        //    //    Duration = TimeSpan.Parse(movieModel.Duration),
        //    // };

        // // this.dbContext.Movies.Add(movie);
        //    // this.dbContext.SaveChanges();

        // // return $"Move {movie.Name} was successfully added!";
        //    return null;
        // }
        public async Task<string> CreateMovieAsync(CreateMovieServiceModel createMovieModel)
        {
            DirectorFormModel directorFormModel = createMovieModel.OverallMovieInformation.Director;
            int directorId = await this.directorService.GetDirectorIdAsync(directorFormModel);

            MovieFormModel movieFormModel = createMovieModel.OverallMovieInformation;
            Movie movie = await this.CreateAsync(movieFormModel, directorId);

            IEnumerable<ImageFormModel> images = createMovieModel.Images;
            foreach (ImageFormModel image in images)
            {
                await this.imageService.CreateAsync(image);
            }

            IEnumerable<int> genresIds = createMovieModel.OverallMovieInformation.GenresId;
            foreach (int genreId in genresIds)
            {
                await this.dbContext.MovieGenres.AddAsync(new MovieGenre() { MovieId = movie.Id, GenreId = genreId });
            }


            return null;
        }

        public async Task<Movie> CreateAsync(MovieFormModel movieFormModel, int directorId)
        {
            Movie movie = new Movie()
            {
                Name = movieFormModel.Name,
                DateOfReleased = movieFormModel.DateOfReleased,
                Resolution = movieFormModel.Resolution,
                Description = movieFormModel.Description,
                TrailerPath = movieFormModel.TrailerPath,
                DirectorId = directorId,
                Duration = TimeSpan.Parse(movieFormModel.Duration),
            };

            await this.dbContext.AddAsync(movie);
            await this.dbContext.SaveChangesAsync();

            return new Movie();
        }
    }
}
