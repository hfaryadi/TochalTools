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
    [Authorize(Roles = "Developer,SuperAdministrator")]
    public class InfoController : Controller
    {
        InfoService infoService = new InfoService();

        public async Task<ActionResult> List()
        {
            return View(await infoService.ReadAll());
        }

        public async Task<ActionResult> Create()
        {
            AddressService addressService = new AddressService();
            InfoCreateViewModel viewModel = new InfoCreateViewModel
            {
                CountriesList = await addressService.ReadAllCountriesForSelect(),
                StatesList = new List<SelectListItem>(),
                CitiesList = new List<SelectListItem>(),
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "WebsiteTitle,CompanyName,WorkingHours,Description,FirstTell,SecondTell,FirstMobile,SecondMobile,Fax,Email,WebsiteUrl,CountryId,StateId,CityId,PostalCode,Address,Telegram,Instagram,Twitter,Facebook,GooglePlus,Linkedin,Language,OptimizationTitle,OptimizationDescription,OptimizationKeywords")] InfoCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await infoService.Create(viewModel);
            }
            return RedirectToAction("List");
        }

        public async Task<ActionResult> Edit(int id)
        {
            return View(await infoService.ReadByIdForEdit(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,WebsiteTitle,CompanyName,WorkingHours,Description,FirstTell,SecondTell,FirstMobile,SecondMobile,Fax,Email,WebsiteUrl,CountryId,StateId,CityId,PostalCode,Address,Telegram,Instagram,Twitter,Facebook,GooglePlus,Linkedin,Language,OptimizationTitle,OptimizationDescription,OptimizationKeywords")] InfoEditViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await infoService.Edit(viewModel);
            }
            return RedirectToAction("List");
        }


        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            return Json(await infoService.Delete(id), JsonRequestBehavior.AllowGet);
        }
    }
}