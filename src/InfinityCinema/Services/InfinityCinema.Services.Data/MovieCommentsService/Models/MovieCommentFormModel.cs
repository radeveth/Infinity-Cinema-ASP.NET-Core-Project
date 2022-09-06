namespace InfinityCinema.Services.Data.MovieCommentsService.Models
{
    using System.ComponentModel.DataAnnotations;

    using static InfinityCinema.Data.Common.DataValidation.CommentValidation;

    public class MovieCommentFormModel
    {
        [Required]
        [StringLength(ContentMaxLength)]
        public string Content { get; set; }

        public string UserId { get; set; }
    }
}
