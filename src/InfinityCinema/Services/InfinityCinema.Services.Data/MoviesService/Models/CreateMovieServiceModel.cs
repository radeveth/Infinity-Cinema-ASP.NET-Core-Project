namespace InfinityCinema.Services.Data.MoviesService.Models
{
    using System.Collections.Generic;
    using InfinityCinema.Services.Data.ActorsService.Models;
    using InfinityCinema.Services.Data.GenresService;
    using InfinityCinema.Services.Data.ImagesService.Models;
    using InfinityCinema.Services.Data.PlatformsService.Models;

    public class CreateMovieServiceModel
    {
        public MovieFormModel OverallMovieInformation { get; set; }

        public IEnumerable<ImageFormModel> Images { get; set; }

        public IEnumerable<ActorFormModel> Actors { get; set; }

        public IEnumerable<PlatformFormModel> Platforms { get; set; }
    }
}
