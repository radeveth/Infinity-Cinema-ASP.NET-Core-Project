namespace InfinityCinema.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using InfinityCinema.Data.Common.Models;

    using static InfinityCinema.Data.Common.DataValidation.PlatformValidation;

    public class Platform : BaseDeletableModel<int>
    {
        public Platform()
        {
            this.MoviePlatforms = new HashSet<MoviePlatform>();
        }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(PathUrlMaxLength)]
        public string PathUrl { get; set; }

        [Required]
        [MaxLength(IconUrlMaxLength)]
        public string IconUrl { get; set; }

        public virtual ICollection<MoviePlatform> MoviePlatforms { get; set; }
    }
}
