namespace InfinityCinema.Services.Data.CountriesService.Models
{
    using InfinityCinema.Data.Models;
    using InfinityCinema.Services.Mapping;

    public class CountryViewModel : IMapFrom<Country>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Abbreviation { get; set; }
    }
}
