namespace InfinityCinema.Services.Data.MoviesService.Models
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using InfinityCinema.Data.Models;
    using InfinityCinema.Services.Mapping;

    public class MovieListingViewModel : IMapFrom<Movie>, IHaveCustomMappings
    {
        public MovieListingViewModel()
        {
            this.Genres = new List<string>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<string> Genres { get; set; }

        public string ImageUrl { get; set; }

        public decimal StarRating { get; set; }

        public string Duration { get; set; }

        public int YearOfReleassed { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Movie, MovieListingViewModel>()
                .ForMember(x => x.ImageUrl, y => y.MapFrom(s => s.Images.Select(i => i.Url).FirstOrDefault()))
                .ForMember(x => x.Genres, y => y.MapFrom(s => s.MovieGenres.Select(m => m.Genre.Name)))
                .ForMember(x => x.YearOfReleassed, y => y.MapFrom(s => s.DateOfReleased.Year))
                .ForMember(x => x.StarRating, y => y
                    .MapFrom(s => s.MovieUserStarRatings.Count != 0 ? s.MovieUserStarRatings.Sum(r => r.Rate) / s.MovieUserStarRatings.Count : -1));
        }
    }
}
