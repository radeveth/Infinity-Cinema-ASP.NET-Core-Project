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
    public class CommentsController : AdministrationController
    {
        private readonly InfinityCinemaDbContext context;

        public CommentsController(InfinityCinemaDbContext context)
        {
            this.context = context;
        }

        // GET: Administration/Comments
        public async Task<IActionResult> Index()
        {
            var infinityCinemaDbContext = this.context.Comments.Include(c => c.Parent).Include(c => c.Post).Include(c => c.User);
            return this.View(await infinityCinemaDbContext.ToListAsync());
        }

        // GET: Administration/Comments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || this.context.Comments == null)
            {
                return this.NotFound();
            }

            var comment = await this.context.Comments
                .Include(c => c.Parent)
                .Include(c => c.Post)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comment == null)
            {
                return this.NotFound();
            }

            return this.View(comment);
        }

        // GET: Administration/Comments/Create
        public IActionResult Create()
        {
            this.ViewData["ParentId"] = new SelectList(this.context.Comments, "Id", "Content");
            this.ViewData["PostId"] = new SelectList(this.context.Posts, "Id", "Content");
            this.ViewData["UserId"] = new SelectList(this.context.Users, "Id", "Id");
            return this.View();
        }

        // POST: Administration/Comments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Content,PostId,UserId,ParentId,Id,CreatedOn,ModifiedOn")] Comment comment)
        {
            if (this.ModelState.IsValid)
            {
                this.context.Add(comment);
                await this.context.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            this.ViewData["ParentId"] = new SelectList(this.context.Comments, "Id", "Content", comment.ParentId);
            this.ViewData["PostId"] = new SelectList(this.context.Posts, "Id", "Content", comment.PostId);
            this.ViewData["UserId"] = new SelectList(this.context.Users, "Id", "Id", comment.UserId);
            return this.View(comment);
        }

        // GET: Administration/Comments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || this.context.Comments == null)
            {
                return this.NotFound();
            }

            var comment = await this.context.Comments.FindAsync(id);
            if (comment == null)
            {
                return this.NotFound();
            }

            this.ViewData["ParentId"] = new SelectList(this.context.Comments, "Id", "Content", comment.ParentId);
            this.ViewData["PostId"] = new SelectList(this.context.Posts, "Id", "Content", comment.PostId);
            this.ViewData["UserId"] = new SelectList(this.context.Users, "Id", "Id", comment.UserId);
            return this.View(comment);
        }

        // POST: Administration/Comments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Content,PostId,UserId,ParentId,Id,CreatedOn,ModifiedOn")] Comment comment)
        {
            if (id != comment.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.context.Update(comment);
                    await this.context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.CommentExists(comment.Id))
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

            this.ViewData["ParentId"] = new SelectList(this.context.Comments, "Id", "Content", comment.ParentId);
            this.ViewData["PostId"] = new SelectList(this.context.Posts, "Id", "Content", comment.PostId);
            this.ViewData["UserId"] = new SelectList(this.context.Users, "Id", "Id", comment.UserId);
            return this.View(comment);
        }

        // GET: Administration/Comments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || this.context.Comments == null)
            {
                return this.NotFound();
            }

            var comment = await this.context.Comments
                .Include(c => c.Parent)
                .Include(c => c.Post)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comment == null)
            {
                return this.NotFound();
            }

            return this.View(comment);
        }

        // POST: Administration/Comments/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (this.context.Comments == null)
            {
                return this.Problem("Entity set 'InfinityCinemaDbContext.Comments'  is null.");
            }

            var comment = await this.context.Comments.FindAsync(id);
            if (comment != null)
            {
                this.context.Comments.Remove(comment);
            }

            await this.context.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool CommentExists(int id)
        {
            return this.context.Comments.Any(e => e.Id == id);
        }
    }
}
