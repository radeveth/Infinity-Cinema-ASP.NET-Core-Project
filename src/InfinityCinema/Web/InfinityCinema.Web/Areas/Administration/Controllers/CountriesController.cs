namespace InfinityCinema.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using InfinityCinema.Data;
    using InfinityCinema.Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Area("Administration")]
    public class CountriesController : AdministrationController
    {
        private readonly InfinityCinemaDbContext context;

        public CountriesController(InfinityCinemaDbContext context)
        {
            this.context = context;
        }

        // GET: Administration/Countries
        public async Task<IActionResult> Index()
        {
            return this.View(await this.context.Countries.ToListAsync());
        }

        // GET: Administration/Countries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || this.context.Countries == null)
            {
                return this.NotFound();
            }

            var country = await this.context.Countries
                .FirstOrDefaultAsync(m => m.Id == id);
            if (country == null)
            {
                return this.NotFound();
            }

            return this.View(country);
        }

        // GET: Administration/Countries/Create
        public IActionResult Create()
        {
            return this.View();
        }

        // POST: Administration/Countries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Abbreviation,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Country country)
        {
            if (this.ModelState.IsValid)
            {
                this.context.Add(country);
                await this.context.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(country);
        }

        // GET: Administration/Countries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || this.context.Countries == null)
            {
                return this.NotFound();
            }

            var country = await this.context.Countries.FindAsync(id);
            if (country == null)
            {
                return this.NotFound();
            }

            return this.View(country);
        }

        // POST: Administration/Countries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Abbreviation,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Country country)
        {
            if (id != country.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.context.Update(country);
                    await this.context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.CountryExists(country.Id))
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

            return this.View(country);
        }

        // GET: Administration/Countries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || this.context.Countries == null)
            {
                return this.NotFound();
            }

            var country = await this.context.Countries
                .FirstOrDefaultAsync(m => m.Id == id);
            if (country == null)
            {
                return this.NotFound();
            }

            return this.View(country);
        }

        // POST: Administration/Countries/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (this.context.Countries == null)
            {
                return this.Problem("Entity set 'InfinityCinemaDbContext.Countries'  is null.");
            }

            var country = await this.context.Countries.FindAsync(id);
            if (country != null)
            {
                this.context.Countries.Remove(country);
            }

            await this.context.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool CountryExists(int id)
        {
            return this.context.Countries.Any(e => e.Id == id);
        }
    }
}
