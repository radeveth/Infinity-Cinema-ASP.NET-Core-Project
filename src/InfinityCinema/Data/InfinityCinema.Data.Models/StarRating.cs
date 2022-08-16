namespace InfinityCinema.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using InfinityCinema.Data.Common.Models;

    public class StarRating : BaseDeletableModel<int>
    {
        [Required]
        public int Rate { get; set; }

        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; }
    }
}
