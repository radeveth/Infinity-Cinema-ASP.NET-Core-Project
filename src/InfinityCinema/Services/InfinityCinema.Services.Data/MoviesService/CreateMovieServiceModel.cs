namespace InfinityCinema.Services.Data.MoviesService
{
    using System.Collections.Generic;

    using InfinityCinema.Services.Data.ActorsService;
    using InfinityCinema.Services.Data.GenresService;
    using InfinityCinema.Services.Data.ImagesService;
    using InfinityCinema.Services.Data.PlatformsService;

    public class CreateMovieServiceModel
    {
        public MovieFormModel OverallMovieInformation { get; set; }

        public IEnumerable<ImageFormModel> Images { get; set; }

        public ActorFormModel Actors { get; set; }

        public IEnumerable<GenreFormModel> Genres { get; set; }

        public IEnumerable<PlatformFormModel> Platforms { get; set; }
    }
}
