namespace InfinityCinema.Services.Data.MoviesService
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using InfinityCinema.Services.Data.MoviesService.Enums;

    public class AllMoviesQueryModel
    {
        public const int MoviesPerPage = 9;

        public int TotalMovies { get; set; }

        public int CurrentPage { get; set; }

        [Display(Name = "")]
        public MovieSorting Sorting { get; set; }

        [Display(Name = "")]
        public string SearchName { get; set; }

        [Display(Name = "")]
        public string Genre { get; set; }

        public IEnumerable<MovieListingViewModel> Movies { get; set; }
    }
}
