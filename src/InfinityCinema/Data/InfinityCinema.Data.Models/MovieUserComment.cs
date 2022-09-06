namespace InfinityCinema.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    using InfinityCinema.Data.Common.Models;

    public class MovieUserComment : IDeletableEntity
    {
        [ForeignKey(nameof(Movie))]
        public int MovieId { get; set; }

        public Movie Movie { get; set; }

        [ForeignKey(nameof(MovieComment))]
        public int CommentId { get; set; }

        public virtual MovieComment MovieComment { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
