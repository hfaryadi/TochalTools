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
    public class AddressController : Controller
    {
        AddressService addressService = new AddressService();

        public async Task<ActionResult> CountryList()
        {
            return View(await addressService.ReadAllCountries());
        }

        public ActionResult CreateCountry()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateCountry([Bind(Include = "Name")] CountryCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await addressService.CreateCountry(viewModel);
            }
            return RedirectToAction("CountryList");
        }

        public async Task<ActionResult> EditCountry(int id)
        {
            return View(await addressService.ReadCountryByIdForEdit(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditCountry([Bind(Include = "Id,Name")] CountryEditViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await addressService.EditCountry(viewModel);
            }
            return RedirectToAction("CountryList");
        }

        [HttpPost]
        public async Task<JsonResult> DeleteCountry(int id)
        {
            return Json(await addressService.DeleteCountry(id), JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> StateList(int id)
        {
            ViewBag.CountryId = id;
            return View(await addressService.ReadAllStatesByCountryId(id));
        }

        public ActionResult CreateState(int id)
        {
            ViewBag.CountryId = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateState([Bind(Include = "Name,CountryId")] StateCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await addressService.CreateState(viewModel);
            }
            return RedirectToAction("StateList", new { id = viewModel.CountryId});
        }

        public async Task<ActionResult> EditState(int id)
        {
            return View(await addressService.ReadStateByIdForEdit(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditState([Bind(Include = "Id,Name,CountryId")] StateEditViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await addressService.EditState(viewModel);
            }
            return RedirectToAction("StateList", new { id = viewModel.CountryId });
        }

        [HttpPost]
        public async Task<JsonResult> DeleteState(int id)
        {
            return Json(await addressService.DeleteState(id), JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> CityList(int id, int countryId)
        {
            ViewBag.StateId = id;
            ViewBag.CountryId = countryId;
            return View(await addressService.ReadAllCitiesByStateId(id));
        }

        public ActionResult CreateCity(int id, int countryId)
        {
            ViewBag.StateId = id;
            ViewBag.CountryId = countryId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateCity([Bind(Include = "Name,StateId")] CityCreateViewModel viewModel, int countryId)
        {
            if (ModelState.IsValid)
            {
                await addressService.CreateCity(viewModel);
            }
            return RedirectToAction("CityList", new { id = viewModel.StateId, countryId = countryId });
        }

        public async Task<ActionResult> EditCity(int id, int countryId)
        {
            ViewBag.CountryId = countryId;
            return View(await addressService.ReadCityByIdForEdit(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditCity([Bind(Include = "Id,Name,StateId")] CityEditViewModel viewModel, int countryId)
        {
            if (ModelState.IsValid)
            {
                await addressService.EditCity(viewModel);
            }
            return RedirectToAction("CityList", new { id = viewModel.StateId, countryId = countryId });
        }

        [HttpPost]
        public async Task<JsonResult> DeleteCity(int id)
        {
            return Json(await addressService.DeleteCity(id), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<JsonResult> ReadAllCountriesForSelect()
        {
            return Json(await addressService.ReadAllCountriesForSelect(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<JsonResult> ReadAllStatesByCountryIdForSelect(int id)
        {
            return Json(await addressService.ReadAllStatesByCountryIdForSelect(id), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<JsonResult> ReadAllCitiesByStateIdForSelect(int id)
        {
            return Json(await addressService.ReadAllCitiesByStateIdForSelect(id), JsonRequestBehavior.AllowGet);
        }
    }
}