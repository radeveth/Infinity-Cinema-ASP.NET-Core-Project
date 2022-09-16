namespace InfinityCinema.Services.Data.HomeService.Models
{
    using System.Collections.Generic;
    using InfinityCinema.Services.Data.ApplicationUserMoviesService.Models;
    using InfinityCinema.Services.Data.MoviesService.Models;

    public class IndexViewModel
    {
        public List<MovieHomeViewModel> TopThreeRatedMovies { get; set; }

        public IEnumerable<UserSavedMovieViewModel> SavdMoviesFromUser { get; set; }
    }
}
