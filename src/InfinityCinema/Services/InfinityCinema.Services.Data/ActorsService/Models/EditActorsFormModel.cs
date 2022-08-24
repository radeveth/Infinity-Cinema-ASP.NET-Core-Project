namespace InfinityCinema.Services.Data.ActorsService.Models
{
    using System.Collections.Generic;

    public class EditActorsFormModel
    {
        public EditActorsFormModel()
        {
            this.ExistingActors = new List<ActorViewModel>();
            this.NewActors = new List<ActorFormModel>();
        }

        public IEnumerable<ActorViewModel> ExistingActors { get; set; }

        public IEnumerable<ActorFormModel> NewActors { get; set; }
    }
}
