namespace InfinityCinema.Services.Data.DirectorsService
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using InfinityCinema.Data;
    using InfinityCinema.Data.Models;

    public class DirectorService : IDirectorService
    {
        private readonly InfinityCinemaDbContext dbContext;

        public DirectorService(InfinityCinemaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<int> GetDirectorIdAsync(DirectorFormModel directorFormModel)
        {
            Director director = this.dbContext.Directors
                    .FirstOrDefault(d => $"{d.FirstName}{d.LastName}"
                        .Replace(" ", string.Empty).ToLower() == directorFormModel.FullName
                            .Replace(" ", string.Empty).ToLower());

            if (director == null)
            {
                director = await this.CreateAsync(directorFormModel);
            }

            return director.Id;
        }

        public async Task<Director> CreateAsync(DirectorFormModel directorFormModel)
        {
            var givenDirectorNames = directorFormModel
                    .FullName
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

            string givenDirectorFirstName = givenDirectorNames[0];
            string givenDirectorLastName = givenDirectorNames[1];

            Director director = new Director()
            {
                FirstName = givenDirectorFirstName,
                LastName = givenDirectorLastName,
            };

            await this.dbContext.AddAsync(director);
            await this.dbContext.SaveChangesAsync();

            return director;
        }
    }
}
