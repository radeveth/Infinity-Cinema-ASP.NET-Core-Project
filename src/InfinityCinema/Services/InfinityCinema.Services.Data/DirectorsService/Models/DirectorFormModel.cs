namespace InfinityCinema.Services.Data.DirectorsService.Models
{
    using System.ComponentModel.DataAnnotations;

    using static InfinityCinema.Data.Common.DataValidation.DirectorValidation;

    public class DirectorFormModel
    {
        private const string DirectorFullNameLengthErrorMessage = "The Director Name field should be between {2} and {1} characters.";

        [Required]
        [StringLength(FullNameMaxLength, MinimumLength = FullNameMinLength, ErrorMessage = DirectorFullNameLengthErrorMessage)]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Url]
        [Required]
        [StringLength(InformationUrlMaxLength)]
        [Display(Name = "Information Link")]
        public string InformationUrl { get; set; }
    }
}
