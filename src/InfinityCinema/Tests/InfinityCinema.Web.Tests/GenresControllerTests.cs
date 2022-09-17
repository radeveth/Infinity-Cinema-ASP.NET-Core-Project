namespace InfinityCinema.Web.Tests
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using InfinityCinema.Data;
    using InfinityCinema.Data.Models;
    using InfinityCinema.Services.Data.GenresService;
    using InfinityCinema.Services.Data.GenresService.Models;
    using InfinityCinema.Web.Controllers;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class GenresControllerTests
    {
        //[Fact]
        //public async Task All()
        //{
        //    Genre genre = new Genre()
        //    {
        //        Name = "Action",
        //        Description = "rnd desc..",
        //        ImageUrl = "rnd img...",
        //    };

        //    InfinityCinemaDbContext dbContext = this.PrepareDbContext();
        //    GenreService genreService = new GenreService(dbContext);
        //    GenresController genresController = new GenresController(genreService);
        //    await dbContext.Genres.AddAsync(genre);
        //    await dbContext.SaveChangesAsync();

        //    var result = genresController.All() as ViewResult;
        //    var list = (IEnumerable<GenreViewModel>)result.ViewData.Model;
        //    Assert.Single(list);
        //}

        private InfinityCinemaDbContext PrepareDbContext()
        {
            DbContextOptions<InfinityCinemaDbContext> dbContextOptionsBuilder =
                new DbContextOptionsBuilder<InfinityCinemaDbContext>()
                .UseInMemoryDatabase(databaseName: "GenreControllerTestDb").Options;

            InfinityCinemaDbContext dbContext = new InfinityCinemaDbContext(dbContextOptionsBuilder);

            dbContext.Database.EnsureCreated();

            return dbContext;
        }
    }
}
