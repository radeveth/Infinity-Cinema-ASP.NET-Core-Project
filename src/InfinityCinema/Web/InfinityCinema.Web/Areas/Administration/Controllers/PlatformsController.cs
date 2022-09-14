namespace InfinityCinema.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using InfinityCinema.Data;
    using InfinityCinema.Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Area("Administration")]
    public class PlatformsController : AdministrationController
    {
        private readonly InfinityCinemaDbContext context;

        public PlatformsController(InfinityCinemaDbContext context)
        {
            this.context = context;
        }

        // GET: Administration/Platforms
        public async Task<IActionResult> Index()
        {
            return this.View(await this.context.Platforms.ToListAsync());
        }

        // GET: Administration/Platforms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || this.context.Platforms == null)
            {
                return this.NotFound();
            }

            var platform = await this.context.Platforms
                .FirstOrDefaultAsync(m => m.Id == id);
            if (platform == null)
            {
                return this.NotFound();
            }

            return this.View(platform);
        }

        // GET: Administration/Platforms/Create
        public IActionResult Create()
        {
            return this.View();
        }

        // POST: Administration/Platforms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,PathUrl,IconUrl,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Platform platform)
        {
            if (this.ModelState.IsValid)
            {
                this.context.Add(platform);
                await this.context.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(platform);
        }

        // GET: Administration/Platforms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || this.context.Platforms == null)
            {
                return this.NotFound();
            }

            var platform = await this.context.Platforms.FindAsync(id);
            if (platform == null)
            {
                return this.NotFound();
            }

            return this.View(platform);
        }

        // POST: Administration/Platforms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,PathUrl,IconUrl,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Platform platform)
        {
            if (id != platform.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.context.Update(platform);
                    await this.context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.PlatformExists(platform.Id))
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

            return this.View(platform);
        }

        // GET: Administration/Platforms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || this.context.Platforms == null)
            {
                return this.NotFound();
            }

            var platform = await this.context.Platforms
                .FirstOrDefaultAsync(m => m.Id == id);
            if (platform == null)
            {
                return this.NotFound();
            }

            return this.View(platform);
        }

        // POST: Administration/Platforms/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (this.context.Platforms == null)
            {
                return this.Problem("Entity set 'InfinityCinemaDbContext.Platforms'  is null.");
            }

            var platform = await this.context.Platforms.FindAsync(id);
            if (platform != null)
            {
                this.context.Platforms.Remove(platform);
            }

            await this.context.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool PlatformExists(int id)
        {
            return this.context.Platforms.Any(e => e.Id == id);
        }
    }
}
