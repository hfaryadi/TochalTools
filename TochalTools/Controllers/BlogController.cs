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
    [Authorize(Roles = "Developer,SuperAdministrator,Blog")]
    public class BlogController : Controller
    {
        BlogService blogService = new BlogService();

        [AllowAnonymous]
        public async Task<ActionResult> Index(string category = null, int page = 1)
        {
            string Category = null;
            string Search = null;
            if (category != null)
            {
                Category = category.Replace('-', ' ');
            }
            if (Request.Params["search"] != null)
            {
                Search = (Request.Params["search"].ToString()).Replace('-', ' ');
            }
            return View(await blogService.ReadAllForIndex(Category, Search, page, 12));
        }

        public async Task<ActionResult> List()
        {
            return View(await blogService.ReadAll());
        }

        [AllowAnonymous]
        public async Task<ActionResult> Details(int id, string title)
        {
            var model = await blogService.ReadById(id, 6, 6);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        public async Task<ActionResult> Create()
        {
            CategoryService categoryService = new CategoryService();
            TagService tagService = new TagService();
            BlogCreateViewModel viewModel = new BlogCreateViewModel
            {
                CategoriesList = await categoryService.ReadAllByGroupForSelect("Blog"),
                TagsList = await tagService.ReadAllByGroupForSelect("Blog")
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Title,ShortContent,FullContent,Categories,Tags,Language,OptimizationTitle,OptimizationKeywords,OptimizationDescription,IsPublicationActive,IsAtHomeActive,IsCommentsActive")] BlogCreateViewModel viewModel, HttpPostedFileBase File)
        {
            if (ModelState.IsValid)
            {
                var blogId = await blogService.Create(viewModel);
                StorageHelper.SaveFile(File, "Images", "Blog", blogId.ToString(), ".jpg");
            }
            return RedirectToAction("List");
        }

        public async Task<ActionResult> Edit(int id)
        {
            var model = await blogService.ReadByIdForEdit(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Title,ShortContent,FullContent,Categories,Tags,Language,OptimizationTitle,OptimizationKeywords,OptimizationDescription,IsPublicationActive,IsAtHomeActive,IsCommentsActive")] BlogEditViewModel viewModel, HttpPostedFileBase File)
        {
            if (ModelState.IsValid)
            {
                var result = await blogService.Edit(viewModel);
                if (result)
                {
                    StorageHelper.SaveFile(File, "Images", "Blog", viewModel.Id.ToString() , ".jpg");
                }
            }
            return RedirectToAction("List");
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            return Json(await blogService.Delete(id), JsonRequestBehavior.AllowGet);
        }
    }
}