namespace InfinityCinema.Data.Models.ForumSystem
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using InfinityCinema.Data.Common.Models;
    using InfinityCinema.Data.Models.Enums;

    public class Vote : BaseModel<int>
    {
        [ForeignKey(nameof(Post))]
        public int PostId { get; set; }

        public Post Post { get; set; }

        [Required]
        [ForeignKey(nameof(ApplicationUser))]
        public string UserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public VoteType Type { get; set; }
    }
}
