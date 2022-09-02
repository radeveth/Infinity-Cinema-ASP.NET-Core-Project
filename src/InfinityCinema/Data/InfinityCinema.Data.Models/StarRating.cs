﻿namespace InfinityCinema.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using InfinityCinema.Data.Common.Models;

    using static InfinityCinema.Data.Common.DataValidation.StarRatingValidation;

    public class StarRating : BaseDeletableModel<int>
    {
        [Required]
        [Range(0, StarMaLength)]
        public decimal Rate { get; set; }

        [Required]
        [ForeignKey(nameof(Movie))]
        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; }
    }
}
