namespace InfinityCinema.Web.Controllers
{
    using System.Collections.Generic;

    using InfinityCinema.Services.Data.GenresService;
    using InfinityCinema.Services.Data.GenresService.Models;
    using Microsoft.AspNetCore.Mvc;

    public class GenresController : BaseController
    {
        private readonly IGenreService genreService;

        public GenresController(IGenreService genreService)
        {
            this.genreService = genreService;
        }

        [HttpGet]
        public IActionResult All(string searchName = null)
        {
            IEnumerable<GenreViewModel> genres = this.genreService.All<GenreViewModel>(searchName);

            return this.View(genres);
        }
    }
}
