namespace InfinityCinema.Services.Data.MoviesService.Models
{
    using InfinityCinema.Services.Data.ActorsService.Models;
    using InfinityCinema.Services.Data.ImagesService.Models;
    using InfinityCinema.Services.Data.PlatformsService.Models;

    public class EditMovieServiceModel
    {
        public MovieFormModel OverallMovieInformation { get; set; }

        public EditActorsFormModel Actors { get; set; }

        public EditImagesFormModel Images { get; set; }

        public EditPlatformsFormModel Platforms { get; set; }
    }
}
