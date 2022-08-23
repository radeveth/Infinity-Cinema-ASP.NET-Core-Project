namespace InfinityCinema.Services.Data.DirectorsService.Models
{
    using System.ComponentModel.DataAnnotations;

    using static InfinityCinema.Data.Common.DataValidation.DirectorValidation;

    public class DirectorFormModel
    {
        private const string DirectorFullNameLengthErrorMessage = "The Director Name field should be between {2} and {1} characters.";

        [Required]
        [StringLength(FullNameMaxLength, MinimumLength = FullNameMinLength, ErrorMessage = DirectorFullNameLengthErrorMessage)]
        public string FullName { get; set; }
    }
}
