namespace InfinityCinema.Web.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using InfinityCinema.Data;
    using InfinityCinema.Data.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    using static InfinityCinema.Common.GlobalConstants;

    public static class ApplicationBuilderExtension
    {
        public static IApplicationBuilder PrepaerDatabase(this IApplicationBuilder app)
        {
            using var scopedService = app.ApplicationServices.CreateScope();

            var data = scopedService.ServiceProvider.GetService<InfinityCinemaDbContext>();

            IServiceProvider services = scopedService.ServiceProvider;

            data.Database.Migrate();
            SeedGenres(data);
            SeedAdministrator(services);

            return app;
        }

        private static void SeedGenres(InfinityCinemaDbContext data)
        {
            if (data.Genres.Any())
            {
                return;
            }

            var genres = new List<Genre>()
            {
                new Genre() { Name = "Action", ImageUrl = "https://previews.123rf.com/images/pedrolieb/pedrolieb1210/pedrolieb121000023/16084899-clap-film-of-cinema-action-genre-clapperboard-text-illustration-.jpg?fj=1" },
                new Genre() { Name = "Animation", ImageUrl = "https://www.hitechanimation.com/blog/wp-content/uploads/2018/02/mouse-hd-png1258.png" },
                new Genre() { Name = "Comedy", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/c/c2/Film_Comici.svg/1200px-Film_Comici.svg.png" },
                new Genre() { Name = "Crime", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/0/03/Crime_film_clapperboard.svg/1200px-Crime_film_clapperboard.svg.png" },
                new Genre() { Name = "Drama", ImageUrl = "https://previews.123rf.com/images/pedrolieb/pedrolieb1210/pedrolieb121000024/16084918-clap-film-of-cinema-drama-genre-clapperboard-text-illustration-.jpg" },
                new Genre() { Name = "Fantasy", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/9/96/Variance_in_character_design_-_Lia_Turtle%2C_Shain%2C_and_Cendrea_from_Chaos%26Evolutions.png/1920px-Variance_in_character_design_-_Lia_Turtle%2C_Shain%2C_and_Cendrea_from_Chaos%26Evolutions.png" },
                new Genre() { Name = "Historical", ImageUrl = "https://cdn2.vectorstock.com/i/1000x1000/57/66/movie-genre-history-cinema-icon-of-ancient-vector-17935766.jpg" },
                new Genre() { Name = "Horror", ImageUrl = "https://previews.123rf.com/images/pedrolieb/pedrolieb1210/pedrolieb121000025/16084905-clap-film-of-cinema-horror-genre-clapperboard-text-illustration-.jpg" },
                new Genre() { Name = "Mystery", ImageUrl = "https://previews.123rf.com/images/pedrolieb/pedrolieb1210/pedrolieb121000034/16084904-clap-film-of-cinema-mystery-genre-clapperboard-text-illustration-.jpg" },
                new Genre() { Name = "Romance", ImageUrl = "https://previews.123rf.com/images/pedrolieb/pedrolieb1210/pedrolieb121000028/16084913-clap-film-of-cinema-romance-genre-clapperboard-text-illustration-.jpg" },
                new Genre() { Name = "Thriller", ImageUrl = "https://previews.123rf.com/images/pedrolieb/pedrolieb1210/pedrolieb121000031/16084898-clap-film-of-cinema-thriller-genre-clapperboard-text-illustration-.jpg" },
                new Genre() { Name = "Western", ImageUrl = "https://previews.123rf.com/images/pedrolieb/pedrolieb1210/pedrolieb121000032/16084902-clap-film-of-cinema-western-genre-clapperboard-text-illustration-.jpg" },
            };

            data.Genres.AddRange(genres);
            data.SaveChanges();
        }

        private static void SeedAdministrator(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = services.GetRequiredService<RoleManager<ApplicationRole>>();

            Task
                .Run(async () =>
                {
                    if (await roleManager.RoleExistsAsync(AdministratorRoleName))
                    {
                        return;
                    }

                    var role = new ApplicationRole { Name = AdministratorRoleName };

                    await roleManager.CreateAsync(role);

                    const string adminEmail = "admin@crs.com";
                    const string adminPassword = "admin12";

                    ApplicationUser user = new ApplicationUser
                    {
                        Email = adminEmail,
                        UserName = adminEmail,
                        FullName = "Admin",
                    };

                    await userManager.CreateAsync(user, adminPassword);

                    await userManager.AddToRoleAsync(user, role.Name);
                })
                .GetAwaiter()
                .GetResult();
        }
    }
}
