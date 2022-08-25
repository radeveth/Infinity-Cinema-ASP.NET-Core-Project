﻿namespace InfinityCinema.Services.Data.MoviesService.Models
{
    using System;
    using System.Collections.Generic;
    using InfinityCinema.Data.Models.Enums;
    using InfinityCinema.Services.Data.ActorsService.Models;
    using InfinityCinema.Services.Data.PlatformsService.Models;

    public class MovieDetailsViewModel
    {
        public MovieDetailsViewModel()
        {
            this.Images = new List<string>();
            this.Actors = new List<ActorViewModel>();
            this.Platforms = new List<PlatformViewModel>();
            this.Languages = new List<string>();
        }

        public string Name { get; set; }

        public string Trailer { get; set; }

        public IEnumerable<string> Genres { get; set; }

        public string Description { get; set; }

        public string Duration { get; set; }

        public Resolution Resolution { get; set; }

        public DateTime DateOfReleased { get; set; }

        public IEnumerable<string> Images { get; set; }

        public IEnumerable<ActorViewModel> Actors { get; set; }

        public string Director { get; set; }

        public IEnumerable<PlatformViewModel> Platforms { get; set; }

        public IEnumerable<string> Languages { get; set; }

        public string Countruy { get; set; }

        public IEnumerable<MovieListingViewModel> UpNextMovies { get; set; }
    }
}
