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
    [Authorize(Roles = "Developer,SuperAdministrator,Marketer")]
    public class MarketerController : Controller
    {
        MarketerService marketerService = new MarketerService();
        public async Task<ActionResult> List(string id)
        {
            ViewBag.Id = id;
            return View(await marketerService.ReadAll(id));
        }

        public async Task<ActionResult> Details(int id)
        {
            var model = await marketerService.ReadById(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        [AllowAnonymous]
        public async Task<ActionResult> Print(int id)
        {
            var model = await marketerService.ReadById(id, true);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        [AllowAnonymous]
        public async Task<ActionResult> Create()
        {
            AddressService addressService = new AddressService();
            MarketerCreateViewModel viewModel = new MarketerCreateViewModel
            {
                CountriesList = await addressService.ReadAllCountriesForSelect()
            };
            return View(viewModel);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Name,Tell,Age,CountryId,StateId,CityId,Address,Description")] MarketerCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var trackingCode = await marketerService.Create(viewModel);
                if (trackingCode > 0)
                {
                    return RedirectToAction("Print", new { id = trackingCode});
                }
            }
            return RedirectToAction("Create");
        }

        [HttpPost]
        public async Task<JsonResult> Archive(int id)
        {
            return Json(await marketerService.Archive(id), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            return Json(await marketerService.Delete(id), JsonRequestBehavior.AllowGet);
        }
    }
}