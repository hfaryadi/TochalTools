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
    [Authorize(Roles = "Developer,SuperAdministrator,Tag")]
    public class TagController : Controller
    {
        TagService tagService = new TagService();
        public async Task<ActionResult> List()
        {
            return View(await tagService.ReadAll());
        }

        [AllowAnonymous]
        public async Task<ActionResult> Details(int id, string title)
        {
            var tag = await tagService.ReadById(id);
            if (tag == null)
            {
                return HttpNotFound();
            }
            return View(tag);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Title,Description,Group,Language,OptimizationTitle,OptimizationKeywords,OptimizationDescription")] TagCreateViewModel viewModel, HttpPostedFileBase File)
        {
            if (ModelState.IsValid)
            {
                var tagId = await tagService.Create(viewModel);
                StorageHelper.SaveFile(File, "Images", "Tag", tagId.ToString(), ".jpg");
            }
            return RedirectToAction("List");
        }

        public async Task<ActionResult> Edit(int id)
        {
            var tag = await tagService.ReadByIdForEdit(id);
            if (tag != null)
            {
                return View(tag);
            }
            return RedirectToAction("List");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Title,Description,Group,Language,OptimizationTitle,OptimizationKeywords,OptimizationDescription")] TagEditViewModel viewModel, HttpPostedFileBase File)
        {
            if (ModelState.IsValid)
            {
                var result = await tagService.Edit(viewModel);
                if (result)
                {
                    StorageHelper.SaveFile(File, "Images", "Tag", viewModel.Id.ToString(), ".jpg");
                }
            }
            return RedirectToAction("List");
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            return Json(await tagService.Delete(id), JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public async Task<JsonResult> ReadAllByGroupForSelect(string group)
        {
            return Json(await tagService.ReadAllByGroupForSelect(group), JsonRequestBehavior.AllowGet);
        }
    }
}