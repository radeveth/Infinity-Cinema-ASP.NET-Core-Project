namespace InfinityCinema.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using InfinityCinema.Data;
    using InfinityCinema.Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Area("Administration")]
    public class ActorsController : AdministrationController
    {
        private readonly InfinityCinemaDbContext context;

        public ActorsController(InfinityCinemaDbContext context)
        {
            this.context = context;
        }

        // GET: Administration/Actors
        public async Task<IActionResult> Index()
        {
            return this.View(await this.context.Actors.ToListAsync());
        }

        // GET: Administration/Actors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || this.context.Actors == null)
            {
                return this.NotFound();
            }

            var actor = await this.context.Actors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (actor == null)
            {
                return this.NotFound();
            }

            return this.View(actor);
        }

        // GET: Administration/Actors/Create
        public IActionResult Create()
        {
            return this.View();
        }

        // POST: Administration/Actors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,ImageUrl,InformationLink,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Actor actor)
        {
            if (this.ModelState.IsValid)
            {
                this.context.Add(actor);
                await this.context.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(actor);
        }

        // GET: Administration/Actors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || this.context.Actors == null)
            {
                return this.NotFound();
            }

            var actor = await this.context.Actors.FindAsync(id);
            if (actor == null)
            {
                return this.NotFound();
            }

            return this.View(actor);
        }

        // POST: Administration/Actors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FirstName,LastName,ImageUrl,InformationLink,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Actor actor)
        {
            if (id != actor.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.context.Update(actor);
                    await this.context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.ActorExists(actor.Id))
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

            return this.View(actor);
        }

        // GET: Administration/Actors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || this.context.Actors == null)
            {
                return this.NotFound();
            }

            var actor = await this.context.Actors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (actor == null)
            {
                return this.NotFound();
            }

            return this.View(actor);
        }

        // POST: Administration/Actors/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (this.context.Actors == null)
            {
                return this.Problem("Entity set 'InfinityCinemaDbContext.Actors'  is null.");
            }

            var actor = await this.context.Actors.FindAsync(id);
            if (actor != null)
            {
                this.context.Actors.Remove(actor);
            }

            await this.context.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool ActorExists(int id)
        {
            return this.context.Actors.Any(e => e.Id == id);
        }
    }
}
