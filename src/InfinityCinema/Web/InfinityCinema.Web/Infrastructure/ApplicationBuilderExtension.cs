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
                new Genre() { Name = "Action", ImageUrl = "https://previews.123rf.com/images/pedrolieb/pedrolieb1210/pedrolieb121000023/16084899-clap-film-of-cinema-action-genre-clapperboard-text-illustration-.jpg?fj=1", Description = "Movies in the action genre are fast-paced and include a lot of action like fight scenes, chase scenes, and slow-motion shots. They can feature superheroes, martial arts, or exciting stunts. These high-octane films are more about the execution of the plot rather than the plot itself. Action movies are thrilling to watch and leave audience members on the edge of their seats. Cop movies, disaster films, and some spy films fall under the action category. James Cameron’s 1994 film True Lies about an American spy starring Arnold Schwarzenegger is an example of the genre. Learn how to write an action screenplay in our comprehensive guide here." },
                new Genre() { Name = "Animation", ImageUrl = "https://www.hitechanimation.com/blog/wp-content/uploads/2018/02/mouse-hd-png1258.png", Description = "Animated film is a collection of illustrations that are photographed frame-by-frame and then played in a quick succession. Since its inception, animation has had a creative and imaginative tendency. Being able to bring animals and objects to life, this genre has catered towards fairy tales and children’s stories. However, animation has long been a genre enjoyed by all ages. As of recent, there has even been an influx of animation geared towards adults. Animation is commonly thought of as a technique, thus it’s ability to span over many different genres." },
                new Genre() { Name = "Comedy", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/c/c2/Film_Comici.svg/1200px-Film_Comici.svg.png", Description = "Comedy films are funny and entertaining. The films in this genre center around a comedic premise—usually putting someone in a challenging, amusing, or humorous situation they’re not prepared to handle. Good comedy movies are less about making constant jokes and more about presenting a universally relatable, real-life story with complex characters who learn an important lesson. Mockumentary, dark comedy (or black comedy), romantic comedy, parody/spoof, and slapstick comedy are all examples of comedy subgenres. The Jerk (1979), starring Steve Martin, is a comedy about a rhythmless and clueless member of a Black family of sharecroppers who realizes he is white." },
                new Genre() { Name = "Crime", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/0/03/Crime_film_clapperboard.svg/1200px-Crime_film_clapperboard.svg.png", Description = "Crime film is a genre that revolves around the action of a criminal mastermind. A Crime film will often revolve around the criminal himself, chronicling his rise and fall. Some Crime films will have a storyline that follows the criminal's victim, yet others follow the person in pursuit of the criminal. This genre tends to be fast paced with an air of mystery – this mystery can come from the plot or from the characters themselves." },
                new Genre() { Name = "Drama", ImageUrl = "https://previews.123rf.com/images/pedrolieb/pedrolieb1210/pedrolieb121000024/16084918-clap-film-of-cinema-drama-genre-clapperboard-text-illustration-.jpg", Description = "The drama genre features stories with high stakes and many conflicts. They’re plot-driven and demand that every character and scene move the story forward. Dramas follow a clearly defined narrative plot structure, portraying real-life scenarios or extreme situations with emotionally-driven characters. Films that fall into drama sub-genres include historical drama or costume drama, romantic drama, teen drama, medical drama, docudrama, film noir, and neo-noir. Citizen Kane (1941), The Godfather (1972), and The Social Network (2010) are examples of dramatic films." },
                new Genre() { Name = "Fantasy", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/9/96/Variance_in_character_design_-_Lia_Turtle%2C_Shain%2C_and_Cendrea_from_Chaos%26Evolutions.png/1920px-Variance_in_character_design_-_Lia_Turtle%2C_Shain%2C_and_Cendrea_from_Chaos%26Evolutions.png", Description = "Films in the fantasy genre feature magical and supernatural elements that do not exist in the real world. Although some films juxtapose a real-world setting with fantastical elements, many create entirely imaginary universes with their own laws, logic, and populations of imaginary races and creatures. Like science fiction films, fantasy films are speculative but unrelated to reality or scientific fact. High fantasy, fairy tales, and magical realism are all fantasy subgenres. The Harry Potter film series, based on the novels by J.K. Rowling, follows a young boy at the Hogwarts School of Witchcraft and Wizardry and is a prime example of a fantasy film." },
                new Genre() { Name = "Historical", ImageUrl = "https://cdn2.vectorstock.com/i/1000x1000/57/66/movie-genre-history-cinema-icon-of-ancient-vector-17935766.jpg", Description = "A genre of film that deals with a major historical event and the actual historical figures involved in it. Often great liberties are taken with the facts to facilitate a 2-hour running time or a particular political agenda. These films pay a great deal of attention to re-creating events that live mostly in our memories or in textbooks. Earlier historical films tended to concentrate on eras and political figures who were far from the present and put little emphasis on undermining the accepted mythologies of history. When the production becomes expensive and sumptuous, the cast of characters, and the narrative sweeping and panoramic, this becomes what is called an epic film." },
                new Genre() { Name = "Horror", ImageUrl = "https://previews.123rf.com/images/pedrolieb/pedrolieb1210/pedrolieb121000025/16084905-clap-film-of-cinema-horror-genre-clapperboard-text-illustration-.jpg", Description = "Horror films feature elements that leave people with an overwhelming sense of fear and dread. Horror movies often include serial killers or monsters as persistent, evil antagonists to play on viewers’ fears or nightmares. Audiences who love the horror genre seek out these movies specifically for the adrenaline rush produced by ghosts, gore, monsters, and jump scares. Films that fall into the horror sub-genres include macabre, ghost stories, gothic horror movies, science fiction horror movies, supernatural movies, dark fantasy movies, psychological horror movies, and slasher movies.The Exorcist (1973) and A Nightmare on Elm Street (1984) fall under the horror genre. Learn how to write a horror screenplay with our comprehensive guide here." },
                new Genre() { Name = "Mystery", ImageUrl = "https://previews.123rf.com/images/pedrolieb/pedrolieb1210/pedrolieb121000034/16084904-clap-film-of-cinema-mystery-genre-clapperboard-text-illustration-.jpg", Description = "Mystery films are all about the puzzle and often feature a detective or amateur sleuth trying to solve it. Mystery films are full of suspense, and the protagonist searches for clues or evidence throughout the movie, piecing together events and interviewing suspects to solve the central question. Hardboiled noirs and police procedurals are two subcategories that often fall under the mystery genre. Murder on the Orient Express (1974), The Girl with the Dragon Tattoo (2011), and Knives Out (2019) are examples." },
                new Genre() { Name = "Romance", ImageUrl = "https://previews.123rf.com/images/pedrolieb/pedrolieb1210/pedrolieb121000028/16084913-clap-film-of-cinema-romance-genre-clapperboard-text-illustration-.jpg", Description = "Romance films are love stories. They center around two protagonists exploring some of the elements of love like relationships, sacrifice, marriage, obsession, or destruction. Romance movies sometimes feature hardships like illness, infidelity, tragedy, or other obstacles for the love interests to overcome. Romantic comedies, gothic romance, and romantic action are some popular romance subgenres. When Harry Met Sally…(1989), starring Meg Ryan and Billy Crystal, is a popular romantic comedy." },
                new Genre() { Name = "Thriller", ImageUrl = "https://previews.123rf.com/images/pedrolieb/pedrolieb1210/pedrolieb121000031/16084898-clap-film-of-cinema-thriller-genre-clapperboard-text-illustration-.jpg", Description = "Thrillers expertly blend mystery, tension, and anticipation into one exciting story. Successful thrillers are well-paced, often introducing red herrings, divulging plot twists, and revealing information at the exact right moments to keep the audience intrigued. Thrillers often include a “ticking clock” aspect, where the stakes are against a finite amount of time. Crime films, political thrillers, and techno-thrillers are all featured in the thriller genre. The Shining (1980) and The Silence of the Lambs (1991) are examples of psychological thrillers." },
                new Genre() { Name = "Western", ImageUrl = "https://previews.123rf.com/images/pedrolieb/pedrolieb1210/pedrolieb121000032/16084902-clap-film-of-cinema-western-genre-clapperboard-text-illustration-.jpg", Description = "Westerns tell the tale of a cowboy or gunslinger pursuing an outlaw in the Wild West. The main character often seeks revenge and will face the criminal in a duel or shootout at the end. Westerns are vivid productions set in the American West—such as the desert, mountains, or plains—that can inspire and inform the characters and the action. Spaghetti westerns, space westerns, and sci-fi westerns are all subgenres within the Western category. The Good, the Bad and the Ugly (1966) and Django Unchained (2012) are examples of Westerns." },
                new Genre() { Name = "Sports", ImageUrl = "https://res.cloudinary.com/jerrick/image/upload/c_scale,f_jpg,q_auto/n7ahytv0xmpgthetuouz.jpg", Description = "Movies in the sports genre will center around a team, individual player, or fan, with the sport itself to motivate the plot and keep the story advancing. These movies aren’t entirely focused on the sport itself, however, mainly using it as a backdrop to provide context into the emotional arcs of the main characters. Sports movies can be dramatic or comical and are often allegorical. Some popular sports movies include The Bad News Bears (1976), A League of Their Own (1992), and Bend It Like Beckham (2003)." },
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

                    const string adminEmail = "infinitycinemaadmin@gmail.com";
                    const string adminPassword = "!IinfinityCcinemaAadmin0713!";
                    const string adminFullName = "Radev Admin";

                    ApplicationUser user = new ApplicationUser
                    {
                        Email = adminEmail,
                        UserName = adminEmail,
                        FullName = adminFullName,
                    };

                    await userManager.CreateAsync(user, adminPassword);

                    await userManager.AddToRoleAsync(user, role.Name);
                })
                .GetAwaiter()
                .GetResult();
        }
    }
}
