namespace InfinityCinema.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class GenresController1 : BaseController
    {
        public IActionResult All()
        {
            return this.View();
        }
    }
}
