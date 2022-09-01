namespace InfinityCinema.Services.Data.MoviesService.Models
{
    using System.ComponentModel.DataAnnotations;

    public class DeleteMovieServiceModel
    {
        [Display(Name = "Movie Id")]
        [StringLength(10)]
        public int Id { get; set; }
    }
}
