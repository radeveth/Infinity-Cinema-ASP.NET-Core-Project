namespace InfinityCinema.Services.Data.PlatformsService.Models
{
    using AutoMapper;
    using InfinityCinema.Data.Models;
    using InfinityCinema.Services.Mapping;

    public class PlatformViewModel : IMapFrom<Platform>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Icon { get; set; }

        public string SiteUrl { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Platform, PlatformViewModel>()
                .ForMember(x => x.SiteUrl, y => y.MapFrom(s => s.PathUrl));
        }
    }
}
