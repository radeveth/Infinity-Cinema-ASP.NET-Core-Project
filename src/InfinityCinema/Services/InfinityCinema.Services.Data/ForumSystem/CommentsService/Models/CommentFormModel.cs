namespace InfinityCinema.Services.Data.ForumSystem.CommentsService.Models
{
    using System.ComponentModel.DataAnnotations;

    using static InfinityCinema.Data.Common.DataValidation.CommentValidation;

    public class CommentFormModel
    {
        [Required]
        [StringLength(ContentMaxLength)]
        public string Content { get; set; }

        [Required]
        public int PostId { get; set; }

        [Required]
        public string UserId { get; set; }

        public int? ParentId { get; set; }
    }
}
