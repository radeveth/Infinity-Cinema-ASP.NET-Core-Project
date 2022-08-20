namespace InfinityCinema.Services.Data.CountriesService
{
    using System.ComponentModel.DataAnnotations;

    using static InfinityCinema.Data.Common.DataValidation.CountryValidation;

    public class CountryFormModel
    {
        private const string CountryNameLengthErrorMessage = "The Country Name field should be between {2} and {1} characters.";

        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength, ErrorMessage = CountryNameLengthErrorMessage)]
        // [CountryExistAttrubute(ErrorMessage = "The given country does not exist!")]
        public string Name { get; set; }
    }
}
