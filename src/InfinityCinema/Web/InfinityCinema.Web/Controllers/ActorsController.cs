namespace InfinityCinema.Web.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Http;

    using InfinityCinema.Common;
    using InfinityCinema.Services.Data.ActorsService;
    using InfinityCinema.Services.Data.ActorsService.Models;
    using InfinityCinema.Services.Data.MovieActorsService;
    using InfinityCinema.Services.Data.MoviesService;
    using Microsoft.AspNetCore.Mvc;

    using HttpPostAttribute = System.Web.Http.HttpPostAttribute;

    public class ActorsController : BaseController
    {
        private readonly IActorService actorService;
        private readonly IMovieActorService movieActorService;

        public ActorsController(IActorService actorService, IMovieActorService movieActorService)
        {
            this.actorService = actorService;
            this.movieActorService = movieActorService;
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> DeleteAsync(int actorId, int movieId)
        {
            await this.movieActorService.RemoveRelationBetweenMovieActorsAndActosTablesAsync(actorId, movieId);
            await this.actorService.DeleteAsync(actorId);

            return this.RedirectToAction("EditMovieActors", "Movies", new { movieId = movieId });
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> CreateAsync(ActorFormModel actorForm)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("EditMovieActors", "Movies", new { movieId = actorForm.MovieId, newActor = actorForm });
            }

            ActorViewModel actor = this.actorService.GetActorByNames(actorForm.FullName);

            if (actor == null)
            {
                actor = await this.actorService.CreateAsync<ActorViewModel>(actorForm);
            }

            await this.actorService.CreateRowForMappingTableMovieActorsAsync(actorForm.MovieId, actor.Id);

            return this.RedirectToAction("EditMovieActors", "Movies", new { movieId = actorForm.MovieId });
        }
    }
}
