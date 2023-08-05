using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TochalTools.Services;

namespace TochalTools.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        DashboardService dashboardService = new DashboardService();

        [Authorize(Roles = "Developer,SuperAdministrator,Administrator")]
        public async Task<ActionResult> Admin()
        {
            return View(await dashboardService.Admin());
        }

        [Authorize(Roles = "Developer,SuperAdministrator,Administrator,User")]
        public async Task<ActionResult> User()
        {
            return View(await dashboardService.User());
        }
    }
}