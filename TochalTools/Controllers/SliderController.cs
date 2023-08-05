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
    public class SliderController : Controller
    {
        SliderService sliderService = new SliderService();
        public async Task<ActionResult> List()
        {
            return View(await sliderService.ReadAll());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Title,Description,Link,Priority,Language")] SliderCreateViewModel viewModel, HttpPostedFileBase File)
        {
            if (ModelState.IsValid)
            {
                var sliderId = await sliderService.Create(viewModel);
                if (sliderId > 0)
                {
                    StorageHelper.SaveFile(File, "Images", "Slider", sliderId.ToString(), ".jpg");
                }
            }
            return RedirectToAction("List");
        }

        public async Task<ActionResult> Edit(int id)
        {
            var model = await sliderService.ReadByIdForEdit(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Title,Description,Link,Priority,Language")] SliderEditViewModel viewModel, HttpPostedFileBase File)
        {
            if (ModelState.IsValid)
            {
                var result = await sliderService.Edit(viewModel);
                if (result)
                {
                    StorageHelper.SaveFile(File, "Images", "Slider", viewModel.Id.ToString(), ".jpg");
                }
            }
            return RedirectToAction("List");
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            return Json(await sliderService.Delete(id), JsonRequestBehavior.AllowGet);
        }
    }
}