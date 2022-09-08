namespace InfinityCinema.Services.Data.DirectorsService
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using InfinityCinema.Data;
    using InfinityCinema.Data.Models;
    using InfinityCinema.Services.Data.DirectorsService.Models;
    using InfinityCinema.Services.Mapping;

    public class DirectorService : IDirectorService
    {
        private const string SPLITING_DIRECTOR_FULL_NAME = " ";

        private readonly InfinityCinemaDbContext dbContext;

        public DirectorService(InfinityCinemaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // Create
        public async Task<T> CreateAsync<T>(DirectorFormModel directorFormModel)
        {
            string[] givenDirectorNames = this.SplitDirectorFullName(directorFormModel.FullName);

            string givenDirectorFirstName = givenDirectorNames[0];
            string givenDirectorLastName = givenDirectorNames[1];

            Director director = new Director()
            {
                FirstName = givenDirectorFirstName,
                LastName = givenDirectorLastName,
                InformationUrl = directorFormModel.InformationUrl,
            };

            await this.dbContext.AddAsync(director);
            await this.dbContext.SaveChangesAsync();

            return this.GetViewModelById<T>(director.Id);
        }

        // Read
        public T GetViewModelByGivenFullName<T>(string fullName)
        {
            string[] directorNameParts = this.SplitDirectorFullName(fullName);
            string firstName = directorNameParts[0];
            string lastName = directorNameParts[1];

            T director = this.dbContext
                .Directors
                .Where(d => d.FirstName.ToLower() == firstName.ToLower() && d.LastName.ToLower() == lastName.ToLower())
                .To<T>()
                .FirstOrDefault();

            return director;
        }

        public T GetViewModelById<T>(int id)
            => this.dbContext
                .Directors
                .Where(d => d.Id == id)
                .To<T>()
                .FirstOrDefault();

        // Update
        public async Task<bool> EditDirectorAsync(int directorId, DirectorFormModel directorFormModel)
        {
            try
            {
                Director director = this.dbContext.Directors.FirstOrDefault(d => d.Id == directorId);

                string[] directorNameParts = this.SplitDirectorFullName(directorFormModel.FullName);

                director.FirstName = directorNameParts[0];
                director.LastName = directorNameParts[1];
                director.InformationUrl = directorFormModel.InformationUrl;

            }
            catch (Exception)
            {
                throw new InvalidOperationException();
            }

            await this.dbContext.SaveChangesAsync();
            return true;
        }

        // Delete

        // Useful methods
        private string[] SplitDirectorFullName(string fullName)
            => fullName
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .ToArray();
    }
}
