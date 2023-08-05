using Microsoft.AspNet.Identity;
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
    [Authorize]
    public class ProfileController : Controller
    {
        ProfileService profileService = new ProfileService();

        public async Task<ActionResult> Edit()
        {
            var userId = User.Identity.GetUserId();
            return View(await profileService.ReadByIdForEdit(userId));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Name,Tell,Mobile,Fax,CountryId,StateId,CityId,PostalCode,Address,Email,Website,Description,Telegram,Instagram,Twitter,Facebook,GooglePlus,Linkedin")] ProfileEditViewModel viewModel, HttpPostedFileBase File)
        {
            if (ModelState.IsValid)
            {
                viewModel.Id = User.Identity.GetUserId();
                await profileService.Edit(viewModel);
                StorageHelper.SaveFile(File, "Images", "Profile", viewModel.Id, ".jpg");
            }
            return RedirectToAction("Admin", "Dashboard");
        }

        [HttpPost]
        public async Task<JsonResult> ReadAllByRoleForSelect(string id)
        {
            return Json(await profileService.ReadAllByRoleForSelect(id), JsonRequestBehavior.AllowGet);
        }
    }
}