﻿namespace InfinityCinema.Services.Data.ActorsService
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using InfinityCinema.Data.Models;
    using InfinityCinema.Services.Data.ActorsService.Models;

    public interface IActorService
    {
        Task<Actor> CreateAsync(ActorFormModel actorFormModel);

        Actor GetActorByNames(string fullName);

        IEnumerable<ActorViewModel> GetActorsForGivenMovie(int movieId);
    }
}
