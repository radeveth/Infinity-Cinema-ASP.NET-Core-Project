namespace InfinityCinema.Services.Data.DirectorsService
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using InfinityCinema.Data;
    using InfinityCinema.Data.Models;
    using InfinityCinema.Services.Data.DirectorsService.Models;

    public class DirectorService : IDirectorService
    {
        private const string SPLITING_DIRECTOR_FULL_NAME = " ";

        private readonly InfinityCinemaDbContext dbContext;

        public DirectorService(InfinityCinemaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // Create
        public async Task<DirectorViewModel> CreateAsync(DirectorFormModel directorFormModel)
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

            return new DirectorViewModel()
            {
                Id = director.Id,
                FullName = director.FirstName + " " + director.LastName,
                InformationLink = director.InformationUrl,
            };
        }

        // Read
        public string GetDirectorFullNameById(int id)
        {
            Director director = this.dbContext.Directors.Find(id);

            return $"{director.FirstName} {director.LastName}";
        }

        public int GetDirectorIdByGivenFullName(string fullName)
        {
            Director director = this.dbContext
                .Directors
                .FirstOrDefault(d => (d.FirstName + d.LastName).ToLower() == fullName);

            if (director == null)
            {
                return 0;
            }

            return director.Id;
        }

        public DirectorViewModel GetDirectorForParticularMovie(int directorId)
            => this.dbContext
                .Directors
                .Select(d => new DirectorViewModel()
                {
                    Id = d.Id,
                    FullName = d.FirstName + " " + d.LastName,
                    InformationLink = d.InformationUrl,
                })
                .FirstOrDefault(d => d.Id == directorId);

        // Update
        // Delete

        // Useful methods
        private string[] SplitDirectorFullName(string fullName)
            => fullName
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .ToArray();
    }
}
