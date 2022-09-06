namespace InfinityCinema.Web.Areas.Administration.Controllers
{
    using InfinityCinema.Web.Areas.Administration.AdministartionsService;
    using Microsoft.AspNetCore.Mvc;

    public class DashboardController : AdministrationController
    {
        private readonly IAdministartionService administartionService;

        public DashboardController(IAdministartionService administartionService)
        {
            this.administartionService = administartionService;
        }

        public IActionResult Statistics()
        {
            return this.View(this.administartionService.ApplicationStatistics());
        }
    }
}
