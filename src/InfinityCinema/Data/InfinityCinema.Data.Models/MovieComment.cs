namespace InfinityCinema.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using InfinityCinema.Data.Common.Models;

    using static InfinityCinema.Data.Common.DataValidation.CommentValidation;

    public class MovieComment : BaseDeletableModel<int>
    {
        public MovieComment()
        {
            this.MovieUserComments = new HashSet<MovieUserComment>();
            this.UserComments = new HashSet<UserComment>();
        }

        [Required]
        [MaxLength(ContentMaxLength)]
        public string Content { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }

        [ForeignKey(nameof(ApplicationUser))]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public ICollection<MovieUserComment> MovieUserComments { get; set; }

        public ICollection<UserComment> UserComments { get; set; }
    }
}
