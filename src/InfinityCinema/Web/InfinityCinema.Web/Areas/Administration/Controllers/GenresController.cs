namespace InfinityCinema.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using InfinityCinema.Data;
    using InfinityCinema.Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Area("Administration")]
    public class GenresController : AdministrationController
    {
        private readonly InfinityCinemaDbContext context;

        public GenresController(InfinityCinemaDbContext context)
        {
            this.context = context;
        }

        // GET: Administration/Genres
        public async Task<IActionResult> Index()
        {
            return this.View(await this.context.Genres.ToListAsync());
        }

        // GET: Administration/Genres/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || this.context.Genres == null)
            {
                return this.NotFound();
            }

            var genre = await this.context.Genres
                .FirstOrDefaultAsync(m => m.Id == id);
            if (genre == null)
            {
                return this.NotFound();
            }

            return this.View(genre);
        }

        // GET: Administration/Genres/Create
        public IActionResult Create()
        {
            return this.View();
        }

        // POST: Administration/Genres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,ImageUrl,Description,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Genre genre)
        {
            if (this.ModelState.IsValid)
            {
                this.context.Add(genre);
                await this.context.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(genre);
        }

        // GET: Administration/Genres/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || this.context.Genres == null)
            {
                return this.NotFound();
            }

            var genre = await this.context.Genres.FindAsync(id);
            if (genre == null)
            {
                return this.NotFound();
            }

            return this.View(genre);
        }

        // POST: Administration/Genres/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,ImageUrl,Description,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Genre genre)
        {
            if (id != genre.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.context.Update(genre);
                    await this.context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.GenreExists(genre.Id))
                    {
                        return this.NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(genre);
        }

        // GET: Administration/Genres/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || this.context.Genres == null)
            {
                return this.NotFound();
            }

            var genre = await this.context.Genres
                .FirstOrDefaultAsync(m => m.Id == id);
            if (genre == null)
            {
                return this.NotFound();
            }

            return this.View(genre);
        }

        // POST: Administration/Genres/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (this.context.Genres == null)
            {
                return this.Problem("Entity set 'InfinityCinemaDbContext.Genres'  is null.");
            }

            var genre = await this.context.Genres.FindAsync(id);
            if (genre != null)
            {
                this.context.Genres.Remove(genre);
            }

            await this.context.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool GenreExists(int id)
        {
            return this.context.Genres.Any(e => e.Id == id);
        }
    }
}
