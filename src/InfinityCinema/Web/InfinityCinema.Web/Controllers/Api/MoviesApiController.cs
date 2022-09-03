namespace InfinityCinema.Web.Controllers.Api
{
    using InfinityCinema.Services.Data.MoviesService;
    using InfinityCinema.Services.Data.MoviesService.Models;
    using Microsoft.AspNetCore.Mvc;

    [Route("/api/movies/")]
    [ApiController]
    public class MoviesApiController : BaseController
    {
        private readonly IMovieService movieService;

        public MoviesApiController(IMovieService movieService)
        {
            this.movieService = movieService;
        }

        [Route("all")]
        public ActionResult<AllMoviesQueryModel> All([FromQuery] AllMoviesQueryModel moviesQueryModel)
        {
            AllMoviesQueryModel queryResult = this.movieService
                .All(moviesQueryModel.SearchName, moviesQueryModel.Sorting, moviesQueryModel.CurrentPage, AllMoviesQueryModel.MoviesPerPage, moviesQueryModel.SearchGenre);

            moviesQueryModel.Movies = queryResult.Movies;
            moviesQueryModel.TotalMovies = queryResult.TotalMovies;
            moviesQueryModel.CurrentPage = queryResult.CurrentPage;
            moviesQueryModel.SearchGenre = queryResult.SearchGenre;

            return this.Json(moviesQueryModel);
        }
    }
}
