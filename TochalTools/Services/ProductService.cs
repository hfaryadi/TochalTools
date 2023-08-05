using Microsoft.AspNet.Identity;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using TochalTools.Classes;
using TochalTools.Models;

namespace TochalTools.Services
{
    public interface IProduct
    {
        Task<ProductIndexPageViewModel> ReadAllForIndex(string mainCategory, string subCategory, string search, string type, string brand, int page, int pageSize);
        Task<List<ProductListViewModel>> ReadAll();
        Task<ProductDetailsViewModel> ReadById(int productId);
        Task<string> Create(ProductCreateViewModel viewModel);
        Task<ProductEditViewModel> ReadByIdForEdit(int productId);
        Task<bool> Edit(ProductEditViewModel viewModel);
        Task<bool> Delete(int productId);
        Task<bool> SubmitRate(int productId, int rate);
        Task<ProductCartPageViewModel> ReadAllForCart(string cookie);
        Task<List<SelectListItem>> ReadAllForSelect();
        Task<ProductSettingsEditViewModel> CreateSettings();
        Task<ProductSettingsEditViewModel> ReadSettingsForEdit();
        Task EditSettings(ProductSettingsEditViewModel viewModel);
    }
    public class ProductService : IProduct
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public async Task<ProductIndexPageViewModel> ReadAllForIndex(string mainCategory, string subCategory, string search, string type, string brand, int page, int pageSize)
        {
            var now = DateTime.Now;
            var Comments = await db.Comments.Where(x => !x.IsDeleted && x.IsConfirmed && x.Group == "Product").ToListAsync();
            var Profiles = await db.Profiles.ToListAsync();
            var model = await db.Products.Where(x => !x.IsDeleted && x.IsPublicationActive && x.Language == "fa" && (search == null || x.Title.Contains(search) || (x.Code != null && x.Code == search))).OrderByDescending(x => x.Id).ToListAsync();
            var Category = new SelectListItem { Value = "Default", Text = "جدیدترین محصولات" };
            var category = "";
            if (mainCategory != null && mainCategory != "")
            {
                category = mainCategory;
            }
            if (subCategory != null && subCategory != "")
            {
                if (category != "")
                {
                    category = category + "," + subCategory;
                }
                else
                {
                    category = subCategory;
                }
            }
            if (category != "")
            {
                CategoryService categoryService = new CategoryService();
                var categoryIds = await categoryService.ReadCategoriesIdByName("Product", category);
                if (categoryIds != null && categoryIds != "")
                {
                    var categoryIdsList = categoryIds.Split(',');
                    foreach (var item in categoryIdsList)
                    {
                        model = model.Where(x => x.Categories != null && x.Categories.Contains(item)).ToList();
                    }
                }
                else
                {
                    model = new List<ProductModel>();
                }
                if (subCategory != null && subCategory != "")
                {
                    Category.Value = await categoryService.ReadIdByName("Product", subCategory);
                    Category.Text = subCategory;
                }
                else if (mainCategory != null && mainCategory != "")
                {
                    Category.Value = await categoryService.ReadIdByName("Product", mainCategory);
                    Category.Text = mainCategory;
                }
            }
            else if (search != null)
            {
                Category.Text = search;
            }
            else if (type != null && type != "")
            {
                switch (type.ToLower())
                {
                    case "cheap":
                        var productSettings = await db.ProductSettings.FirstOrDefaultAsync();
                        if (productSettings != null && productSettings.CheapProductEndPrice > 0)
                        {
                            model = model.Where(x => ((x.Off > 0 && (x.OffExpireDate == null || (x.OffExpireDate >= now))) ? (x.Price - x.Off) : x.Price) >= productSettings.CheapProductStartPrice && ((x.Off > 0 && (x.OffExpireDate == null || (x.OffExpireDate >= now))) ? (x.Price - x.Off) : x.Price) <= productSettings.CheapProductEndPrice).ToList();
                        }
                        else
                        {
                            model = new List<ProductModel>();
                        }
                        Category.Text = "سبدت خریدت رو پر کن";
                        break;
                    case "proposal":
                        model = model.Where(x => x.IsProposal).ToList();
                        Category.Text = "پیشنهادهای شگفت انگیز";
                        break;
                    case "off":
                        model = model.Where(x => x.QT > 0 && x.Off > 0 && (x.OffExpireDate == null || (x.OffExpireDate >= now))).OrderByDescending(x => x.Off).ThenByDescending(x => x.Id).ToList();
                        Category.Text = "تخفیف های ویژه";
                        break;
                    case "buy":
                        model = model.Where(x => x.BuyCount > 0).OrderByDescending(x => x.BuyCount).ThenByDescending(x => x.Id).ToList();
                        Category.Text = "پرفروش ترین محصولات";
                        break;
                }
            }
            else if (brand != null && brand != "")
            {
                BrandService brandService = new BrandService();
                model = model.Where(x => x.BrandId > 0 && x.BrandId.ToString() == brand).ToList();
                Category.Text = await brandService.ReadTitleById(Convert.ToInt32(brand));
            }
            var PageCount = Convert.ToDecimal(model.Count()) / Convert.ToDecimal(pageSize);
            var pageCount = Convert.ToInt32(Math.Ceiling(PageCount));
            var products = model.Select(x => new ProductIndexViewModel
            {
                Id = x.Id,
                FileId = x.FileId,
                IsAvailable = (x.QT > 0) ? true : false,
                IsPercentShow = x.IsPercentShow,
                IsSpecial = x.IsSpecial,
                Off = x.Off,
                OffExpireDate = x.OffExpireDate,
                Percent = CountHelper.GetPercent(x.Price, x.Off),
                Price = x.Price,
                Rate = (x.RateCount > 0) ? (x.Rate / x.RateCount) : 0,
                Title = x.Title,
                TotalPrice = (x.Off > 0 && (x.OffExpireDate == null || (x.OffExpireDate >= now))) ? (x.Price - x.Off) : x.Price
            }).ToPagedList(page, pageSize);
            return new ProductIndexPageViewModel
            {
                Products = products,
                MainCategory = (mainCategory != null) ? mainCategory : "",
                SubCategory = (subCategory != null) ? subCategory : "",
                Category = Category,
                Page = page,
                PageCount = pageCount,
                Search = (search != null) ? search : "",
                Type = (type != null) ? type : "",
                Brand = (brand != null) ? brand : ""
            };
        }

        public async Task<List<ProductListViewModel>> ReadAll()
        {
            BaseService baseService = new BaseService();
            var userDetails = baseService.ReadUserDetails();
            var model = await db.Products.Where(x => !x.IsDeleted && (userDetails.Role == "Administrator" || userDetails.UserId == x.UserId)).OrderByDescending(x => x.Id).ToListAsync();
            var Profiles = await db.Profiles.ToListAsync();
            return model.Select(x => new ProductListViewModel
            {
                Id = x.Id,
                IsAtHomeActive = (x.IsAtHomeActive == true) ? "checked" : "",
                IsPublicationActive = (x.IsPublicationActive == true) ? "checked" : "",
                Language = LanguageHelper.ReadLanguageByValue(x.Language),
                TotalPrice = (x.Off > 0 && (x.OffExpireDate == null || (x.OffExpireDate.Value.Date >= DateTime.Today.Date))) ? (x.Price - x.Off).ToString("N0") : x.Price.ToString("N0"),
                QT = x.QT.ToString(),
                Title = x.Title,
                Update = DateTimeHelper.ConvertToShamsi(x.Update),
                UserId = x.UserId,
                UserName = Profiles.Where(z => z.Id == x.UserId).Select(z => z.Name).FirstOrDefault()
            }).ToList();
        }

        public async Task<ProductDetailsViewModel> ReadById(int productId)
        {
            var now = DateTime.Now;
            var products = await db.Products.Where(x => !x.IsDeleted && x.IsPublicationActive && x.Language == "fa").ToListAsync();
            var model = products.Where(x => x.Id == productId).FirstOrDefault();
            if (model == null)
            {
                return null;
            }
            TagService tagService = new TagService();
            CommentService commentService = new CommentService();
            BrandService brandService = new BrandService();
            var tags = await tagService.ReadAllTagsNameByIdsAsync(model.Tags);
            var images = new List<string>();
            try
            {
                images = Directory.GetFiles(HostingEnvironment.MapPath("/Content/Images/Product/"), model.FileId + "*.jpg", SearchOption.TopDirectoryOnly).Select(Path.GetFileName).ToList();
            }
            catch { }
            var product = new ProductDetailsViewModel
            {
                Id = model.Id,
                BrandName = await brandService.ReadTitleById(model.BrandId),
                Code = model.Code,
                FileId = model.FileId,
                FullDescription = model.FullDescription,
                IsAvailable = (model.QT > 0) ? true : false,
                IsCommentsActive = model.IsCommentsActive,
                IsSpecial = model.IsSpecial,
                MoreDescription = model.MoreDescription,
                Off = model.Off,
                OffExpireDate = model.OffExpireDate,
                OptimizationDescription = model.OptimizationDescription,
                OptimizationKeywords = model.OptimizationKeywords,
                OptimizationTitle = model.OptimizationTitle,
                Price = model.Price,
                Rate = (model.RateCount > 0) ? (model.Rate / model.RateCount) : 0,
                ShortDescription = model.ShortDescription,
                Tags = tags,
                Title = model.Title,
                TotalPrice = (model.Off > 0 && (model.OffExpireDate == null || (model.OffExpireDate >= DateTime.Now))) ? (model.Price - model.Off) : model.Price,
                Images = images
            };
            product.SimilarProducts = products.Where(x => x.Categories != null && x.Categories.Contains(model.Categories) && x.Id != model.Id).OrderByDescending(x => x.Id).Take(12).Select(x => new ProductIndexViewModel
            {
                Id = x.Id,
                FileId = x.FileId,
                IsAvailable = (x.QT > 0) ? true : false,
                IsPercentShow = x.IsPercentShow,
                IsSpecial = x.IsSpecial,
                Off = x.Off,
                OffExpireDate = x.OffExpireDate,
                Percent = CountHelper.GetPercent(x.Price, x.Off),
                Price = x.Price,
                Rate = (x.RateCount > 0) ? (x.Rate / x.RateCount) : 0,
                Title = x.Title,
                TotalPrice = (x.Off > 0 && (x.OffExpireDate == null || (x.OffExpireDate >= now))) ? (x.Price - x.Off) : x.Price
            }).ToList();
            product.Comments = await commentService.ReadAllByGroupAndRefId("Product", productId.ToString());
            return product;
        }

        public async Task<string> Create(ProductCreateViewModel viewModel)
        {
            var now = DateTime.Now;
            DateTime? offExpireDate = null;
            ProductModel model = new ProductModel
            {
                BuyCount = 0,
                BrandId = viewModel.BrandId ?? 0,
                Categories = viewModel.Categories,
                Code = viewModel.Code,
                Date = now,
                FileId = viewModel.FileId,
                FullDescription = viewModel.FullDescription,
                IsAtHomeActive = viewModel.IsAtHomeActive,
                IsCommentsActive = viewModel.IsCommentsActive,
                IsDeleted = false,
                IsPercentShow = viewModel.IsPercentShow,
                IsPublicationActive = viewModel.IsPublicationActive,
                IsSpecial = viewModel.IsSpecial,
                IsProposal = viewModel.IsProposal,
                Language = viewModel.Language,
                MoreDescription = viewModel.MoreDescription,
                Off = viewModel.Off,
                OffExpireDate = (viewModel.OffExpireDate != null && viewModel.OffExpireDate != "") ? DateTimeHelper.ConvertToDateTime(viewModel.OffExpireDate, viewModel.OffExpireTime) : offExpireDate,
                OptimizationDescription = viewModel.OptimizationDescription,
                OptimizationKeywords = viewModel.OptimizationKeywords,
                OptimizationTitle = viewModel.OptimizationTitle,
                Price = viewModel.Price,
                QT = viewModel.QT,
                Rate = 0,
                RateCount = 0,
                ShortDescription = viewModel.ShortDescription,
                Tags = viewModel.Tags,
                Title = viewModel.Title,
                Update = now,
                UserId = HttpContext.Current.User.Identity.GetUserId()
            };
            db.Products.Add(model);
            await db.SaveChangesAsync();
            return model.FileId;
        }

        public async Task<ProductEditViewModel> ReadByIdForEdit(int productId)
        {
            BaseService baseService = new BaseService();
            var userDetails = baseService.ReadUserDetails();
            var model = await db.Products.FindAsync(productId);
            if (model == null || (userDetails.Role != "Administrator" && userDetails.UserId != model.UserId))
            {
                return null;
            }
            CategoryService categoryService = new CategoryService();
            TagService tagService = new TagService();
            BrandService brandService = new BrandService();
            var images = new List<SelectListItem>();
            try
            {
                var tempDir = new DirectoryInfo(HostingEnvironment.MapPath("/Content/Images/Product/"));
                images = tempDir.EnumerateFiles(model.FileId + "_*.jpg", SearchOption.TopDirectoryOnly).Select(x => new SelectListItem
                {
                    Value = x.Length.ToString(),
                    Text = Path.GetFileNameWithoutExtension(x.FullName)
                }).ToList();
            }
            catch { }
            return new ProductEditViewModel
            {
                Id = model.Id,
                BrandId = model.BrandId,
                Categories = model.Categories,
                Code = model.Code,
                FileId = model.FileId,
                FullDescription = model.FullDescription,
                IsAtHomeActive = model.IsAtHomeActive,
                IsCommentsActive = model.IsCommentsActive,
                IsPercentShow = model.IsPercentShow,
                IsPublicationActive = model.IsPublicationActive,
                IsSpecial = model.IsSpecial,
                IsProposal = model.IsProposal,
                Language = model.Language,
                MoreDescription = model.MoreDescription,
                Off = model.Off,
                OffExpireDate = (model.OffExpireDate != null) ? DateTimeHelper.ConvertToShamsi(model.OffExpireDate ?? DateTime.Now) : "",
                OffExpireTime = (model.OffExpireDate != null) ? DateTimeHelper.ConvertToShamsi(model.OffExpireDate ?? DateTime.Now, true).Split('-')[1].Trim() : "",
                OptimizationDescription = model.OptimizationDescription,
                OptimizationKeywords = model.OptimizationKeywords,
                OptimizationTitle = model.OptimizationTitle,
                Price = model.Price,
                QT = model.QT,
                ShortDescription = model.ShortDescription,
                Tags = model.Tags,
                Title = model.Title,
                ImagesList = images,
                CategoriesList = await categoryService.ReadAllByGroupForSelect("Product"),
                TagsList = await tagService.ReadAllByGroupForSelect("Product"),
                BrandsList = await brandService.ReadAllForSelect()
            };
        }
        public async Task<bool> Edit(ProductEditViewModel viewModel)
        {
            DateTime? offExpireDate = null;
            BaseService baseService = new BaseService();
            var userDetails = baseService.ReadUserDetails();
            var model = await db.Products.FindAsync(viewModel.Id);
            if (model == null || (userDetails.Role != "Administrator" && userDetails.UserId != model.UserId))
            {
                return false;
            }
            model.BrandId = viewModel.BrandId ?? 0;
            model.Categories = viewModel.Categories;
            model.Code = viewModel.Code;
            model.FullDescription = viewModel.FullDescription;
            model.IsAtHomeActive = viewModel.IsAtHomeActive;
            model.IsCommentsActive = viewModel.IsCommentsActive;
            model.IsPercentShow = viewModel.IsPercentShow;
            model.IsPublicationActive = viewModel.IsPublicationActive;
            model.IsSpecial = viewModel.IsSpecial;
            model.IsProposal = viewModel.IsProposal;
            model.Language = viewModel.Language;
            model.MoreDescription = viewModel.MoreDescription;
            model.Off = viewModel.Off;
            model.OffExpireDate = (viewModel.OffExpireDate != null && viewModel.OffExpireDate != "") ? DateTimeHelper.ConvertToDateTime(viewModel.OffExpireDate, viewModel.OffExpireTime) : offExpireDate;
            model.OptimizationDescription = viewModel.OptimizationDescription;
            model.OptimizationKeywords = viewModel.OptimizationKeywords;
            model.OptimizationTitle = viewModel.OptimizationTitle;
            model.Price = viewModel.Price;
            model.QT = viewModel.QT;
            model.ShortDescription = viewModel.ShortDescription;
            model.Tags = viewModel.Tags;
            model.Title = viewModel.Title;
            model.Update = DateTime.Now;
            db.Entry(model).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int productId)
        {
            BaseService baseService = new BaseService();
            var userDetails = baseService.ReadUserDetails();
            var model = await db.Products.FindAsync(productId);
            if (model == null || (userDetails.Role != "Administrator" && userDetails.UserId != model.UserId))
            {
                return false;
            }
            model.IsDeleted = true;
            model.Update = DateTime.Now;
            db.Entry(model).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SubmitRate(int productId, int rate)
        {
            var model = await db.Products.FindAsync(productId);
            if (model == null)
            {
                return false;
            }
            model.Rate = model.Rate + rate;
            model.RateCount++;
            db.Entry(model).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<ProductCartPageViewModel> ReadAllForCart(string cookie)
        {
            if (cookie == null || cookie == "")
            {
                return new ProductCartPageViewModel { Price = 0, Products = new List<ProductCartViewModel> { } };
            }
            var now = DateTime.Now;
            List<ProductCartViewModel> list = new List<ProductCartViewModel>();
            Int64 Price = 0;
            var Products = await db.Products.Where(x => !x.IsDeleted && x.IsPublicationActive && x.QT > 0).ToListAsync();
            var products = cookie.Split(',');
            foreach (var item in products)
            {
                var prds = item.Split('-');
                var productId = Convert.ToInt32(prds[0]);
                var count = Convert.ToInt32(prds[1]);
                var product = Products.Where(x => x.Id == productId).FirstOrDefault();
                if (product != null && count > 0)
                {
                    var Product = new ProductCartViewModel
                    {
                        Id = product.Id,
                        Count = count,
                        FileId = product.FileId,
                        FinalPrice = ((product.Off > 0 && (product.OffExpireDate == null || (product.OffExpireDate >= now))) ? (product.Price - product.Off) : product.Price) * count,
                        IsAvailable = (product.QT > 0) ? true : false,
                        Title = product.Title,
                        TotalPrice = (product.Off > 0 && (product.OffExpireDate == null || (product.OffExpireDate >= now))) ? (product.Price - product.Off) : product.Price
                    };
                    list.Add(Product);
                    Price = Price + Product.FinalPrice;
                }
            }
            return new ProductCartPageViewModel
            {
                Products = list,
                Price = Price
            };
        }

        public async Task<List<SelectListItem>> ReadAllForSelect()
        {
            return await db.Products.Where(x => !x.IsDeleted).OrderBy(x => x.Title).Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Title
            }).ToListAsync();
        }

        public async Task<ProductSettingsEditViewModel> CreateSettings()
        {
            var model = new ProductSettingsModel { };
            db.ProductSettings.Add(model);
            await db.SaveChangesAsync();
            return new ProductSettingsEditViewModel
            {
                Id = model.Id,
                CheapProductEndPrice = model.CheapProductEndPrice,
                CheapProductStartPrice = model.CheapProductStartPrice,
                OffEndDate = (model.OffEndDate != null) ? DateTimeHelper.ConvertToShamsi(model.OffEndDate ?? DateTime.Now) : "",
                OffEndTime = (model.OffEndDate != null) ? DateTimeHelper.ConvertToShamsi(model.OffEndDate ?? DateTime.Now, true).Split('-')[1].Trim() : "",
                ProposalEndDate = (model.ProposalEndDate != null) ? DateTimeHelper.ConvertToShamsi(model.ProposalEndDate ?? DateTime.Now) : "",
                ProposalEndTime = (model.ProposalEndDate != null) ? DateTimeHelper.ConvertToShamsi(model.ProposalEndDate ?? DateTime.Now, true).Split('-')[1].Trim() : "",
                OffProductId = model.OffProductId,
                OffStartDate = (model.OffStartDate != null) ? DateTimeHelper.ConvertToShamsi(model.OffStartDate ?? DateTime.Now) : "",
                OffStartTime = (model.OffStartDate != null) ? DateTimeHelper.ConvertToShamsi(model.OffStartDate ?? DateTime.Now, true).Split('-')[1].Trim() : "",
                ProposalStartDate = (model.ProposalStartDate != null) ? DateTimeHelper.ConvertToShamsi(model.ProposalStartDate ?? DateTime.Now) : "",
                ProposalStartTime = (model.ProposalStartDate != null) ? DateTimeHelper.ConvertToShamsi(model.ProposalStartDate ?? DateTime.Now, true).Split('-')[1].Trim() : "",
                ProductsList = await ReadAllForSelect()
            };
        }

        public async Task<ProductSettingsEditViewModel> ReadSettingsForEdit()
        {
            var model = await db.ProductSettings.FirstOrDefaultAsync();
            if (model == null)
            {
                return await CreateSettings();
            }
            return new ProductSettingsEditViewModel
            {
                Id = model.Id,
                CheapProductEndPrice = model.CheapProductEndPrice,
                CheapProductStartPrice = model.CheapProductStartPrice,
                OffEndDate = (model.OffEndDate != null) ? DateTimeHelper.ConvertToShamsi(model.OffEndDate ?? DateTime.Now) : "",
                OffEndTime = (model.OffEndDate != null) ? DateTimeHelper.ConvertToShamsi(model.OffEndDate ?? DateTime.Now, true).Split('-')[1].Trim() : "",
                ProposalEndDate = (model.ProposalEndDate != null) ? DateTimeHelper.ConvertToShamsi(model.ProposalEndDate ?? DateTime.Now) : "",
                ProposalEndTime = (model.ProposalEndDate != null) ? DateTimeHelper.ConvertToShamsi(model.ProposalEndDate ?? DateTime.Now, true).Split('-')[1].Trim() : "",
                OffProductId = model.OffProductId,
                OffStartDate = (model.OffStartDate != null) ? DateTimeHelper.ConvertToShamsi(model.OffStartDate ?? DateTime.Now) : "",
                OffStartTime = (model.OffStartDate != null) ? DateTimeHelper.ConvertToShamsi(model.OffStartDate ?? DateTime.Now, true).Split('-')[1].Trim() : "",
                ProposalStartDate = (model.ProposalStartDate != null) ? DateTimeHelper.ConvertToShamsi(model.ProposalStartDate ?? DateTime.Now) : "",
                ProposalStartTime = (model.ProposalStartDate != null) ? DateTimeHelper.ConvertToShamsi(model.ProposalStartDate ?? DateTime.Now, true).Split('-')[1].Trim() : "",
                ProductsList = await ReadAllForSelect()
            };
        }

        public async Task EditSettings(ProductSettingsEditViewModel viewModel)
        {
            DateTime? nullDate = null;
            var model = await db.ProductSettings.FindAsync(viewModel.Id);
            model.CheapProductEndPrice = viewModel.CheapProductEndPrice;
            model.CheapProductStartPrice = viewModel.CheapProductStartPrice;
            model.OffEndDate = (viewModel.OffEndDate != null && viewModel.OffEndDate != "") ? DateTimeHelper.ConvertToDateTime(viewModel.OffEndDate, viewModel.OffEndTime) : nullDate;
            model.ProposalEndDate = (viewModel.ProposalEndDate != null && viewModel.ProposalEndDate != "") ? DateTimeHelper.ConvertToDateTime(viewModel.ProposalEndDate, viewModel.ProposalEndTime) : nullDate;
            model.OffProductId = viewModel.OffProductId ?? 0;
            model.OffStartDate = (viewModel.OffStartDate != null && viewModel.OffStartDate != "") ? DateTimeHelper.ConvertToDateTime(viewModel.OffStartDate, viewModel.OffStartTime) : nullDate;
            model.ProposalStartDate = (viewModel.ProposalStartDate != null && viewModel.ProposalStartDate != "") ? DateTimeHelper.ConvertToDateTime(viewModel.ProposalStartDate, viewModel.ProposalStartTime) : nullDate;
            db.Entry(model).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }
    }
}