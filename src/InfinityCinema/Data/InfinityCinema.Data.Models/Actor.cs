namespace InfinityCinema.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using InfinityCinema.Data.Common.Models;

    using static InfinityCinema.Data.Common.DataValidation.ActorValidation;

    public class Actor : BaseDeletableModel<int>
    {
        public Actor()
        {
            this.MovieActors = new HashSet<MovieActor>();
        }

        [Required]
        [MaxLength(FirstNameMaxLength)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(LastNameMaxLength)]
        public string LastName { get; set; }

        [MaxLength(ImageUrlMaxLength)]
        public string ImageUrl { get; set; }

        public virtual ICollection<MovieActor> MovieActors { get; set; }
    }
}
