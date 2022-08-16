﻿namespace InfinityCinema.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using InfinityCinema.Data.Common.Models;
    using InfinityCinema.Data.Models.Enums;

    using static InfinityCinema.Data.Common.DataValidation.MovieValidation;

    public class Movie : BaseDeletableModel<int>
    {
        public Movie()
        {
            this.MovieGenres = new HashSet<MovieGenre>();
            this.MovieActors = new HashSet<MovieActor>();
            this.MoviePlatforms = new HashSet<MoviePlatform>();
            this.MovieLanguages = new HashSet<MovieLanguage>();
            this.StarRatings = new HashSet<StarRating>();
            this.Comments = new HashSet<MovieComment>();
        }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        public DateTime DateOfReleased { get; set; }

        // Not Required
        public Resolution Resolution { get; set; }

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }

        [MaxLength(TrailerPathMaxLength)]
        public string TrailerPath { get; set; }

        [Required]
        [MaxLength(ImageUrlMaxLength)]
        public string ImageUrl { get; set; }

        [Required]
        public TimeSpan Duration { get; set; }

        public virtual ICollection<MovieGenre> MovieGenres { get; set; }

        public virtual ICollection<MovieActor> MovieActors { get; set; }

        public virtual ICollection<MoviePlatform> MoviePlatforms { get; set; }

        public virtual ICollection<MovieLanguage> MovieLanguages { get; set; }

        public virtual ICollection<StarRating> StarRatings { get; set; }

        public virtual ICollection<MovieComment> Comments { get; set; }
    }
}