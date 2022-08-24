﻿namespace InfinityCinema.Services.Data.ActorsService.Models
{
    using System.ComponentModel.DataAnnotations;

    using static InfinityCinema.Data.Common.DataValidation.ActorValidation;

    public class ActorFormModel
    {
        private const string FullNameLengthErrorMessage = "The Full Name field should be between {2} and {1} characters.";

        [Required]
        [Display(Name = "Full Name")]
        [StringLength(FirstNameMaxLength, MinimumLength = FirstNameMinLength, ErrorMessage = FullNameLengthErrorMessage)]
        public string FullName { get; set; }

        [Url]
        [StringLength(ImageUrlMaxLength)]
        public string ImageUrl { get; set; }
    }
}
