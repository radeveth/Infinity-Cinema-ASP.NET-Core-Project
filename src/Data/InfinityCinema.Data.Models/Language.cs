namespace InfinityCinema.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using InfinityCinema.Data.Common.Models;

    using static InfinityCinema.Data.Common.DataValidation.LanguageValidation;

    public class Language : BaseDeletableModel<int>
    {
        public Language()
        {
            this.MovieLanguages = new HashSet<MovieLanguage>();
        }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(AbbreviationMaxLength)]
        public string Abbreviation { get; set; }

        public virtual ICollection<MovieLanguage> MovieLanguages { get; set; }
    }
}
