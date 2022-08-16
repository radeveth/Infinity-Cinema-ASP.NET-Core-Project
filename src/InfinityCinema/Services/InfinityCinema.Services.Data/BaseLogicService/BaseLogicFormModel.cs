namespace InfinityCinema.Services.Data.BaseLogicService
{
    using System.Collections.Generic;

    using InfinityCinema.Services.Data.ActorsService;
    using InfinityCinema.Services.Data.GenresService;
    using InfinityCinema.Services.Data.ImagesService;
    using InfinityCinema.Services.Data.MoviesService;
    using InfinityCinema.Services.Data.PlatformsService;

    public class BaseLogicFormModel
    {
        public CreateMovieFormModel OverallMovieInformation { get; set; }

        public IEnumerable<AddImageFormModel> Images { get; set; }

        public CreateActorFormModel Actors { get; set; }

        public IEnumerable<CreateGenreFormModel> Genres { get; set; }

        public IEnumerable<CreatePlatformFormModel> Platforms { get; set; }
    }
}
