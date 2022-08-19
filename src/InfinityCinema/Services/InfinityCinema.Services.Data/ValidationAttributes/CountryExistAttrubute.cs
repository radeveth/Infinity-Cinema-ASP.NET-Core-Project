namespace InfinityCinema.Services.Data.ValidationAttributes
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Linq;

    public class CountryExistAttrubute : ValidationAttribute
    {
        private string countryName;

        public CountryExistAttrubute(string countryName)
        {
            this.countryName = countryName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string file = File.ReadAllText(@"C:\Users\User\Documents\GitHub\Infinity-Cinema-ASP.NET-Core-Project\src\InfinityCinema\Services\InfinityCinema.Services.Data\CountriesService\Abbreviation-Country.txt");

            string[] countries = file
                .Split("|", StringSplitOptions.RemoveEmptyEntries)
                .ToArray();

            string targetCountry = null;
            foreach (string country in countries)
            {
                try
                {
                    // Get only whole country name
                    int startIndex = country.IndexOf("\"") + 1;
                    int length = country.LastIndexOf("\"") - startIndex;

                    string currentCountryName = country.Substring(startIndex, length).ToLower().Trim();

                    // Check if the country exist
                    if (currentCountryName == this.countryName)
                    {
                        // If country exist get the particular country and break the loop
                        targetCountry = country;
                        return ValidationResult.Success;
                    }
                }
                catch (Exception)
                {
                    return new ValidationResult(this.ErrorMessage = "Invalid country name");
                }
            }

            if (string.IsNullOrEmpty(targetCountry))
            {
                return new ValidationResult(this.ErrorMessage = "Invalid country name");
            }

            return ValidationResult.Success;
        }
    }
}
