namespace InfinityCinema.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using InfinityCinema.Data;
    using InfinityCinema.Data.Models;
    using InfinityCinema.Data.Models.Enums;
    using InfinityCinema.Services.Data.ApplicationUsersService;
    using InfinityCinema.Services.Data.ApplicationUsersService.Models;
    using InfinityCinema.Services.Mapping;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class ApplicationUsersServiceTests
    {
        public ApplicationUsersServiceTests()
        {
            this.InitializeMapper();
        }

        // Test #1
        [Fact]
        public async Task GetViewModelByIdTest()
        {
            // Arrange
            ApplicationUser targetApplicationUser = new ApplicationUser()
            {
                FullName = "User 1",
                Email = "user1@gmail.com",
                Gender = Gender.Male,
            };

            InfinityCinemaDbContext dbContext = new InfinityCinemaDbContext(this.PrepareOptionsForDbContext(1));
            ApplicationUserService applicationUserService = new ApplicationUserService(dbContext);

            List<ApplicationUser> applicationUsers = this.SeedingUsers();
            await dbContext.Users.AddRangeAsync(applicationUsers);
            await dbContext.SaveChangesAsync();

            // Act
            ApplicationUserViewModel viewModel = applicationUserService
                .GetViewModelById<ApplicationUserViewModel>(applicationUsers.First().Id);

            Assert.Equal(targetApplicationUser.FullName, viewModel.FullName);
            Assert.Equal(targetApplicationUser.Gender, viewModel.Gender);
        }

        // Test #2
        [Fact]
        public async Task GetViewModelByIdShouldReturnDefaultValueIfUserWithGiveIdDoesNotExist()
        {
            // Arrange
            InfinityCinemaDbContext dbContext = new InfinityCinemaDbContext(this.PrepareOptionsForDbContext(2));
            ApplicationUserService applicationUserService = new ApplicationUserService(dbContext);

            List<ApplicationUser> applicationUsers = this.SeedingUsers();
            await dbContext.Users.AddRangeAsync(applicationUsers);
            await dbContext.SaveChangesAsync();

            // Act
            ApplicationUserViewModel viewModel = applicationUserService
                .GetViewModelById<ApplicationUserViewModel>(applicationUsers.First().Id + applicationUsers.Skip(1).First().Id);

            Assert.True(viewModel == null);
        }

        private List<ApplicationUser> SeedingUsers()
        {
            return new List<ApplicationUser>()
            {
                new ApplicationUser()
                {
                    FullName = "User 1",
                    Email = "user1@gmail.com",
                    Gender = Gender.Male,
                },
                new ApplicationUser()
                {
                    FullName = "User 2",
                    Email = "user2@gmail.com",
                    Gender = Gender.Female,
                },
                new ApplicationUser()
                {
                    FullName = "User 3",
                    Email = "user3@gmail.com",
                    Gender = Gender.Male,
                },
            };
        }

        private DbContextOptions<InfinityCinemaDbContext> PrepareOptionsForDbContext(int testCount)
        {
            return new DbContextOptionsBuilder<InfinityCinemaDbContext>()
                .UseInMemoryDatabase(databaseName: $"ApplicationUserTestDb{testCount}").Options;
        }

        private void InitializeMapper() => AutoMapperConfig.RegisterMappings(typeof(ApplicationUserViewModel).Assembly);
    }
}
