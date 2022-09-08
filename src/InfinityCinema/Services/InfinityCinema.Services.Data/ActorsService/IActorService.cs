﻿namespace InfinityCinema.Services.Data.ActorsService
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using InfinityCinema.Services.Data.ActorsService.Models;

    public interface IActorService
    {
        // Create
        Task<T> CreateAsync<T>(ActorFormModel actorFormModel);

        // Read
        IEnumerable<ActorViewModel> All(string searchName = null);

        ActorViewModel GetActorByNames(string fullName);

        IEnumerable<T> GetActorsForGivenMovie<T>(int movieId);

        T GetViewModelByIdAsync<T>(int id);

        // Update

        // Delete
        Task DeleteActorsForParticularMovie(int movieId);
    }
}
