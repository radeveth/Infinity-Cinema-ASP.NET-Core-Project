namespace InfinityCinema.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class GenresController : Controller
    {
        public IActionResult All()
        {
            return this.View();
        }
    }
}
