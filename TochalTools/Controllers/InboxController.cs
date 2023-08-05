using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TochalTools.Models;
using TochalTools.Services;

namespace TochalTools.Controllers
{
    [Authorize]
    public class InboxController : Controller
    {
        InboxService inboxService = new InboxService();
        public async Task<ActionResult> List(string id)
        {
            ViewBag.Id = id;
            return View(await inboxService.ReadAll(id));
        }

        public async Task<ActionResult> Details(int id)
        {
            var model = await inboxService.ReadById(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        [Authorize(Roles = "Developer,SuperAdministrator,Administrator")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Developer,SuperAdministrator,Administrator")]
        public async Task<ActionResult> Create(InboxCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await inboxService.Create(viewModel);
            }
            return RedirectToAction("List", new { id = "OutBox" });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> CustomCreate(InboxCustomCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                return Json(await inboxService.CustomCreate(viewModel), JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            return Json(await inboxService.Delete(id), JsonRequestBehavior.AllowGet);
        }
    }
}