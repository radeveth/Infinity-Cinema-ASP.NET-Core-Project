namespace InfinityCinema.Services.Data.ForumSystem.CategoriesService.Models
{
    using AutoMapper;
    using InfinityCinema.Data.Models.ForumSystem;
    using InfinityCinema.Services.Mapping;

    using System;

    public class PostInCategoryViewModel : IMapFrom<Post>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string PastDays { get; set; }

        public int CommentsCount { get; set; }

        public ApplicationUserInPostViewModel User { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Post, PostInCategoryViewModel>()
                .ForMember(x => x.PastDays, y => y.MapFrom(s => $"{(DateTime.UtcNow - s.CreatedOn).Days}"));
        }
    }
}
