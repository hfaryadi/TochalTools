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
    public class PageController : Controller
    {
        PageService pageService = new PageService();
        public async Task<ActionResult> List()
        {
            return View(await pageService.ReadAll());
        }

        [AllowAnonymous]
        public async Task<ActionResult> Details(string id)
        {
            if (id == null || id == "")
            {
                return HttpNotFound();
            }
            var model = await pageService.ReadByTitle(id.Replace('-', ' '));
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Title,Content,Language,OptimizationTitle,OptimizationKeywords,OptimizationDescription")] PageCreateViewModel viewModel, HttpPostedFileBase File)
        {
            if (ModelState.IsValid)
            {
                var pageId = await pageService.Create(viewModel);
                if (pageId > 0)
                {
                    StorageHelper.SaveFile(File, "Images", "Page", pageId.ToString(), ".jpg");
                }
            }
            return RedirectToAction("List");
        }

        public async Task<ActionResult> Edit(int id)
        {
            var model = await pageService.ReadByIdForEdit(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Title,Content,Language,OptimizationTitle,OptimizationKeywords,OptimizationDescription")] PageEditViewModel viewModel, HttpPostedFileBase File)
        {
            if (ModelState.IsValid)
            {
                var result = await pageService.Edit(viewModel);
                if (result)
                {
                    StorageHelper.SaveFile(File, "Images", "Page", viewModel.Id.ToString(), ".jpg");
                }
            }
            return RedirectToAction("List");
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            return Json(await pageService.Delete(id), JsonRequestBehavior.AllowGet);
        }
    }
}