namespace InfinityCinema.Data
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;

    using InfinityCinema.Data.Common.Models;
    using InfinityCinema.Data.Configurations;
    using InfinityCinema.Data.Models;

    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class InfinityCinemaDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        private static readonly MethodInfo SetIsDeletedQueryFilterMethod =
            typeof(InfinityCinemaDbContext).GetMethod(
                nameof(SetIsDeletedQueryFilter),
                BindingFlags.NonPublic | BindingFlags.Static);

        public InfinityCinemaDbContext(DbContextOptions<InfinityCinemaDbContext> options)
            : base(options)
        {
        }

        public DbSet<Actor> Actors { get; set; }

        public DbSet<ApplicationUserMovie> ApplicationUserMovies { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Director> Directors { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<Image> Images { get; set; }

        public DbSet<Language> Languages { get; set; }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<MovieActor> MovieActors { get; set; }

        public DbSet<MovieComment> MovieComments { get; set; }

        public DbSet<MovieGenre> MovieGenres { get; set; }

        public DbSet<MovieLanguage> MovieLanguages { get; set; }

        public DbSet<MoviePlatform> MoviePlatform { get; set; }

        public DbSet<MovieUserComment> MovieUserComments { get; set; }

        public DbSet<MovieUserStarRating> MovieUserStarRatings { get; set; }

        public DbSet<Platform> Platforms { get; set; }

        public DbSet<UserComment> UserComments { get; set; }

        public DbSet<Setting> Settings { get; set; }

        public override int SaveChanges() => this.SaveChanges(true);

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            this.ApplyAuditInfoRules();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
            this.SaveChangesAsync(true, cancellationToken);

        public override Task<int> SaveChangesAsync(
            bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            this.ApplyAuditInfoRules();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ActorConfiguration());
            builder.ApplyConfiguration(new ApplicationUserConfiguration());
            builder.ApplyConfiguration(new ApplicationUserMoviesConfiguration());
            builder.ApplyConfiguration(new CountryConfiguration());
            builder.ApplyConfiguration(new DirectorConfiguration());
            builder.ApplyConfiguration(new GenreConfiguration());
            builder.ApplyConfiguration(new LanguageConfiguration());
            builder.ApplyConfiguration(new MovieActorConfiguration());
            builder.ApplyConfiguration(new MovieConfiguration());
            builder.ApplyConfiguration(new MovieGenreConfiguration());
            builder.ApplyConfiguration(new MovieLanguageConfiguration());
            builder.ApplyConfiguration(new MoviePlatformConfiguration());
            builder.ApplyConfiguration(new MovieUserCommentConfiguration());
            builder.ApplyConfiguration(new MovieUserStarRatingConfiguration());
            builder.ApplyConfiguration(new PlatformConfiguration());
            builder.ApplyConfiguration(new UserCommentConfiguration());

            // Needed for Identity models configuration
            base.OnModelCreating(builder);

            this.ConfigureUserIdentityRelations(builder);

            EntityIndexesConfiguration.Configure(builder);

            var entityTypes = builder.Model.GetEntityTypes().ToList();

            // Set global query filter for not deleted entities only
            var deletableEntityTypes = entityTypes
                .Where(et => et.ClrType != null && typeof(IDeletableEntity).IsAssignableFrom(et.ClrType));
            foreach (var deletableEntityType in deletableEntityTypes)
            {
                var method = SetIsDeletedQueryFilterMethod.MakeGenericMethod(deletableEntityType.ClrType);
                method.Invoke(null, new object[] { builder });
            }

            // Disable cascade delete
            var foreignKeys = entityTypes
                .SelectMany(e => e.GetForeignKeys().Where(f => f.DeleteBehavior == DeleteBehavior.Cascade));
            foreach (var foreignKey in foreignKeys)
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        private static void SetIsDeletedQueryFilter<T>(ModelBuilder builder)
            where T : class, IDeletableEntity
        {
            builder.Entity<T>().HasQueryFilter(e => !e.IsDeleted);
        }

        // Applies configurations
        private void ConfigureUserIdentityRelations(ModelBuilder builder)
             => builder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);

        private void ApplyAuditInfoRules()
        {
            var changedEntries = this.ChangeTracker
                .Entries()
                .Where(e =>
                    e.Entity is IAuditInfo &&
                    (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in changedEntries)
            {
                var entity = (IAuditInfo)entry.Entity;
                if (entry.State == EntityState.Added && entity.CreatedOn == default)
                {
                    entity.CreatedOn = DateTime.UtcNow;
                }
                else
                {
                    entity.ModifiedOn = DateTime.UtcNow;
                }
            }
        }
    }
}
