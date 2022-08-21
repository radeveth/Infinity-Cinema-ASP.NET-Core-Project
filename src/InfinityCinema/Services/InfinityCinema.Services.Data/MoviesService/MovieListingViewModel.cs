namespace InfinityCinema.Services.Data.MoviesService
{
    using System.Collections.Generic;

    public class MovieListingViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<string> Genres { get; set; }

        public string ImageUrl { get; set; }

        public int StarRating { get; set; }

        public string Duration { get; set; }

        public int YearOfReleassed { get; set; }
    }
}
