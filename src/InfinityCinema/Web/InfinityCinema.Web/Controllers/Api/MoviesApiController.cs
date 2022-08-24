﻿namespace InfinityCinema.Web.Controllers.Api
{
    using InfinityCinema.Services.Data.MoviesService;
    using Microsoft.AspNetCore.Mvc;

    [Route("/api/{controller}/")]
    [ApiController]
    public class MoviesApiController : BaseController
    {
        private readonly IMovieService movieService;

        public MoviesApiController(IMovieService movieService)
        {
            this.movieService = movieService;
        }

        //[Route("{action}")]
        //public ActionResult<AllMoviesQueryModel> All(string genre)
        //{

        //}
    }
}
