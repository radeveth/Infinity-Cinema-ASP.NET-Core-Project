namespace InfinityCinema.Services.Data.MoviesService.Models.Api
{
    using InfinityCinema.Services.Data.MoviesService.Enums;

    public class AllMoviesApiRequestModel
    {
        public int MoviesPerPage { get; set; } = 10;

        public int TotalMovies { get; set; }

        public int CurrentPage { get; set; } = 1;

        public MovieSorting Sorting { get; set; }

        public string SearchName { get; set; }

        public string SearchGenre { get; set; } = "all";
    }
}
