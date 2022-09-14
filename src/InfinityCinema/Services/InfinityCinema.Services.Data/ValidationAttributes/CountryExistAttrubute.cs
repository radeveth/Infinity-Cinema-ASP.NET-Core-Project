namespace InfinityCinema.Services.Data.ValidationAttributes
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Linq;

    public class CountryExistAttrubute : ValidationAttribute
    {
        private const string TxtFilePath = @"C:\Users\User\Documents\GitHub\Infinity-Cinema-ASP.NET-Core-Project\src\InfinityCinema\Services\InfinityCinema.Services.Data\CountriesService\Abbreviation-Country.txt";

        public override bool IsValid(object value)
        {
            value = value.ToString().Replace(" ", string.Empty).ToLower();

            string file = File.ReadAllText(TxtFilePath);

            string[] countriesAbbreviation = file
                .Split("|", StringSplitOptions.RemoveEmptyEntries)
                .ToArray();

            bool isCountryExist = false;
            foreach (var country in countriesAbbreviation)
            {
                string[] countryAbbreviationParts = country
                    .Split("-", StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();
                string currCountryName = countryAbbreviationParts[1].Replace(" ", string.Empty).ToLower();

                if (currCountryName == value)
                {
                    isCountryExist = true;
                    return true;
                }
            }

            if (isCountryExist)
            {
                return true;
            }

            return false;
        }
    }
}
