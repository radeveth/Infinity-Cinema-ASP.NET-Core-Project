namespace InfinityCinema.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using InfinityCinema.Data;
    using InfinityCinema.Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Area("Administration")]
    public class ApplicationRolesController : AdministrationController
    {
        private readonly InfinityCinemaDbContext context;

        public ApplicationRolesController(InfinityCinemaDbContext context)
        {
            this.context = context;
        }

        // GET: Administration/ApplicationRoles
        public async Task<IActionResult> Index()
        {
            return this.View(await this.context.Roles.ToListAsync());
        }

        // GET: Administration/ApplicationRoles/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || this.context.Roles == null)
            {
                return this.NotFound();
            }

            var applicationRole = await this.context.Roles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicationRole == null)
            {
                return this.NotFound();
            }

            return this.View(applicationRole);
        }

        // GET: Administration/ApplicationRoles/Create
        public IActionResult Create()
        {
            return this.View();
        }

        // POST: Administration/ApplicationRoles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CreatedOn,ModifiedOn,IsDeleted,DeletedOn,Id,Name,NormalizedName,ConcurrencyStamp")] ApplicationRole applicationRole)
        {
            if (this.ModelState.IsValid)
            {
                this.context.Add(applicationRole);
                await this.context.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(applicationRole);
        }

        // GET: Administration/ApplicationRoles/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || this.context.Roles == null)
            {
                return this.NotFound();
            }

            var applicationRole = await this.context.Roles.FindAsync(id);
            if (applicationRole == null)
            {
                return this.NotFound();
            }

            return this.View(applicationRole);
        }

        // POST: Administration/ApplicationRoles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CreatedOn,ModifiedOn,IsDeleted,DeletedOn,Id,Name,NormalizedName,ConcurrencyStamp")] ApplicationRole applicationRole)
        {
            if (id != applicationRole.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.context.Update(applicationRole);
                    await this.context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.ApplicationRoleExists(applicationRole.Id))
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

            return this.View(applicationRole);
        }

        // GET: Administration/ApplicationRoles/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || this.context.Roles == null)
            {
                return this.NotFound();
            }

            var applicationRole = await this.context.Roles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicationRole == null)
            {
                return this.NotFound();
            }

            return this.View(applicationRole);
        }

        // POST: Administration/ApplicationRoles/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (this.context.Roles == null)
            {
                return this.Problem("Entity set 'InfinityCinemaDbContext.Roles'  is null.");
            }

            var applicationRole = await this.context.Roles.FindAsync(id);
            if (applicationRole != null)
            {
                this.context.Roles.Remove(applicationRole);
            }

            await this.context.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool ApplicationRoleExists(string id)
        {
            return this.context.Roles.Any(e => e.Id == id);
        }
    }
}
