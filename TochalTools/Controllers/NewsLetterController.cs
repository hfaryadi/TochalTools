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
    [Authorize(Roles = "Developer,SuperAdministrator,Sms")]
    public class NewsLetterController : Controller
    {
        NewsLetterService newsLetterService = new NewsLetterService();
        public async Task<ActionResult> List()
        {
            return View(await newsLetterService.ReadAll());
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Create([Bind(Include = "Mobile")] NewsLetterCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                return Json(await newsLetterService.Create(viewModel), JsonRequestBehavior.AllowGet);
            }
            return Json("لطفا شماره موبایل را به درستی وارد نمایید.", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            return Json(await newsLetterService.Delete(id), JsonRequestBehavior.AllowGet);
        }
    }
}