namespace InfinityCinema.Services.Data.PlatformsService.Models
{
    using System.ComponentModel.DataAnnotations;

    using static InfinityCinema.Data.Common.DataValidation.PlatformValidation;

    public class PlatformFormModel
    {
        private const string NameErrorMessage = "The Platform Name field should be between {2} and {1} characters.";
        private const string SitePathMaxLengthErrorMessage = "The Site Path cannot be more than {1} symbols.";
        private const string IconUrlMaxLengthErrorMessage = "The Image Url cannot be more than {1} symbols.";

        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength, ErrorMessage = NameErrorMessage)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Site Url")]
        [StringLength(PathUrlMaxLength, ErrorMessage = SitePathMaxLengthErrorMessage)]
        public string PathUrl { get; set; }

        [Required]
        [Display(Name = "Icon Url")]
        [StringLength(IconUrlMaxLength, ErrorMessage = IconUrlMaxLengthErrorMessage)]
        public string IconUrl { get; set; }
    }
}
