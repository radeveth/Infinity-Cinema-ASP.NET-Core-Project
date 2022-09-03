namespace InfinityCinema.Services.Data.MoviesService.Models
{
    using System.Collections.Generic;

    public class UserSavedMovieViewModel
    {
        public string UserId { get; set; }

        public int MovieId { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public IEnumerable<string> Genres { get; set; }

        public decimal StarRating { get; set; }

        public string Duration { get; set; }
    }
}
