namespace InfinityCinema.Services.Data.MoviesService
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using InfinityCinema.Data.Models.Enums;
    using InfinityCinema.Services.Data.DirectorsService;
    using InfinityCinema.Services.Data.GenresService;
    using InfinityCinema.Services.Data.ValidationAttributes;

    using static InfinityCinema.Data.Common.DataValidation;

    public class MovieFormModel
    {
        private const string MovieNameErrorMessage = "The Mouvie Name field should be between {2} and {1} characters.";
        private const string DateOfReleasedMaxValueErrorMessage = "The Date of Released field should be between {0} and {1}.";
        private const string TrailerPathMaxLengthErrorMessage = "The Trailer Path cannot be more than {1} symbols.";
        private const string DurationLengthErrorMessage = "The Duration field should be between {2} and {1} characters.";
        private const string LanguageMaxLengthErrorMessage = "The Language field must be smaller than {1}";
        private const string CountryNameLengthErrorMessage = "The Country Name field should be between {2} and {1} characters.";
        private DateTime dateOfReleased = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day);

        [Required]
        [Display(Name = "Movie")]
        [StringLength(MovieValidation.NameMaxLength, MinimumLength = MovieValidation.NameMinLength, ErrorMessage = MovieNameErrorMessage)]
        public string Name { get; set; }

        [Display(Name = "Genre")]
        public int[] GenresId { get; set; }

        public IEnumerable<GenreFormModel> Genres { get; set; }

        [Required]
        [MaxLength(MovieValidation.DescriptionMaxLength)]
        public string Description { get; set; }

        public DirectorFormModel Director { get; set; }

        [Url]
        [Display(Name = "Trailer Path")]
        [StringLength(MovieValidation.TrailerPathMaxLength, ErrorMessage = TrailerPathMaxLengthErrorMessage)]
        public string TrailerPath { get; set; }

        [Required]
        [Display(Name = "Date of Released")]
        [YearMaxValue(1888, DateOfReleasedMaxValueErrorMessage)] // In 1888 is released first movie
        public DateTime DateOfReleased
        {
            get => this.dateOfReleased;
            set => this.dateOfReleased = value;
        }

        public Resolution Resolution { get; set; }

        [Required]
        [StringLength(MovieValidation.DurationAsStringMaxLength, MinimumLength = MovieValidation.DurationAsStringMinLength, ErrorMessage = DurationLengthErrorMessage)]
        public string Duration { get; set; } // Can be with TimeSpan type

        [StringLength(LanguageValidation.NameMaxLength, ErrorMessage = LanguageMaxLengthErrorMessage)]
        public string Language { get; set; }

        [Required]
        [StringLength(CountryValidation.NameMaxLength, MinimumLength = CountryValidation.NameMinLength, ErrorMessage = CountryNameLengthErrorMessage)]
        public string Country { get; set; }
    }
}
