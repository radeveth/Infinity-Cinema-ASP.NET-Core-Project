namespace InfinityCinema.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class ApplicationUsersController : BaseController
    {
        [HttpGet]
        public IActionResult SavedMovies()
        {
            return this.View();
        }
    }
}
