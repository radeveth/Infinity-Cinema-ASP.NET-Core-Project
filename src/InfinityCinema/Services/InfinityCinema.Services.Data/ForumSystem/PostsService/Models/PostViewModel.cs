namespace InfinityCinema.Services.Data.ForumSystem.PostsService.Models
{
    using System;

    using AutoMapper;
    using InfinityCinema.Data.Models.ForumSystem;
    using InfinityCinema.Services.Mapping;

    public class PostViewModel : IMapFrom<Post>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string PastDays { get; set; }

        public int CommentsCount { get; set; }

        public ApplicationUserInPostViewModel User { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Post, PostViewModel>()
                .ForMember(x => x.PastDays, y => y.MapFrom(s => $"{(DateTime.UtcNow - s.CreatedOn).Days}"));
        }
    }
}
