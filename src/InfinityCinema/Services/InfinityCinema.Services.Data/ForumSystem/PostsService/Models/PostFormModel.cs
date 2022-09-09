namespace InfinityCinema.Services.Data.ForumSystem.PostsService.Models
{
    using System.ComponentModel.DataAnnotations;

    using static InfinityCinema.Data.Common.DataValidation.ForumPostValidation;

    public class PostFormModel
    {
        [Required]
        [StringLength(TitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [StringLength(ContentMaxLength)]
        public string Content { get; set; }

        public string UserId { get; set; }

        public int CategoryId { get; set; }
    }
}
