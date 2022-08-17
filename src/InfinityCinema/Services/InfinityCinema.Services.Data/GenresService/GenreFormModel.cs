namespace InfinityCinema.Services.Data.GenresService
{
    using System.ComponentModel.DataAnnotations;

    using static InfinityCinema.Data.Common.DataValidation.GenreValidation;

    public class GenreFormModel
    {
        private const string ImageUrlMaxLengthErrorMessage = "The Genre Image Url cannot be more than {1} symbols.";
        private const string NameLengthErrorMessage = "The Genre Name field should be between {2} and {1} characters.";

        public int Id { get; set; }

        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength, ErrorMessage = NameLengthErrorMessage)]
        public string Name { get; set; }

        // [Display(Name = "Genre Image")]
        // [StringLength(ImageUrlMaxLength, ErrorMessage = ImageUrlMaxLengthErrorMessage)]
        // public string GenreImaqgeUrl { get; set; }
    }
}
