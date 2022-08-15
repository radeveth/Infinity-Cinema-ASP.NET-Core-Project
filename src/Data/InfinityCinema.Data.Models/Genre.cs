namespace InfinityCinema.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using InfinityCinema.Data.Common.Models;

    using static InfinityCinema.Data.Common.DataValidation.GenreValidation;

    public class Genre : BaseDeletableModel<int>
    {
        public Genre()
        {
            this.MovieGenres = new HashSet<MovieGenre>();
        }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [MaxLength(ImageUrlMaxLength)]
        public string ImageUrl { get; set; }

        public virtual ICollection<MovieGenre> MovieGenres { get; set; }
    }
}
