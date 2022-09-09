namespace InfinityCinema.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class ForumCommentsController : BaseController
    {
        public IActionResult AllForParticularPost(int postId)
        {
            return this.View();
        }
    }
}
