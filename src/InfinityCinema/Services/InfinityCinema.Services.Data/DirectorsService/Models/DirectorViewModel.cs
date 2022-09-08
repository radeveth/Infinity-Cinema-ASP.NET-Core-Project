namespace InfinityCinema.Services.Data.DirectorsService.Models
{
    using AutoMapper;
    using InfinityCinema.Data.Models;
    using InfinityCinema.Services.Mapping;

    public class DirectorViewModel : IMapFrom<Director>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string InformationLink { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Director, DirectorViewModel>()
                .ForMember(x => x.FullName, y => y.MapFrom(s => $"{s.FirstName} {s.LastName}"))
                .ForMember(x => x.InformationLink, y => y.MapFrom(s => s.InformationUrl));
        }
    }
}
