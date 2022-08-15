namespace InfinityCinema.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using InfinityCinema.Data.Common.Models;

    public class MoviePlatform : IDeletableEntity
    {
        [Required]
        [ForeignKey(nameof(Movie))]
        public int MovieId { get; set; }

        public Movie Movie { get; set; }

        [Required]
        [ForeignKey(nameof(Platform))]
        public int PlatformId { get; set; }

        public Platform Platform { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
