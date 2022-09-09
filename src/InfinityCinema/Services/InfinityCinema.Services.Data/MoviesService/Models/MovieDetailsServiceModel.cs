namespace InfinityCinema.Services.Data.MoviesService.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using InfinityCinema.Data.Models.Enums;
    using InfinityCinema.Services.Data.ActorsService.Models;
    using InfinityCinema.Services.Data.DirectorsService.Models;
    using InfinityCinema.Services.Data.GenresService.Models;
    using InfinityCinema.Services.Data.MovieCommentsService.Models;
    using InfinityCinema.Services.Data.PlatformsService.Models;

    using static InfinityCinema.Data.Common.DataValidation.CommentValidation;

    public class MovieDetailsServiceModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string TrailerPath { get; set; }

        public DirectorViewModel Director { get; set; }

        public IEnumerable<GenreViewModel> Genres { get; set; }

        public string Description { get; set; }

        public string Duration { get; set; }

        public Resolution Resolution { get; set; }

        public DateTime DateOfReleased { get; set; }

        public IEnumerable<string> Images { get; set; }

        public IEnumerable<ActorViewModel> Actors { get; set; }

        public IEnumerable<PlatformViewModel> Platforms { get; set; }

        public IEnumerable<string> Languages { get; set; }

        public string Countruy { get; set; }

        public IEnumerable<MovieListingViewModel> UpNextMovies { get; set; }

        public IEnumerable<string> ApplicationUsersId { get; set; }

        public IEnumerable<MovieCommentViewModel> Comments { get; set; }

        [Required]
        [StringLength(ContentMaxLength)]
        [Display(Name = "Comment content")]
        public string NewCommentContent { get; set; }

        //public void CreateMappings(IProfileExpression configuration)
        //{
        //    configuration.CreateMap<Movie, MovieDetailsServiceModel>()
        //        .ForMember(x => x.Genres, y => y.MapFrom(s => s.MovieGenres.Select(m => m.Genre)))
        //        .ForMember(x => x.Images, y => y.MapFrom(s => s.Images.Select(i => i.Url)))
        //        .ForMember(x => x.Actors, y => y.MapFrom(s => s.MovieActors.Select(m => m.Actor)))
        //        .ForMember(x => x.Platforms, y => y.MapFrom(s => s.MoviePlatforms.Select(m => m.Platform)))
        //        .ForMember(x => x.Languages, y => y.MapFrom(s => s.MovieLanguages.Select(i => i.Language.Name)))
        //        .ForMember(x => x.ApplicationUsersId, y => y.MapFrom(s => s.ApplicationUserMovies.Select(a => a.UserId)));

        //    configuration.CreateMap<Movie, MovieListingViewModel>();
        //}
    }
}
