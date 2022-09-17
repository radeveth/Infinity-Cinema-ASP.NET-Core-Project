namespace InfinityCinema.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using InfinityCinema.Data;
    using InfinityCinema.Data.Models;
    using InfinityCinema.Services.Data.PlatformsService;
    using InfinityCinema.Services.Data.PlatformsService.Models;
    using InfinityCinema.Services.Mapping;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class PlatformServiceTests
    {
        public PlatformServiceTests()
        {
            this.InitializeMapper();
        }

        // Test #1
        [Fact]
        public async Task CreateAsyncShouldMapPlatformFormModelToPlatfromEntityAndAddNewPlatformInDatabase()
        {
            // Arrange
            int expectedPlatformsCountInDatabase = 1;
            InfinityCinemaDbContext dbContext = new InfinityCinemaDbContext(this.PrepareOptionsForDbContext(1));
            PlatformService platformsService = new PlatformService(dbContext);

            // Act
            await platformsService.CreateAsync<PlatformViewModel>(new PlatformFormModel()
            {
                Name = "Netflix",
                IconUrl = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAHcAAAAgCAMAAAAfbBf3AAAAbFBMVEX///+4HSS0AAC1AAm3ERrIZGffra7pycnow8TisbO3Fx/GXWDw2tu/O0C0AAW2Cxa8MTfjuLnPfYD8+Pj36+u1AA/UjY/Nd3j68vL15eXEVlnAREjdqKns0NG6JCvCTlHXlpfboKHLb3LRhYfvjZQoAAACpklEQVRIib3X6baqIBQAYCZNqTRTckgr9f3f8bCZNOoeO6vF3b9YRHyAsEWEZZADgigyjGmsigS7IKVXgXF+gaqUugpWQ8VONmJ7tEQCFRGUBlni3VK5R6qfObSLblz2VEGpY7KUIN1RcPchMBajLFQwc460S3bvXJZDGFeWmEZl1eJy3WbaclvgjrJwyeUAHsbV4/VcflYxKbeGEsC3WZYK6/LrrBr1Wy46wkK3CEEfsOB6oUT56lLkhRuzDnDz8+r3X91RqHkWtso8xOSNW3ru4RtXPdcbunODmY0RhXaVSFPogiHnql0b1k0z2JTYbGvr0jS029ijqI+xPYFdaBdFbHmkzoVRbO3nL91ET5gMT67ofZf1cS+j+c3lnWpTfOIild/4CS0uVx36+UpQGXDW/+1irtrcP3Jr2FV0dC6/diqZvM3PG65+ZPuPXHgpmXRv3Fhl7SSwG1PPPZYqmfgugch+d3PV6LN1PnHsAKQfNSSTfGZPrmgriKWbNy6rm6XRhquWGfP72o0zmz/Cnd+JrdKGcUsS3LX5SqdJ49pkEtAdzTY0PRi3IKFduGHxG7a3SJtCQrvwI7+nwt7OrFuzF9djv3PhQZK0Mbe6xd0R32VFPF7q6Oqm/YmLGSH42EUXz63UyTS32GHlopf5YkqFvESSv7nQH2fi6rlwj4VrOrz91Z+cO+e+axLln12XHVYuNtdJdWDztXsgm24mU+LNMTFkyOnJpQJWiMkJe25C7Fgi/bmwuE/pkwiRqcxLGHduk+yGw5Kuy0bGes9XaTyOl/MU3Y+5WWcqMrXR9qAVbizT2n2QXGLqBo6mR1/sDm2Dvoqq6B9n2GFtRjIqdKV5GUpW6LVr9/NYDF9S76Nsh2JMdbkmOYfvslOkvzL+V7TjHJ2iH2Z9MrR/jXLjAAAAAElFTkSuQmCC",
                PathUrl = "https://www.netflix.com/",
            });

            // Assert
            Assert.Equal(expectedPlatformsCountInDatabase, dbContext.Platforms.Count());
        }

        // Test #2
        [Fact]
        public async Task AllShouldReturnAllPlatformsMappedToGivenViewModelIfSerchNameCriteriaIsNotGiven()
        {
            // Arrange
            InfinityCinemaDbContext dbContext = new InfinityCinemaDbContext(this.PrepareOptionsForDbContext(2));
            PlatformService platformsService = new PlatformService(dbContext);

            List<Platform> platforms = this.SeedPlatforms();
            await dbContext.AddRangeAsync(platforms);
            await dbContext.SaveChangesAsync();

            int expectedPlatfromsCountInDatabase = platforms.Count;

            // Act
            IEnumerable<PlatformViewModel> platformViewModels = platformsService.All<PlatformViewModel>();

            // Assert
            Assert.Equal(platformViewModels.Count(), dbContext.Platforms.Count());
            Assert.Equal(expectedPlatfromsCountInDatabase, platformViewModels.Count());
        }

        // Test #3
        [Fact]
        public async Task AllShouldReturnAllPlatformsMappedToGivenViewModelThatContainsSerchNameThatIsGiven()
        {
            // Arrange
            InfinityCinemaDbContext dbContext = new InfinityCinemaDbContext(this.PrepareOptionsForDbContext(3));
            PlatformService platformsService = new PlatformService(dbContext);

            List<Platform> platforms = this.SeedPlatforms();
            await dbContext.AddRangeAsync(platforms);
            await dbContext.SaveChangesAsync();

            int expectedPlatfromsCountInDatabase = platforms.Where(p => p.Name.ToLower().Contains("netflix1")).Count();

            // Act
            IEnumerable<PlatformViewModel> platformViewModels = platformsService.All<PlatformViewModel>("Netflix1");

            // Assert
            Assert.Equal(expectedPlatfromsCountInDatabase, platformViewModels.Count());

            // ---
            // Arrange
            expectedPlatfromsCountInDatabase = platforms.Where(p => p.Name.ToLower().Contains("netflix")).Count();

            // Act
            platformViewModels = platformsService.All<PlatformViewModel>("Netflix");

            // Assert
            Assert.Equal(expectedPlatfromsCountInDatabase, platformViewModels.Count());
        }

        // Test #4
        [Fact]
        public async Task DeleteAsyncTest()
        {
            // Arrange
            InfinityCinemaDbContext dbContext = new InfinityCinemaDbContext(this.PrepareOptionsForDbContext(4));
            PlatformService platformsService = new PlatformService(dbContext);

            List<Platform> platforms = this.SeedPlatforms();
            await dbContext.AddRangeAsync(platforms);
            await dbContext.SaveChangesAsync();

            int expectedPlatfromsCountInDatabase = platforms.Count - 1;

            // Act
            await platformsService.DeleteAsync(1);

            // Assert
            Assert.Equal(expectedPlatfromsCountInDatabase, dbContext.Platforms.Count());
        }

        // Test #5
        [Fact]
        public async Task DeleteAsyncManyTimesTest()
        {
            // Arrange
            InfinityCinemaDbContext dbContext = new InfinityCinemaDbContext(this.PrepareOptionsForDbContext(5));
            PlatformService platformsService = new PlatformService(dbContext);

            List<Platform> platforms = this.SeedPlatforms();
            await dbContext.AddRangeAsync(platforms);
            await dbContext.SaveChangesAsync();

            int expectedPlatfromsCountInDatabase = platforms.Count - 2;

            // Act
            await platformsService.DeleteAsync(1);
            await platformsService.DeleteAsync(2);

            // Assert
            Assert.Equal(expectedPlatfromsCountInDatabase, dbContext.Platforms.Count());
        }

        // Test #6
        [Fact]
        public async Task DeleteAsyncShouldThrowNullReferenceExceptionIfPlatformWithGivenIdDoesNotExist()
        {
            // Arrange
            InfinityCinemaDbContext dbContext = new InfinityCinemaDbContext(this.PrepareOptionsForDbContext(6));
            PlatformService platformsService = new PlatformService(dbContext);

            List<Platform> platforms = this.SeedPlatforms();
            await dbContext.AddRangeAsync(platforms);
            await dbContext.SaveChangesAsync();

            // Act & Assert
            await Assert.ThrowsAsync<NullReferenceException>(async () => await platformsService.DeleteAsync(platforms.Count + 100));
        }

        private List<Platform> SeedPlatforms()
        {
            return new List<Platform>()
            {
                new Platform()
                {
                    Name = "Netflix1",
                    IconUrl = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAHcAAAAgCAMAAAAfbBf3AAAAbFBMVEX///+4HSS0AAC1AAm3ERrIZGffra7pycnow8TisbO3Fx/GXWDw2tu/O0C0AAW2Cxa8MTfjuLnPfYD8+Pj36+u1AA/UjY/Nd3j68vL15eXEVlnAREjdqKns0NG6JCvCTlHXlpfboKHLb3LRhYfvjZQoAAACpklEQVRIib3X6baqIBQAYCZNqTRTckgr9f3f8bCZNOoeO6vF3b9YRHyAsEWEZZADgigyjGmsigS7IKVXgXF+gaqUugpWQ8VONmJ7tEQCFRGUBlni3VK5R6qfObSLblz2VEGpY7KUIN1RcPchMBajLFQwc460S3bvXJZDGFeWmEZl1eJy3WbaclvgjrJwyeUAHsbV4/VcflYxKbeGEsC3WZYK6/LrrBr1Wy46wkK3CEEfsOB6oUT56lLkhRuzDnDz8+r3X91RqHkWtso8xOSNW3ru4RtXPdcbunODmY0RhXaVSFPogiHnql0b1k0z2JTYbGvr0jS029ijqI+xPYFdaBdFbHmkzoVRbO3nL91ET5gMT67ofZf1cS+j+c3lnWpTfOIild/4CS0uVx36+UpQGXDW/+1irtrcP3Jr2FV0dC6/diqZvM3PG65+ZPuPXHgpmXRv3Fhl7SSwG1PPPZYqmfgugch+d3PV6LN1PnHsAKQfNSSTfGZPrmgriKWbNy6rm6XRhquWGfP72o0zmz/Cnd+JrdKGcUsS3LX5SqdJ49pkEtAdzTY0PRi3IKFduGHxG7a3SJtCQrvwI7+nwt7OrFuzF9djv3PhQZK0Mbe6xd0R32VFPF7q6Oqm/YmLGSH42EUXz63UyTS32GHlopf5YkqFvESSv7nQH2fi6rlwj4VrOrz91Z+cO+e+axLln12XHVYuNtdJdWDztXsgm24mU+LNMTFkyOnJpQJWiMkJe25C7Fgi/bmwuE/pkwiRqcxLGHduk+yGw5Kuy0bGes9XaTyOl/MU3Y+5WWcqMrXR9qAVbizT2n2QXGLqBo6mR1/sDm2Dvoqq6B9n2GFtRjIqdKV5GUpW6LVr9/NYDF9S76Nsh2JMdbkmOYfvslOkvzL+V7TjHJ2iH2Z9MrR/jXLjAAAAAElFTkSuQmCC1",
                    PathUrl = "https://www.netflix.com/1",
                },
                new Platform()
                {
                    Name = "Netflix2",
                    IconUrl = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAHcAAAAgCAMAAAAfbBf3AAAAbFBMVEX///+4HSS0AAC1AAm3ERrIZGffra7pycnow8TisbO3Fx/GXWDw2tu/O0C0AAW2Cxa8MTfjuLnPfYD8+Pj36+u1AA/UjY/Nd3j68vL15eXEVlnAREjdqKns0NG6JCvCTlHXlpfboKHLb3LRhYfvjZQoAAACpklEQVRIib3X6baqIBQAYCZNqTRTckgr9f3f8bCZNOoeO6vF3b9YRHyAsEWEZZADgigyjGmsigS7IKVXgXF+gaqUugpWQ8VONmJ7tEQCFRGUBlni3VK5R6qfObSLblz2VEGpY7KUIN1RcPchMBajLFQwc460S3bvXJZDGFeWmEZl1eJy3WbaclvgjrJwyeUAHsbV4/VcflYxKbeGEsC3WZYK6/LrrBr1Wy46wkK3CEEfsOB6oUT56lLkhRuzDnDz8+r3X91RqHkWtso8xOSNW3ru4RtXPdcbunODmY0RhXaVSFPogiHnql0b1k0z2JTYbGvr0jS029ijqI+xPYFdaBdFbHmkzoVRbO3nL91ET5gMT67ofZf1cS+j+c3lnWpTfOIild/4CS0uVx36+UpQGXDW/+1irtrcP3Jr2FV0dC6/diqZvM3PG65+ZPuPXHgpmXRv3Fhl7SSwG1PPPZYqmfgugch+d3PV6LN1PnHsAKQfNSSTfGZPrmgriKWbNy6rm6XRhquWGfP72o0zmz/Cnd+JrdKGcUsS3LX5SqdJ49pkEtAdzTY0PRi3IKFduGHxG7a3SJtCQrvwI7+nwt7OrFuzF9djv3PhQZK0Mbe6xd0R32VFPF7q6Oqm/YmLGSH42EUXz63UyTS32GHlopf5YkqFvESSv7nQH2fi6rlwj4VrOrz91Z+cO+e+axLln12XHVYuNtdJdWDztXsgm24mU+LNMTFkyOnJpQJWiMkJe25C7Fgi/bmwuE/pkwiRqcxLGHduk+yGw5Kuy0bGes9XaTyOl/MU3Y+5WWcqMrXR9qAVbizT2n2QXGLqBo6mR1/sDm2Dvoqq6B9n2GFtRjIqdKV5GUpW6LVr9/NYDF9S76Nsh2JMdbkmOYfvslOkvzL+V7TjHJ2iH2Z9MrR/jXLjAAAAAElFTkSuQmCC2",
                    PathUrl = "https://www.netflix.com/2",
                },
                new Platform()
                {
                    Name = "Netflix3",
                    IconUrl = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAHcAAAAgCAMAAAAfbBf3AAAAbFBMVEX///+4HSS0AAC1AAm3ERrIZGffra7pycnow8TisbO3Fx/GXWDw2tu/O0C0AAW2Cxa8MTfjuLnPfYD8+Pj36+u1AA/UjY/Nd3j68vL15eXEVlnAREjdqKns0NG6JCvCTlHXlpfboKHLb3LRhYfvjZQoAAACpklEQVRIib3X6baqIBQAYCZNqTRTckgr9f3f8bCZNOoeO6vF3b9YRHyAsEWEZZADgigyjGmsigS7IKVXgXF+gaqUugpWQ8VONmJ7tEQCFRGUBlni3VK5R6qfObSLblz2VEGpY7KUIN1RcPchMBajLFQwc460S3bvXJZDGFeWmEZl1eJy3WbaclvgjrJwyeUAHsbV4/VcflYxKbeGEsC3WZYK6/LrrBr1Wy46wkK3CEEfsOB6oUT56lLkhRuzDnDz8+r3X91RqHkWtso8xOSNW3ru4RtXPdcbunODmY0RhXaVSFPogiHnql0b1k0z2JTYbGvr0jS029ijqI+xPYFdaBdFbHmkzoVRbO3nL91ET5gMT67ofZf1cS+j+c3lnWpTfOIild/4CS0uVx36+UpQGXDW/+1irtrcP3Jr2FV0dC6/diqZvM3PG65+ZPuPXHgpmXRv3Fhl7SSwG1PPPZYqmfgugch+d3PV6LN1PnHsAKQfNSSTfGZPrmgriKWbNy6rm6XRhquWGfP72o0zmz/Cnd+JrdKGcUsS3LX5SqdJ49pkEtAdzTY0PRi3IKFduGHxG7a3SJtCQrvwI7+nwt7OrFuzF9djv3PhQZK0Mbe6xd0R32VFPF7q6Oqm/YmLGSH42EUXz63UyTS32GHlopf5YkqFvESSv7nQH2fi6rlwj4VrOrz91Z+cO+e+axLln12XHVYuNtdJdWDztXsgm24mU+LNMTFkyOnJpQJWiMkJe25C7Fgi/bmwuE/pkwiRqcxLGHduk+yGw5Kuy0bGes9XaTyOl/MU3Y+5WWcqMrXR9qAVbizT2n2QXGLqBo6mR1/sDm2Dvoqq6B9n2GFtRjIqdKV5GUpW6LVr9/NYDF9S76Nsh2JMdbkmOYfvslOkvzL+V7TjHJ2iH2Z9MrR/jXLjAAAAAElFTkSuQmCC3",
                    PathUrl = "https://www.netflix.com/3",
                },
            };
        }

        private DbContextOptions<InfinityCinemaDbContext> PrepareOptionsForDbContext(int testCount)
        {
            return new DbContextOptionsBuilder<InfinityCinemaDbContext>()
                .UseInMemoryDatabase(databaseName: $"PlatformServiceTestDb{testCount}")
                .Options;
        }

        private void InitializeMapper() => AutoMapperConfig.RegisterMappings(typeof(PlatformViewModel).Assembly);
    }
}
