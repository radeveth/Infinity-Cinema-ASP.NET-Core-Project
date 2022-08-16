namespace InfinityCinema.Data.Models
{
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using InfinityCinema.Data.Common.Models;

    using static InfinityCinema.Data.Common.DataValidation.DirectorValidation;

    public class Director : BaseDeletableModel<int>
    {
        public Director()
        {
            this.DirectorMovies = new HashSet<DirectorMovie>();
        }

        [Required]
        [MaxLength(FirstNameMaxLength)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(LastNameMaxLength)]
        public string LastName { get; set; }

        public virtual ICollection<DirectorMovie> DirectorMovies { get; set; }
    }
}
