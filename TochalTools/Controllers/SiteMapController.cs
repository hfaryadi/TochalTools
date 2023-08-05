using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using TochalTools.Models;
using TochalTools.Services;
using Microsoft.AspNet.Identity;

namespace TochalTools.Controllers
{
    [Authorize(Roles = "Developer,SuperAdministrator,Seo")]
    public class SiteMapController : Controller
    {
        SiteMapService sitemapService = new SiteMapService();

        [AllowAnonymous]
        public ActionResult Index()
        {
            return new XmlResult(sitemapService.Index());
        }

        public async Task<ActionResult> List()
        {
            return View(await sitemapService.ReadAll());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Title,Url,ChangeFrequency,Priority")] SiteMapCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await sitemapService.Create(viewModel);
            }
            return RedirectToAction("List");
        }

        public async Task<ActionResult> Edit(int id)
        {
            var model = await sitemapService.ReadByIdForEdit(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Title,Url,ChangeFrequency,Priority")] SiteMapEditViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await sitemapService.Edit(viewModel);
            }
            return RedirectToAction("List");
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            return Json(await sitemapService.Delete(id), JsonRequestBehavior.AllowGet);
        }
    }
}
