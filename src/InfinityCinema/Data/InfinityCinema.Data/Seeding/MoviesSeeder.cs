namespace InfinityCinema.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using InfinityCinema.Data.Models;
    using InfinityCinema.Data.Models.Enums;

    public class MoviesSeeder : ISeeder
    {
        public async Task SeedAsync(InfinityCinemaDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Movies.Any())
            {
                return;
            }

            await this.SeedingFirstMovieAsync(dbContext);
            await this.SeedingSecondMovieAsync(dbContext);
            await this.SeedingThirdMovieAsync(dbContext);
        }

        /// <summary>
        /// Initial seeding of first movie data in application.
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        private async Task SeedingFirstMovieAsync(InfinityCinemaDbContext dbContext)
        {
            // Seeding director for movie
            Director director = new Director()
            {
                FirstName = "Patrick",
                LastName = "Hughes",
                InformationUrl = "https://www.imdb.com/name/nm0400850/?ref_=tt_cl_dr_1",
            };
            await dbContext.Directors.AddAsync(director);
            await dbContext.SaveChangesAsync();

            // Seeding country for movie
            Country country = new Country()
            {
                Name = "USA",
                Abbreviation = "USA",
            };
            await dbContext.Countries.AddAsync(country);
            await dbContext.SaveChangesAsync();

            // Seeding movie
            string administratorRoleId = dbContext.Roles.Where(r => r.Name == "Administrator").Select(r => r.Id).FirstOrDefault();
            string randomUserIdWhoIsAdmin = dbContext.UserRoles.Where(u => u.RoleId == administratorRoleId).Select(u => u.UserId).FirstOrDefault();
            Movie movie = new Movie()
            {
                Name = "The Expendables 3",
                TrailerPath = @"https://youtu.be/4xD0junWlFc",
                Description = "Barney (Stallone), Christmas (Statham) and the rest of the team comes face-to-face with Conrad Stonebanks (Gibson), who years ago co-founded The Expendables with Barney. Stonebanks subsequently became a ruthless arms trader and someone who Barney was forced to kill - or so he thought. Stonebanks, who eluded death once before, now is making it his mission to end The Expendables -- but Barney has other plans. Barney decides that he has to fight old blood with new blood, and brings in a new era of Expendables team members, recruiting individuals who are younger, faster and more tech-savvy. The latest mission becomes a clash of classic old-school style versus high-tech expertise in the Expendables' most personal battle yet.",
                CountryId = country.Id,
                DirectorId = director.Id,
                DateOfReleased = DateTime.Parse("2014-08-15 00:00:00.0000000"),
                Resolution = Resolution.HD,
                Duration = "2h 11m",
                UserId = randomUserIdWhoIsAdmin,
            };
            await dbContext.Movies.AddAsync(movie);
            await dbContext.SaveChangesAsync();

            // Seedeing actors for movie
            List<Actor> actors = new List<Actor>()
            {
                new Actor()
                {
                    FirstName = "Sylvester",
                    LastName = "Stallone",
                    ImageUrl = @"https://m.media-amazon.com/images/M/MV5BMTQwMTk3NDU2OV5BMl5BanBnXkFtZTcwNTA3MTI0Mw@@._V1_UY317_CR6,0,214,317_AL_.jpg",
                    InformationLink = @"https://www.imdb.com/name/nm0000230/",
                },
                new Actor()
                {
                    FirstName = "Jason",
                    LastName = "Statham",
                    ImageUrl = @"https://m.media-amazon.com/images/M/MV5BMTkxMzk2MDkwOV5BMl5BanBnXkFtZTcwMDAxODQwMg@@._V1_UX214_CR0,0,214,317_AL_.jpg",
                    InformationLink = @"https://www.imdb.com/name/nm0005458/",
                },
                new Actor()
                {
                    FirstName = "Antonio",
                    LastName = "Banderas",
                    ImageUrl = @"https://m.media-amazon.com/images/M/MV5BMTUyOTQ3NTYyNF5BMl5BanBnXkFtZTcwMTY2NjIzNQ@@._V1_UX214_CR0,0,214,317_AL_.jpg",
                    InformationLink = @"https://www.imdb.com/name/nm0000104/",
                },
                new Actor()
                {
                    FirstName = "Dolph",
                    LastName = "Lundgren",
                    ImageUrl = @"https://m.media-amazon.com/images/M/MV5BMTUyMzEyNzU4NV5BMl5BanBnXkFtZTgwNDg2MzM3MDE@._V1_UX214_CR0,0,214,317_AL_.jpg",
                    InformationLink = @"https://www.imdb.com/name/nm0000185/",
                },
                new Actor()
                {
                    FirstName = "Jet",
                    LastName = "Li",
                    ImageUrl = @"https://m.media-amazon.com/images/M/MV5BMjAxNjc0MjIyM15BMl5BanBnXkFtZTcwNTM2NDA4MQ@@._V1_UY317_CR24,0,214,317_AL_.jpg",
                    InformationLink = @"https://www.imdb.com/name/nm0001472/",
                },
                new Actor()
                {
                    FirstName = "Harrison",
                    LastName = "Ford",
                    ImageUrl = @"https://m.media-amazon.com/images/M/MV5BMTY4Mjg0NjIxOV5BMl5BanBnXkFtZTcwMTM2NTI3MQ@@._V1_UX214_CR0,0,214,317_AL_.jpg",
                    InformationLink = @"https://www.imdb.com/name/nm0000148/",
                },
                new Actor()
                {
                    FirstName = "Arnold",
                    LastName = "Schwarzenegger",
                    ImageUrl = @"https://m.media-amazon.com/images/M/MV5BMTI3MDc4NzUyMV5BMl5BanBnXkFtZTcwMTQyMTc5MQ@@._V1_UY317_CR19,0,214,317_AL_.jpg",
                    InformationLink = @"https://www.imdb.com/name/nm0000216/",
                },
                new Actor()
                {
                    FirstName = "Mel",
                    LastName = "Gibson",
                    ImageUrl = @"https://m.media-amazon.com/images/M/MV5BNTUzOTMwNTM0OV5BMl5BanBnXkFtZTcwNDQwMTUxMw@@._V1_UY317_CR8,0,214,317_AL_.jpg",
                    InformationLink = @"https://www.imdb.com/name/nm0000154/?ref_=tt_cl_t_7",
                },
                new Actor()
                {
                    FirstName = "Wesley",
                    LastName = "Snipes",
                    ImageUrl = @"https://m.media-amazon.com/images/M/MV5BMTI4Mzk4MDk2NV5BMl5BanBnXkFtZTYwODgxNjc4._V1_UX214_CR0,0,214,317_AL_.jpg",
                    InformationLink = @"https://www.imdb.com/name/nm0000648/?ref_=tt_cl_i_8",
                },
                new Actor()
                {
                    FirstName = "Randy",
                    LastName = "Couture",
                    ImageUrl = @"https://m.media-amazon.com/images/M/MV5BMjMxMDkwMTI5OV5BMl5BanBnXkFtZTgwMjQ3NzI4MTE@._V1_UX214_CR0,0,214,317_AL_.jpg",
                    InformationLink = @"https://www.imdb.com/name/nm1330276/?ref_=tt_cl_i_10",
                },
                new Actor()
                {
                    FirstName = "Terry",
                    LastName = "Crews",
                    ImageUrl = @"https://m.media-amazon.com/images/M/MV5BMjE1ODY0NzE4N15BMl5BanBnXkFtZTcwMTY5Mzk0Mw@@._V1_UX214_CR0,0,214,317_AL_.jpg",
                    InformationLink = @"https://www.imdb.com/name/nm0187719/?ref_=tt_cl_i_11",
                },
                new Actor()
                {
                    FirstName = "Kelsey",
                    LastName = "Grammer",
                    ImageUrl = @"https://m.media-amazon.com/images/M/MV5BMjEyNjkxODM5N15BMl5BanBnXkFtZTcwMTU3NDE1Mg@@._V1_UY317_CR9,0,214,317_AL_.jpg",
                    InformationLink = @"https://www.imdb.com/name/nm0001288/?ref_=tt_cl_i_12",
                },
                new Actor()
                {
                    FirstName = "Glen",
                    LastName = "Powell",
                    ImageUrl = @"https://m.media-amazon.com/images/M/MV5BMGEzMTIwM2UtYjc5MC00ZGI4LWJiOTAtYzAwZmU0OTYzYWIxXkEyXkFqcGdeQXVyNDg0MzQyNA@@._V1_UX214_CR0,0,214,317_AL_.jpg",
                    InformationLink = @"https://www.imdb.com/name/nm1412974/?ref_=tt_cl_i_13",
                },
                new Actor()
                {
                    FirstName = "Victor",
                    LastName = "Ortiz",
                    ImageUrl = @"https://m.media-amazon.com/images/M/MV5BMjA5MTQwNzA2NF5BMl5BanBnXkFtZTgwNjIyMDYxODE@._V1_UX214_CR0,0,214,317_AL_.jpg",
                    InformationLink = @"https://www.imdb.com/name/nm4244770/?ref_=tt_cl_i_14",
                },
                new Actor()
                {
                    FirstName = "Ronda",
                    LastName = "Rousey",
                    ImageUrl = @"https://m.media-amazon.com/images/M/MV5BMjMzOTYwODY4NF5BMl5BanBnXkFtZTgwNzI2ODY4NTE@._V1_UY317_CR14,0,214,317_AL_.jpg",
                    InformationLink = @"https://www.imdb.com/name/nm3313925/?ref_=tt_cl_i_15",
                },
            };
            await dbContext.Actors.AddRangeAsync(actors);
            await dbContext.SaveChangesAsync();

            // Seeding mapping tables between actors and movies
            foreach (var actorId in actors.Select(a => a.Id))
            {
                await dbContext.MovieActors.AddAsync(new MovieActor() { ActorId = actorId, MovieId = movie.Id, });
            }

            await dbContext.SaveChangesAsync();

            // Seeding Genres for movie
            await dbContext.MovieGenres
                .AddRangeAsync(new MovieGenre() { MovieId = movie.Id, GenreId = dbContext.Genres.Where(g => g.Name == "Action").Select(g => g.Id).FirstOrDefault() });
            await dbContext.SaveChangesAsync();

            // Seeding images for movie
            List<Image> images = new List<Image>()
            {
                new Image()
                {
                    Url = @"https://media.npr.org/assets/img/2014/08/14/the-expendables-3---final-one-sheet_custom-441d5ac7a0c7373da8171fcd39051c6f7634ca00-s1100-c50.jpg",
                    MovieId = movie.Id,
                },
                new Image()
                {
                    Url = @"https://www.denofgeek.com/wp-content/uploads/2014/08/the_expendables_3.jpg?fit=620%2C368",
                    MovieId = movie.Id,
                },
                new Image()
                {
                    Url = @"https://m.media-amazon.com/images/M/MV5BNzMyMDc3MDA3OF5BMl5BanBnXkFtZTcwNTYzNjAyOA@@._V1_.jpg",
                    MovieId = movie.Id,
                },
                new Image()
                {
                    Url = @"https://i.ytimg.com/vi/DSu9S3UdvKw/maxresdefault.jpg",
                    MovieId = movie.Id,
                },
                new Image()
                {
                    Url = @"https://m.media-amazon.com/images/M/MV5BMTYyNjI3NzM5NV5BMl5BanBnXkFtZTcwMDYzNjAyOA@@._V1_.jpg",
                    MovieId = movie.Id,
                },
                new Image()
                {
                    Url = @"https://media.tabloidbintang.com/files/thumb/bf2b0eee7c030af6206fe116a72ab9e5.jpg/745",
                    MovieId = movie.Id,
                },
                new Image()
                {
                    Url = @"https://www.slantmagazine.com/wp-content/uploads/2014/08/expendables3.jpg",
                    MovieId = movie.Id,
                },
            };

            // Seeding platforms for movie
            List<Platform> platforms = new List<Platform>()
            {
                new Platform()
                {
                    Name = "Netflix",
                    IconUrl = @"https://cdn.icon-icons.com/icons2/3053/PNG/512/netflix_macos_bigsur_icon_189917.png",
                    PathUrl = @"https://www.netflix.com/",
                },
            };
            foreach (var platformId in platforms.Select(p => p.Id))
            {
                await dbContext.MoviePlatform.AddAsync(new MoviePlatform() { MovieId = movie.Id, PlatformId = platformId });
            }

            await dbContext.SaveChangesAsync();

            // Seeding Lamguages for movie
            List<Language> languages = new List<Language>()
            {
                new Language()
                {
                    Name = "English",
                },
                new Language()
                {
                    Name = "Bulgarian",
                },
                new Language()
                {
                    Name = "Garman",
                },
                new Language()
                {
                    Name = "Italian",
                },
                new Language()
                {
                    Name = "English",
                },
            };
            await dbContext.SaveChangesAsync();

            foreach (var languageId in languages.Select(l => l.Id))
            {
                await dbContext.MovieLanguages.AddAsync(new MovieLanguage() { MovieId = movie.Id, LanguageId = languageId });
            }

            await dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Initial seeding of second movie data in application.
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        private Task SeedingSecondMovieAsync(InfinityCinemaDbContext dbContext)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Initial seeding of third movie data in application.
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        private Task SeedingThirdMovieAsync(InfinityCinemaDbContext dbContext)
        {
            throw new NotImplementedException();
        }
    }
}
