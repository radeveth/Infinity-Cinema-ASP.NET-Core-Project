namespace InfinityCinema.Services.Data.LanguagesService.Models
{
    using InfinityCinema.Data.Models;
    using InfinityCinema.Services.Mapping;

    public class LanguageViewModel : IMapFrom<Language>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
