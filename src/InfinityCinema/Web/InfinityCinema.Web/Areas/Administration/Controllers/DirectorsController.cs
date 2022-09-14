namespace InfinityCinema.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using InfinityCinema.Data;
    using InfinityCinema.Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Area("Administration")]
    public class DirectorsController : AdministrationController
    {
        private readonly InfinityCinemaDbContext context;

        public DirectorsController(InfinityCinemaDbContext context)
        {
            this.context = context;
        }

        // GET: Administration/Directors
        public async Task<IActionResult> Index()
        {
            return this.View(await this.context.Directors.ToListAsync());
        }

        // GET: Administration/Directors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || this.context.Directors == null)
            {
                return this.NotFound();
            }

            var director = await this.context.Directors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (director == null)
            {
                return this.NotFound();
            }

            return this.View(director);
        }

        // GET: Administration/Directors/Create
        public IActionResult Create()
        {
            return this.View();
        }

        // POST: Administration/Directors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,InformationUrl,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Director director)
        {
            if (this.ModelState.IsValid)
            {
                this.context.Add(director);
                await this.context.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(director);
        }

        // GET: Administration/Directors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || this.context.Directors == null)
            {
                return this.NotFound();
            }

            var director = await this.context.Directors.FindAsync(id);
            if (director == null)
            {
                return this.NotFound();
            }

            return this.View(director);
        }

        // POST: Administration/Directors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FirstName,LastName,InformationUrl,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Director director)
        {
            if (id != director.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.context.Update(director);
                    await this.context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.DirectorExists(director.Id))
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

            return this.View(director);
        }

        // GET: Administration/Directors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || this.context.Directors == null)
            {
                return this.NotFound();
            }

            var director = await this.context.Directors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (director == null)
            {
                return this.NotFound();
            }

            return this.View(director);
        }

        // POST: Administration/Directors/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (this.context.Directors == null)
            {
                return this.Problem("Entity set 'InfinityCinemaDbContext.Directors'  is null.");
            }

            var director = await this.context.Directors.FindAsync(id);
            if (director != null)
            {
                this.context.Directors.Remove(director);
            }

            await this.context.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool DirectorExists(int id)
        {
            return this.context.Directors.Any(e => e.Id == id);
        }
    }
}
