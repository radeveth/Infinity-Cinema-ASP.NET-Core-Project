namespace InfinityCinema.Services.Data.ValidationAttributes
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class YearMaxValueAttribute : ValidationAttribute
    {
        public YearMaxValueAttribute(int minYear, string errorMessage = null)
        {
            this.MinYear = minYear;
            this.ErrorMessage = errorMessage switch
            {
                null => $"Value should be between {minYear} and {DateTime.UtcNow.Year}.",
                _ => string.Format(errorMessage, this.MinYear, DateTime.UtcNow.Year),
            };
        }

        public YearMaxValueAttribute()
        {
            this.MinYear = DateTime.UtcNow.Year - 130;
        }

        public int MinYear { get; }

        public override bool IsValid(object value)
        {
            if (value is int intValue)
            {
                if (intValue <= DateTime.UtcNow.Year
                    && intValue >= this.MinYear)
                {
                    return true;
                }
            }

            if (value is DateTime dtValue)
            {
                if (dtValue.Year <= DateTime.UtcNow.Year
                    && dtValue.Year >= this.MinYear)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
