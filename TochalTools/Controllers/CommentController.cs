using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TochalTools.Classes;
using TochalTools.Models;
using TochalTools.Services;

namespace TochalTools.Controllers
{
    [Authorize(Roles = "Developer,SuperAdministrator,Comment")]
    public class CommentController : Controller
    {
        CommentService commentService = new CommentService();
        public async Task<ActionResult> List(string group, string confirmed)
        {
            var isConfirmed = (confirmed != null && confirmed.ToLower() == "unconfirmed") ? false : true;
            ViewBag.IsConfirmed = isConfirmed;
            ViewBag.PageTitle = ModuleHelper.ReadPersianNameByISO(group);
            return View(await commentService.ReadAll(group, isConfirmed));
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Create([Bind (Include = "Name,Content,CommentId,Group,RefId")] CommentCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                return Json(await commentService.Create(viewModel), JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> Confirm(int id)
        {
            return Json(await commentService.Confirm(id), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            return Json(await commentService.Delete(id), JsonRequestBehavior.AllowGet);
        }
    }
}