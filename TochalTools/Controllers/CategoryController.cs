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
    [Authorize(Roles = "Developer,SuperAdministrator,Category")]
    public class CategoryController : Controller
    {
        CategoryService categoryService = new CategoryService();

        public async Task<ActionResult> List(string id)
        {
            ViewBag.Group = id;
            return View(await categoryService.ReadAll(id));
        }

        public async Task<ActionResult> Create(string id)
        {
            CategoryCreateViewModel viewModel = new CategoryCreateViewModel
            {
                Group = id,
                CategoriesList = await categoryService.ReadAllByGroupForSelect(id)
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Name,Parents,Description,Group,Language,IsPublicationActive")] CategoryCreateViewModel viewModel, HttpPostedFileBase File)
        {
            if (ModelState.IsValid)
            {
                var categoryId = await categoryService.Create(viewModel);
                if (categoryId != "Duplicate")
                {
                    StorageHelper.SaveFile(File, "Images", "Category", categoryId, ".jpg");
                }
            }
            return RedirectToAction("List", new { id = viewModel.Group });
        }

        public async Task<ActionResult> Edit(string id)
        {
            return View(await categoryService.ReadByIdForEdit(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Parents,Description,Group,Language,IsPublicationActive")] CategoryEditViewModel viewModel, HttpPostedFileBase File)
        {
            if (ModelState.IsValid)
            {
                await categoryService.Edit(viewModel);
                StorageHelper.SaveFile(File, "Images", "Category", viewModel.Id, ".jpg");
            }
            return RedirectToAction("List", new { id = viewModel.Group });
        }

        [HttpPost]
        public async Task<JsonResult> Delete(string id)
        {
            return Json(await categoryService.Delete(id), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<JsonResult> ReadAllByGroupForSelect(string group)
        {
            return Json(await categoryService.ReadAllByGroupForSelect(group), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<JsonResult> ReadAllByGroupAndParentForSelect(string group, string parent)
        {
            return Json(await categoryService.ReadAllByGroupAndParentForSelect(group, parent), JsonRequestBehavior.AllowGet);
        }
    }
}