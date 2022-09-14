namespace InfinityCinema.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    using InfinityCinema.Data.Common.Models;
    using InfinityCinema.Data.Models.Enums;

    public class MovieCommentVote : BaseModel<int>
    {
        [ForeignKey(nameof(MovieComment))]
        public int MovieCommentId { get; set; }

        public virtual MovieComment MovieComment { get; set; }

        [ForeignKey(nameof(ApplicationUser))]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public VoteType Vote { get; set; }
    }
}
