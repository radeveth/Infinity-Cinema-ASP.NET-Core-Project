namespace InfinityCinema.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using InfinityCinema.Data.Common.Models;

    using static InfinityCinema.Data.Common.DataValidation.DirectorValidation;

    public class Director : BaseDeletableModel<int>
    {
        public Director()
        {
            this.Movies = new HashSet<Movie>();
        }

        [Required]
        [MaxLength(FirstNameMaxLength)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(LastNameMaxLength)]
        public string LastName { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }
    }
}
