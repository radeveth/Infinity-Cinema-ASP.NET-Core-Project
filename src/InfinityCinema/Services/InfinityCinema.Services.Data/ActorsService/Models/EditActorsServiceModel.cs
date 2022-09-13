namespace InfinityCinema.Services.Data.ActorsService.Models
{
    using System.Collections.Generic;

    public class EditActorsServiceModel
    {
        public int MovieId { get; set; }

        public IEnumerable<ActorViewModel> ExistingActors { get; set; }

        public ActorFormModel NewActor { get; set; }
    }
}
