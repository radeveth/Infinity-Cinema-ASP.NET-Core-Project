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
            this.MovieCommentVotes = new HashSet<MovieCommentVote>();
        }

        [Required]
        [MaxLength(ContentMaxLength)]
        public string Content { get; set; }

        [ForeignKey(nameof(ApplicationUser))]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public ICollection<MovieUserComment> MovieUserComments { get; set; }

        public ICollection<MovieCommentVote> MovieCommentVotes { get; set; }
    }
}
