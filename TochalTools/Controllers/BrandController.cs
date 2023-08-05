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
    [Authorize(Roles = "Developer,SuperAdministrator,Brand")]
    public class BrandController : Controller
    {
        BrandService brandService = new BrandService();
        public async Task<ActionResult> List()
        {
            return View(await brandService.ReadAll());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Title,Description,Language,IsAtHomeActive")] BrandCreateViewModel viewModel, HttpPostedFileBase File)
        {
            if (ModelState.IsValid)
            {
                var brandId = await brandService.Create(viewModel);
                StorageHelper.SaveFile(File, "Images", "Brand", brandId.ToString(), ".jpg");
            }
            return RedirectToAction("List");
        }

        public async Task<ActionResult> Edit(int id)
        {
            var model = await brandService.ReadByIdForEdit(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Title,Description,Language,IsAtHomeActive")] BrandEditViewModel viewModel, HttpPostedFileBase File)
        {
            if (ModelState.IsValid)
            {
                var result = await brandService.Edit(viewModel);
                if (result)
                {
                    StorageHelper.SaveFile(File, "Images", "Brand", viewModel.Id.ToString(), ".jpg");
                }
            }
            return RedirectToAction("List");
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            return Json(await brandService.Delete(id), JsonRequestBehavior.AllowGet);
        }
    }
}