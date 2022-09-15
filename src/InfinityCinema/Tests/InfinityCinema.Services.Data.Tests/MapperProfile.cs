namespace InfinityCinema.Services.Data.Tests
{
    using AutoMapper;
    using InfinityCinema.Data.Models;
    using InfinityCinema.Services.Data.ActorsService.Models;

    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            this.CreateMap<Actor, ActorViewModel>()
                .ForMember(x => x.FullName, y => y.MapFrom(s => $"{s.FirstName} {s.LastName}"));
        }
    }
}
