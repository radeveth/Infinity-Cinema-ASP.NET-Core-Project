namespace InfinityCinema.Services.Data.ActorsService
{
    using System.ComponentModel.DataAnnotations;

    using static InfinityCinema.Data.Common.DataValidation.ActorValidation;

    public class CreateActorFormModel
    {
        private const string FirstNameLengthErrorMessage = "The First Name field should be between {2} and {1} characters.";
        private const string LastNameLengthErrorMessage = "The Last Name field should be between {2} and {1} characters.";

        [Required]
        [Display(Name = "First Name")]
        [StringLength(FirstNameMaxLength, MinimumLength = FirstNameMinLength, ErrorMessage = FirstNameLengthErrorMessage)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(LastNameMaxLength, MinimumLength = LastNameMinLength, ErrorMessage = LastNameLengthErrorMessage)]
        public string LastName { get; set; }
    }
}
