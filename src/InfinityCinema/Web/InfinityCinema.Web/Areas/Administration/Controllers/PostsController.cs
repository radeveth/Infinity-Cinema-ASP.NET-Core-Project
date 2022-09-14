namespace InfinityCinema.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using InfinityCinema.Data;
    using InfinityCinema.Data.Models.ForumSystem;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    [Area("Administration")]
    public class PostsController : AdministrationController
    {
        private readonly InfinityCinemaDbContext context;

        public PostsController(InfinityCinemaDbContext context)
        {
            this.context = context;
        }

        // GET: Administration/Posts
        public async Task<IActionResult> Index()
        {
            var infinityCinemaDbContext = this.context.Posts.Include(p => p.Category).Include(p => p.User);
            return this.View(await infinityCinemaDbContext.ToListAsync());
        }

        // GET: Administration/Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || this.context.Posts == null)
            {
                return this.NotFound();
            }

            var post = await this.context.Posts
                .Include(p => p.Category)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return this.NotFound();
            }

            return this.View(post);
        }

        // GET: Administration/Posts/Create
        public IActionResult Create()
        {
            this.ViewData["CategoryId"] = new SelectList(this.context.Categories, "Id", "ImageUrl");
            this.ViewData["UserId"] = new SelectList(this.context.Users, "Id", "Id");
            return this.View();
        }

        // POST: Administration/Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Content,UserId,CategoryId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Post post)
        {
            if (this.ModelState.IsValid)
            {
                this.context.Add(post);
                await this.context.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            this.ViewData["CategoryId"] = new SelectList(this.context.Categories, "Id", "ImageUrl", post.CategoryId);
            this.ViewData["UserId"] = new SelectList(this.context.Users, "Id", "Id", post.UserId);
            return this.View(post);
        }

        // GET: Administration/Posts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || this.context.Posts == null)
            {
                return this.NotFound();
            }

            var post = await this.context.Posts.FindAsync(id);
            if (post == null)
            {
                return this.NotFound();
            }

            this.ViewData["CategoryId"] = new SelectList(this.context.Categories, "Id", "ImageUrl", post.CategoryId);
            this.ViewData["UserId"] = new SelectList(this.context.Users, "Id", "Id", post.UserId);
            return this.View(post);
        }

        // POST: Administration/Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Title,Content,UserId,CategoryId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Post post)
        {
            if (id != post.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.context.Update(post);
                    await this.context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.PostExists(post.Id))
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

            this.ViewData["CategoryId"] = new SelectList(this.context.Categories, "Id", "ImageUrl", post.CategoryId);
            this.ViewData["UserId"] = new SelectList(this.context.Users, "Id", "Id", post.UserId);
            return this.View(post);
        }

        // GET: Administration/Posts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || this.context.Posts == null)
            {
                return this.NotFound();
            }

            var post = await this.context.Posts
                .Include(p => p.Category)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return this.NotFound();
            }

            return this.View(post);
        }

        // POST: Administration/Posts/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (this.context.Posts == null)
            {
                return this.Problem("Entity set 'InfinityCinemaDbContext.Posts'  is null.");
            }

            var post = await this.context.Posts.FindAsync(id);
            if (post != null)
            {
                this.context.Posts.Remove(post);
            }

            await this.context.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool PostExists(int id)
        {
            return this.context.Posts.Any(e => e.Id == id);
        }
    }
}
