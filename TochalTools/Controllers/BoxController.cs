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
    [Authorize(Roles = "Developer,SuperAdministrator")]
    public class BoxController : Controller
    {
        BoxService boxService = new BoxService();
        public async Task<ActionResult> List()
        {
            return View(await boxService.ReadAll());
        }

        [Authorize(Roles = "Developer")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Developer")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Title,Address,Content,Icon,Link,Priority,Language")] BoxCreateViewModel viewModel, HttpPostedFileBase File)
        {
            if (ModelState.IsValid)
            {
                var boxId = await boxService.Create(viewModel);
                if (boxId > 0)
                {
                    StorageHelper.SaveFile(File, "Images", "Box", boxId.ToString(), ".jpg");
                }
            }
            return RedirectToAction("List");
        }

        public async Task<ActionResult> Edit(int id)
        {
            var model = await boxService.ReadByIdForEdit(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Title,Address,Content,Icon,Link,Priority,Language")] BoxEditViewModel viewModel, HttpPostedFileBase File)
        {
            if (ModelState.IsValid)
            {
                var result = await boxService.Edit(viewModel);
                if (result)
                {
                    StorageHelper.SaveFile(File, "Images", "Box", viewModel.Id.ToString(), ".jpg");
                }
            }
            return RedirectToAction("List");
        }

        [HttpPost]
        [Authorize(Roles = "Developer")]
        public async Task<JsonResult> Delete(int id)
        {
            return Json(await boxService.Delete(id), JsonRequestBehavior.AllowGet);
        }
    }
}