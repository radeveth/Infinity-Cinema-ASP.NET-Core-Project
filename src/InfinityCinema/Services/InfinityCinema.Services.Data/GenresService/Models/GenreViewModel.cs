namespace InfinityCinema.Services.Data.GenresService.Models
{
    using InfinityCinema.Data.Models;
    using InfinityCinema.Services.Mapping;

    public class GenreViewModel : IMapFrom<Genre>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }
    }
}
