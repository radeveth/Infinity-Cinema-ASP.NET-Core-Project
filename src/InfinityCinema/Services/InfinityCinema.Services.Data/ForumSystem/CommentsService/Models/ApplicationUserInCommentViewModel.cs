namespace InfinityCinema.Services.Data.ForumSystem.CommentsService.Models
{
    using System;

    using InfinityCinema.Data.Models;
    using InfinityCinema.Services.Mapping;

    public class ApplicationUserInCommentViewModel : IMapFrom<ApplicationUser>
    {
        public string FullName { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
