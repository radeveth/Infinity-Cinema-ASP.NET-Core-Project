﻿namespace InfinityCinema.Services.Data.ImagesService.Models
{
    using System.ComponentModel.DataAnnotations;

    using static InfinityCinema.Data.Common.DataValidation.ImageValidation;

    public class ImageFormModel
    {
        private const string ImageUrlMaxLengthErrorMessage = "The Image Url cannot be more than {1} symbols.";

        [Url]
        [Required]
        [Display(Name = "Image Url")]
        [StringLength(UrlMaxLength, ErrorMessage = ImageUrlMaxLengthErrorMessage)]
        public string ImageUrl { get; set; }

        public int MovieId { get; set; }
    }
}
