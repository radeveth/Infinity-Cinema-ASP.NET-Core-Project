namespace InfinityCinema.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using InfinityCinema.Data.Models.ForumSystem;

    public class ForumCategoriesSeeder : ISeeder
    {
        public async Task SeedAsync(InfinityCinemaDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Categories.Any())
            {
                return;
            }

            ICollection<Category> categories = new List<Category>()
            {
                new Category()
                {
                    Title = "General Movie Discussion",
                    ImageUrl = "https://bravewriter.com/assets/images/online-classes/movie-discussion-club.jpg",
                },
                new Category()
                {
                    Title = "Events",
                    ImageUrl = "https://images.pexels.com/photos/2774556/pexels-photo-2774556.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
                },
                new Category()
                {
                    Title = "News",
                    ImageUrl = "https://play-lh.googleusercontent.com/Hus6nxcKHJlIcnyMaL6jCtjaftpeprDCRr4DPiJVgYJgGn9O0qvVgWj0pmRvWfblZJk",
                },
                new Category()
                {
                    Title = "Movie Reviews",
                    ImageUrl = "https://ninjaessays.us/wp-content/uploads/2018/05/movie-review-1.jpg",
                },
                new Category()
                {
                    Title = "Actors, Awards, & Directors",
                    ImageUrl = "https://bestactoranddirectorawards.com/wp-content/uploads/sites/11/2021/08/BADA_trophy_2021.png",
                },
            };

            await dbContext.Categories.AddRangeAsync(categories);
        }
    }
}
