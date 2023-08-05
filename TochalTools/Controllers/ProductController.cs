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
    [Authorize(Roles = "Developer,SuperAdministrator,Product")]
    public class ProductController : Controller
    {
        ProductService productService = new ProductService();
        [AllowAnonymous]
        public async Task<ActionResult> Index(string mainCategory = null, string subCategory = null, int page = 1)
        {
            string MainCategory = null;
            string SubCategory = null;
            string Search = null;
            string Type = null;
            string Brand = null;
            if (mainCategory != null)
            {
                MainCategory = mainCategory.Replace('-', ' ');
            }
            if (subCategory != null)
            {
                SubCategory = subCategory.Replace('-', ' ');
            }
            if (Request.Params["search"] != null)
            {
                Search = (Request.Params["search"].ToString()).Replace('-', ' ');
            }
            if (Request.Params["type"] != null)
            {
                Type = (Request.Params["type"].ToString()).Replace('-', ' ');
            }
            if (Request.Params["brand"] != null)
            {
                Brand = (Request.Params["brand"]).ToString();
            }
            return View(await productService.ReadAllForIndex(MainCategory, SubCategory, Search, Type, Brand, page, 12));
        }

        public async Task<ActionResult> List()
        {
            return View(await productService.ReadAll());
        }

        [AllowAnonymous]
        public async Task<ActionResult> Details(int id, string title)
        {
            var model = await productService.ReadById(id);
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
            BrandService brandService = new BrandService();
            ProductCreateViewModel viewModel = new ProductCreateViewModel
            {
                FileId = Guid.NewGuid().ToString(),
                CategoriesList = await categoryService.ReadAllByGroupForSelect("Product"),
                TagsList = await tagService.ReadAllByGroupForSelect("Product"),
                BrandsList = await brandService.ReadAllForSelect()
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Title,ShortDescription,FullDescription,MoreDescription,Code,Categories,Tags,BrandId,Language,FileId,QT,Price,Off,OptimizationTitle,OptimizationKeywords,OptimizationDescription,IsPublicationActive,IsAtHomeActive,IsCommentsActive,IsSpecial,IsProposal,IsPercentShow,OffExpireDate,OffExpireTime")] ProductCreateViewModel viewModel, HttpPostedFileBase File)
        {
            if (ModelState.IsValid)
            {
                var productFileId = await productService.Create(viewModel);
                StorageHelper.SaveFile(File, "Images", "Product", productFileId, ".jpg");
            }
            return RedirectToAction("List");
        }

        public async Task<ActionResult> Edit(int id)
        {
            var model = await productService.ReadByIdForEdit(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Title,ShortDescription,FullDescription,MoreDescription,Code,Categories,Tags,BrandId,Language,FileId,QT,Price,Off,OptimizationTitle,OptimizationKeywords,OptimizationDescription,IsPublicationActive,IsAtHomeActive,IsCommentsActive,IsSpecial,IsProposal,IsPercentShow,OffExpireDate,OffExpireTime")] ProductEditViewModel viewModel, HttpPostedFileBase File)
        {
            if (ModelState.IsValid)
            {
                var result = await productService.Edit(viewModel);
                if (result)
                {
                    StorageHelper.SaveFile(File, "Images", "Product", viewModel.FileId, ".jpg");
                }
            }
            return RedirectToAction("List");
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            return Json(await productService.Delete(id), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<JsonResult> Rate(int productId, int rate)
        {
            return Json(await productService.SubmitRate(productId, rate), JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> Settings()
        {
            return View(await productService.ReadSettingsForEdit());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Settings([Bind(Include = "Id,OffProductId,OffStartDate,OffStartTime,OffEndDate,OffEndTime,ProposalStartDate,ProposalStartTime,ProposalEndDate,ProposalEndTime,CheapProductStartPrice,CheapProductEndPrice")] ProductSettingsEditViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await productService.EditSettings(viewModel);
            }
            return RedirectToAction("List");
        }
    }
}