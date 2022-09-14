namespace InfinityCinema.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using InfinityCinema.Data;
    using InfinityCinema.Data.Models;
    using InfinityCinema.Services.Data.MoviesService;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    [Area("Administration")]
    public class MoviesController : AdministrationController
    {
        private readonly InfinityCinemaDbContext context;

        public MoviesController(InfinityCinemaDbContext context)
        {
            this.context = context;
        }

        // GET: Administration/Movies
        public async Task<IActionResult> Index()
        {
            var infinityCinemaDbContext = this.context.Movies.Include(m => m.Country).Include(m => m.Director).Include(m => m.User);
            return this.View(await infinityCinemaDbContext.ToListAsync());
        }

        // GET: Administration/Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || this.context.Movies == null)
            {
                return this.NotFound();
            }

            var movie = await this.context.Movies
                .Include(m => m.Country)
                .Include(m => m.Director)
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return this.NotFound();
            }

            return this.View(movie);
        }

        // GET: Administration/Movies/Create
        public IActionResult Create()
        {
            this.ViewData["CountryId"] = new SelectList(this.context.Countries, "Id", "Name");
            this.ViewData["DirectorId"] = new SelectList(this.context.Directors, "Id", "FirstName");
            this.ViewData["UserId"] = new SelectList(this.context.Users, "Id", "Id");
            return this.View();
        }

        // POST: Administration/Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,DateOfReleased,Resolution,Description,TrailerPath,Duration,DirectorId,CountryId,UserId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Movie movie)
        {
            if (this.ModelState.IsValid)
            {
                this.context.Add(movie);
                await this.context.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            this.ViewData["CountryId"] = new SelectList(this.context.Countries, "Id", "Name", movie.CountryId);
            this.ViewData["DirectorId"] = new SelectList(this.context.Directors, "Id", "FirstName", movie.DirectorId);
            this.ViewData["UserId"] = new SelectList(this.context.Users, "Id", "Id", movie.UserId);
            return this.View(movie);
        }

        // GET: Administration/Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || this.context.Movies == null)
            {
                return this.NotFound();
            }

            var movie = await this.context.Movies.FindAsync(id);
            if (movie == null)
            {
                return this.NotFound();
            }

            this.ViewData["CountryId"] = new SelectList(this.context.Countries, "Id", "Name", movie.CountryId);
            this.ViewData["DirectorId"] = new SelectList(this.context.Directors, "Id", "FirstName", movie.DirectorId);
            this.ViewData["UserId"] = new SelectList(this.context.Users, "Id", "Id", movie.UserId);
            return this.View(movie);
        }

        // POST: Administration/Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,DateOfReleased,Resolution,Description,TrailerPath,Duration,DirectorId,CountryId,UserId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Movie movie)
        {
            if (id != movie.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.context.Update(movie);
                    await this.context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.MovieExists(movie.Id))
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

            this.ViewData["CountryId"] = new SelectList(this.context.Countries, "Id", "Name", movie.CountryId);
            this.ViewData["DirectorId"] = new SelectList(this.context.Directors, "Id", "FirstName", movie.DirectorId);
            this.ViewData["UserId"] = new SelectList(this.context.Users, "Id", "Id", movie.UserId);
            return this.View(movie);
        }

        // GET: Administration/Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || this.context.Movies == null)
            {
                return this.NotFound();
            }

            var movie = await this.context.Movies
                .Include(m => m.Country)
                .Include(m => m.Director)
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return this.NotFound();
            }

            return this.View(movie);
        }

        // POST: Administration/Movies/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (this.context.Movies == null)
            {
                return this.Problem("Entity set 'InfinityCinemaDbContext.Movies'  is null.");
            }

            var movie = await this.context.Movies.FindAsync(id);
            if (movie != null)
            {
                this.context.Movies.Remove(movie);
            }

            await this.context.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool MovieExists(int id)
        {
            return this.context.Movies.Any(e => e.Id == id);
        }
    }
}
