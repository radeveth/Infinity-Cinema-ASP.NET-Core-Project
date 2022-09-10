namespace InfinityCinema.Services.Data.ForumSystem.PostsService.Models
{
    using System;
    using System.Collections.Generic;

    using AutoMapper;
    using Ganss.XSS;
    using InfinityCinema.Data.Models.ForumSystem;
    using InfinityCinema.Services.Data.ForumSystem.CommentsService.Models;
    using InfinityCinema.Services.Mapping;

    public class PostViewModel : IMapFrom<Post>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string SanitizedContent => new HtmlSanitizer().Sanitize(this.Content);

        public string PastTime { get; set; }

        public int CommentsCount { get; set; }

        public IEnumerable<CommentViewModel> Comments { get; set; }

        public ApplicationUserInPostViewModel User { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Post, PostViewModel>()
                .ForMember(x => x.PastTime, y =>
                    y.MapFrom(s => $"{(DateTime.UtcNow.Subtract(s.CreatedOn).Days != 0
                        ? $"{DateTime.UtcNow.Subtract(s.CreatedOn).Days} days ago"
                        : (DateTime.UtcNow.Subtract(s.CreatedOn).Hours != 0
                            ? $"{DateTime.UtcNow.Subtract(s.CreatedOn).Hours} hours ago"
                            : $"{DateTime.UtcNow.Subtract(s.CreatedOn).Minutes} minutes ago"))}".ToString()));
        }
    }
}
