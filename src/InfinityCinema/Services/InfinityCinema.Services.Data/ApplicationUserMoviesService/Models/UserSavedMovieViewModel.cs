namespace InfinityCinema.Services.Data.ApplicationUserMoviesService.Models
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using InfinityCinema.Data.Models;
    using InfinityCinema.Services.Mapping;

    public class UserSavedMovieViewModel : IMapFrom<Movie>, IHaveCustomMappings
    {
        public string UserId { get; set; }

        public int MovieId { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public IEnumerable<string> Genres { get; set; }

        public decimal StarRating { get; set; }

        public string Duration { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Movie, UserSavedMovieViewModel>()
                .ForMember(x => x.ImageUrl, y => y.MapFrom(s => s.Images.Select(i => i.Url).FirstOrDefault()))
                .ForMember(x => x.MovieId, y => y.MapFrom(s => s.Id))
                .ForMember(x => x.Genres, y => y.MapFrom(s => s.MovieGenres.Select(m => m.Genre.Name)))
                .ForMember(x => x.StarRating, y => y.MapFrom(s => s.MovieUserStarRatings.Count != 0 ?
                           s.MovieUserStarRatings.Sum(m => m.Rate) / s.MovieUserStarRatings.Count : -1));
        }
    }
}
