namespace InfinityCinema.Data.Seeding
{
    using System;
    using System.Threading.Tasks;

    public interface ISeeder
    {
        Task SeedAsync(InfinityCinemaDbContext dbContext, IServiceProvider serviceProvider);
    }
}
