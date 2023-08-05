using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TochalTools.Models;

namespace TochalTools.Controllers
{
    [Authorize(Roles = "Developer,SuperAdministrator,Sms")]
    public class SmsController : Controller
    {
        Services.SmsService smsService = new Services.SmsService();
        public async Task<ActionResult> List(string id)
        {
            ViewBag.Id = id;
            return View(await smsService.ReadAll(id));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Content,Mobiles,IsNewsLetters,IsUsers")] SmsCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await smsService.Create(viewModel);
            }
            return RedirectToAction("List");
        }

        [HttpPost]
        public async Task<JsonResult> Archive(int id)
        {
            return Json(await smsService.Archive(id));
        }

        //[HttpPost]
        //public async Task<JsonResult> Delete(int id)
        //{
        //    return Json(await smsService.Delete(id));
        //}
    }
}