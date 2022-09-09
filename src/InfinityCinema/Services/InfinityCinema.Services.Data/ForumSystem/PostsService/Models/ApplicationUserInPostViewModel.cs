namespace InfinityCinema.Services.Data.ForumSystem.PostsService.Models
{
    using AutoMapper;
    using InfinityCinema.Data.Models;
    using InfinityCinema.Services.Mapping;

    public class ApplicationUserInPostViewModel : IMapFrom<ApplicationUser>, IHaveCustomMappings
    {
        public string FullName { get; set; }

        public string CreatedOn { get; set; }

        public int PostsCount { get; set; }

        public string Gender { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ApplicationUser, ApplicationUserInPostViewModel>()
                .ForMember(x => x.CreatedOn, y => y.MapFrom(s => $"{s.CreatedOn.Day}/{s.CreatedOn.Month}/{s.CreatedOn.Year}"));
        }
    }
}