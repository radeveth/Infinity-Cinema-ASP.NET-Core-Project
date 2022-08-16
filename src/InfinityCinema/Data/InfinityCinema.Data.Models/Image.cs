namespace InfinityCinema.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using InfinityCinema.Data.Common.Models;

    using static InfinityCinema.Data.Common.DataValidation.ImageValidation;

    public class Image : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(UrlMaxLength)]
        public string Url { get; set; }

        [ForeignKey(nameof(Movie))]
        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; }

        // (optional) enumeration of image extension
    }
}
