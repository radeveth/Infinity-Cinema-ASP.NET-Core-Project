namespace InfinityCinema.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using InfinityCinema.Data;
    using InfinityCinema.Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    [Area("Administration")]
    public class ImagesController : AdministrationController
    {
        private readonly InfinityCinemaDbContext context;

        public ImagesController(InfinityCinemaDbContext context)
        {
            this.context = context;
        }

        // GET: Administration/Images
        public async Task<IActionResult> Index()
        {
            var infinityCinemaDbContext = this.context.Images.Include(i => i.Movie);
            return this.View(await infinityCinemaDbContext.ToListAsync());
        }

        // GET: Administration/Images/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || this.context.Images == null)
            {
                return this.NotFound();
            }

            var image = await this.context.Images
                .Include(i => i.Movie)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (image == null)
            {
                return this.NotFound();
            }

            return this.View(image);
        }

        // GET: Administration/Images/Create
        public IActionResult Create()
        {
            this.ViewData["MovieId"] = new SelectList(this.context.Movies, "Id", "Description");
            return this.View();
        }

        // POST: Administration/Images/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Url,MovieId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Image image)
        {
            if (this.ModelState.IsValid)
            {
                this.context.Add(image);
                await this.context.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            this.ViewData["MovieId"] = new SelectList(this.context.Movies, "Id", "Description", image.MovieId);
            return this.View(image);
        }

        // GET: Administration/Images/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || this.context.Images == null)
            {
                return this.NotFound();
            }

            var image = await this.context.Images.FindAsync(id);
            if (image == null)
            {
                return this.NotFound();
            }

            this.ViewData["MovieId"] = new SelectList(this.context.Movies, "Id", "Description", image.MovieId);
            return this.View(image);
        }

        // POST: Administration/Images/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Url,MovieId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Image image)
        {
            if (id != image.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.context.Update(image);
                    await this.context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.ImageExists(image.Id))
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

            this.ViewData["MovieId"] = new SelectList(this.context.Movies, "Id", "Description", image.MovieId);
            return this.View(image);
        }

        // GET: Administration/Images/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || this.context.Images == null)
            {
                return this.NotFound();
            }

            var image = await this.context.Images
                .Include(i => i.Movie)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (image == null)
            {
                return this.NotFound();
            }

            return this.View(image);
        }

        // POST: Administration/Images/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (this.context.Images == null)
            {
                return this.Problem("Entity set 'InfinityCinemaDbContext.Images'  is null.");
            }

            var image = await this.context.Images.FindAsync(id);
            if (image != null)
            {
                this.context.Images.Remove(image);
            }

            await this.context.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool ImageExists(int id)
        {
            return this.context.Images.Any(e => e.Id == id);
        }
    }
}
