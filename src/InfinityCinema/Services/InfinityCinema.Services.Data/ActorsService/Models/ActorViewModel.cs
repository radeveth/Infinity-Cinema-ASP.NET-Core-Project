namespace InfinityCinema.Services.Data.ActorsService.Models
{
    using AutoMapper;
    using InfinityCinema.Data.Models;
    using InfinityCinema.Services.Mapping;

    public class ActorViewModel : IMapFrom<Actor>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string ImageUrl { get; set; }

        public string InformationLink { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Actor, ActorViewModel>()
                .ForMember(x => x.FullName, y => y.MapFrom(s => $"{s.FirstName} {s.LastName}"));
        }
    }
}
