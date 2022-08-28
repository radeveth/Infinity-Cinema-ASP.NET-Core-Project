namespace InfinityCinema.Services.Data.ActorsService.Models
{
    using System.Collections.Generic;

    public class EditActorsFormModel
    {
        public IEnumerable<ActorViewModel> ExistingActors { get; set; }

        public IEnumerable<ActorFormModel> NewActors { get; set; }
    }
}
