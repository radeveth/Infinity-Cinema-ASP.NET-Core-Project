namespace InfinityCinema.Services.Data.MoviesService.Models
{
    using AutoMapper;
    using InfinityCinema.Data.Models;
    using InfinityCinema.Services.Mapping;

    using System.Linq;

    public class MovieHomeViewModel : IMapFrom<Movie>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Movie, MovieHomeViewModel>()
                .ForMember(x => x.ImageUrl, y => y.MapFrom(s => s.Images.Select(i => i.Url).FirstOrDefault()));
        }
    }
}
