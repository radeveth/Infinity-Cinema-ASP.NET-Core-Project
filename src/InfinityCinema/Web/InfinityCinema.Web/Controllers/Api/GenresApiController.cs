namespace InfinityCinema.Web.Controllers.Api
{
    using InfinityCinema.Services.Data.GenresService;
    using InfinityCinema.Services.Data.GenresService.Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;

    [ApiController]
    [Route("/api/genres/")]
    public class GenresApiController : BaseController
    {
        private readonly IGenreService genreService;

        public GenresApiController(IGenreService genreService)
        {
            this.genreService = genreService;
        }

        [HttpGet]
        [Route("all")]
        public ActionResult<IEnumerable<GenreViewModel>> All(string searchName)
        {
            return this.Json(this.genreService.All(searchName));
        }
    }
}
