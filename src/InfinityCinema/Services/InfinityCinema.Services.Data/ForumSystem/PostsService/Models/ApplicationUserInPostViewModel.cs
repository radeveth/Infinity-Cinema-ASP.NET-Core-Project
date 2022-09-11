namespace InfinityCinema.Services.Data.ForumSystem.PostsService.Models
{
    using System;

    using InfinityCinema.Data.Models;
    using InfinityCinema.Services.Mapping;

    public class ApplicationUserInPostViewModel : IMapFrom<ApplicationUser>
    {
        public string FullName { get; set; }

        public DateTime CreatedOn { get; set; }

        public int PostsCount { get; set; }

        public string Gender { get; set; }
    }
}