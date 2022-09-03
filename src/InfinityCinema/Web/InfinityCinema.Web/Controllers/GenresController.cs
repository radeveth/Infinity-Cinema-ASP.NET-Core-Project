namespace InfinityCinema.Web.Controllers
{
    using System.Collections.Generic;

    using InfinityCinema.Services.Data.GenresService;
    using InfinityCinema.Services.Data.GenresService.Models;
    using Microsoft.AspNetCore.Mvc;

    public class GenresController : Controller
    {
        private readonly IGenreService genreService;

        public GenresController(IGenreService genreService)
        {
            this.genreService = genreService;
        }

        public IActionResult All()
        {
            IEnumerable<GenreViewModel> genres = this.genreService.All();

            return this.View(genres);
        }
    }
}
