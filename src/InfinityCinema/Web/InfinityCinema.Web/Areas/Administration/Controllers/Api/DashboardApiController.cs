namespace InfinityCinema.Web.Areas.Administration.Controllers.Api
{
    using InfinityCinema.Common;
    using InfinityCinema.Web.Areas.Administration.AdministartionsService;
    using InfinityCinema.Web.Areas.Administration.AdministartionsService.Models;
    using InfinityCinema.Web.Controllers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("/api/administartion/dashboard/")]
    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public class DashboardApiController : BaseController
    {
        private readonly IAdministartionService administartionService;

        public DashboardApiController(IAdministartionService administartionService)
        {
            this.administartionService = administartionService;
        }

        [HttpGet]
        [Route("statistics")]
        public ActionResult<ApplicationStatisticsViewModel> Statistics()
        {
            return this.Json(this.administartionService.ApplicationStatistics());
        }
    }
}
