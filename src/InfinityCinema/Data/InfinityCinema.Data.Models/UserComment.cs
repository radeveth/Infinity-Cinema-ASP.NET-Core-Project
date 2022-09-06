namespace InfinityCinema.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    using InfinityCinema.Data.Common.Models;

    public class UserComment : IDeletableEntity
    {
        [ForeignKey(nameof(ApplicationUser))]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        [ForeignKey(nameof(MovieComment))]
        public int CommentId { get; set; }

        public MovieComment Comment { get; set; }

        public bool IsUserIsVoted { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
