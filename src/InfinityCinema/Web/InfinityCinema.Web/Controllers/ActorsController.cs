namespace InfinityCinema.Web.Controllers
{
    using System.Threading.Tasks;

    using InfinityCinema.Services.Data.ActorsService;
    using InfinityCinema.Services.Data.ActorsService.Models;
    using InfinityCinema.Services.Data.MoviesService;
    using Microsoft.AspNetCore.Mvc;

    public class ActorsController : BaseController
    {
        private readonly IActorService actorService;

        public ActorsController(IActorService actorService, IMovieService movieService)
        {
            this.actorService = actorService;
        }

        public async Task<IActionResult> DeleteAsync(int actorId, int movieId)
        {
            await this.actorService.RemoveRelationBetweenMovieActorsAndActosTablesAsync(actorId, movieId);
            await this.actorService.DeleteAsync(actorId);

            return this.RedirectToAction("EditMovieActors", "Movies", new { movieId = movieId });
        }

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
