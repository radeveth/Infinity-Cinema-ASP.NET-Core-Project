namespace InfinityCinema.Services.Data.MoviesService
{
    using System.Collections.Generic;

    public class MoviesQueryServiceModel
    {
        public MoviesQueryServiceModel()
        {
            this.Movies = new List<MovieListingViewModel>();
        }

        public int TotalMovies { get; set; }

        public int CurrentPage { get; set; } = 1;

        public int MoviesPerPage { get; set; }

        public IEnumerable<MovieListingViewModel> Movies { get; set; }
    }
}
