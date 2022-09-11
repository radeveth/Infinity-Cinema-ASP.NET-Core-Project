namespace InfinityCinema.Data.Models.ForumSystem
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using InfinityCinema.Data.Common.Models;

    using static InfinityCinema.Data.Common.DataValidation.ForumCommentValidation;

    public class Comment : BaseModel<int>
    {
        [Required]
        [MaxLength(ContentMaxLength)]
        public string Content { get; set; }

        [Required]
        [ForeignKey(nameof(Post))]
        public int PostId { get; set; }

        public virtual Post Post { get; set; }

        [Required]
        [ForeignKey(nameof(ApplicationUser))]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        [ForeignKey(nameof(Comment))]
        public int? ParentId { get; set; }

        public virtual Comment Parent { get; set; }
    }
}
