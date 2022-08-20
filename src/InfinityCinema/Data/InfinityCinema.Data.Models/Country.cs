namespace InfinityCinema.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using InfinityCinema.Data.Common.Models;

    using static InfinityCinema.Data.Common.DataValidation.CountryValidation;

    public class Country : BaseDeletableModel<int>
    {
        public Country()
        {
            this.Movies = new HashSet<Movie>();
        }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [MaxLength(AbbreviationMaxLength)]
        public string Abbreviation { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }
    }
}
