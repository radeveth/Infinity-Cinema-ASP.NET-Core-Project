namespace InfinityCinema.Web
{
    using System.Reflection;

    using InfinityCinema.Data;
    using InfinityCinema.Data.Common;
    using InfinityCinema.Data.Common.Repositories;
    using InfinityCinema.Data.Models;
    using InfinityCinema.Data.Repositories;
    using InfinityCinema.Data.Seeding;
    using InfinityCinema.Services.Data.ActorsService;
    using InfinityCinema.Services.Data.ApplicationUsersService;
    using InfinityCinema.Services.Data.CountriesService;
    using InfinityCinema.Services.Data.DirectorsService;
    using InfinityCinema.Services.Data.GenresService;
    using InfinityCinema.Services.Data.ImagesService;
    using InfinityCinema.Services.Data.LanguagesService;
    using InfinityCinema.Services.Data.MovieCommentsService;
    using InfinityCinema.Services.Data.MoviesService;
    using InfinityCinema.Services.Data.MovieUserCommentsService;
    using InfinityCinema.Services.Data.PlatformsService;
    using InfinityCinema.Services.Data.SettingsService;
    using InfinityCinema.Services.Mapping;
    using InfinityCinema.Services.Messaging;
    using InfinityCinema.Web.Areas.Administration.AdministartionsService;
    using InfinityCinema.Web.Infrastructure;
    using InfinityCinema.Web.ViewModels;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            ConfigureServices(builder.Services, builder.Configuration);
            var app = builder.Build();
            Configure(app);
            app.Run();
        }

        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<InfinityCinemaDbContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>(IdentityOptionsProvider.GetIdentityOptions)
                .AddRoles<ApplicationRole>().AddEntityFrameworkStores<InfinityCinemaDbContext>();

            services.Configure<CookiePolicyOptions>(
                options =>
                {
                    options.CheckConsentNeeded = context => true;
                    options.MinimumSameSitePolicy = SameSiteMode.None;
                });

            services.AddControllersWithViews(
                options =>
                {
                    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                }).AddRazorRuntimeCompilation();
            services.AddRazorPages();
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddSingleton(configuration);

            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();

            // Application services
            services.AddTransient<IEmailSender, NullMessageSender>();
            services.AddTransient<ISettingsService, SettingsService>();
            services.AddTransient<IGenreService, GenreService>();
            services.AddTransient<IDirectorService, DirectorService>();
            services.AddTransient<IImageService, ImageService>();
            services.AddTransient<ILanguageService, LanguageService>();
            services.AddTransient<ICountryService, CountryService>();
            services.AddTransient<IMovieService, MovieService>();
            services.AddTransient<IActorService, ActorService>();
            services.AddTransient<IPlatformService, PlatformService>();
            services.AddTransient<IApplicationUserService, ApplicationUserService>();
            services.AddTransient<IAdministartionService, AdministartionService>();
            services.AddTransient<IMovieCommentService, MovieCommentService>();
            services.AddTransient<IMovieUserCommentService, MovieUserCommentService>();
        }

        private static void Configure(WebApplication app)
        {
            // Seed data on application startup
            using (var serviceScope = app.Services.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<InfinityCinemaDbContext>();
                // dbContext.Database.Migrate();
                new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.PrepaerDatabase();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(name: "areaRoute", pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();
        }
    }
}
