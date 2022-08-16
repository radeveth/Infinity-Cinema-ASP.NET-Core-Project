namespace InfinityCinema.Web.Areas.Administration.Controllers
{
    using InfinityCinema.Common;
    using InfinityCinema.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}
