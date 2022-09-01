// ReSharper disable VirtualMemberCallInConstructor
namespace InfinityCinema.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using InfinityCinema.Data.Common.Models;
    using InfinityCinema.Data.Models.Enums;
    using Microsoft.AspNetCore.Identity;

    using static InfinityCinema.Data.Common.DataValidation.ApplicationUserValidation;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();

            this.MoviesCreated = new HashSet<Movie>();
            this.CommentsCreated = new HashSet<MovieComment>();
            this.ApplicationUserMovies = new HashSet<ApplicationUserMovie>();
        }

        [Required]
        [MaxLength(FullNameMaxLength)]
        public string FullName { get; set; }

        [Required]
        public Gender Gender { get; set; }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }

        public virtual ICollection<Movie> MoviesCreated { get; set; }

        public virtual ICollection<MovieComment> CommentsCreated { get; set; }

        public virtual ICollection<ApplicationUserMovie> ApplicationUserMovies { get; set; }
    }
}
