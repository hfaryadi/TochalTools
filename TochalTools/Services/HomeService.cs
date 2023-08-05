using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TochalTools.Classes;
using TochalTools.Models;

namespace TochalTools.Services
{
    public interface IHome
    {
        HomeLayoutViewModel Layout();
        Task<HomeIndexViewModel> Index();
        Task<HomeContactUsViewModel> ContactUs();
        Task<HomeAboutUsViewModel> AboutUs();
    }
    public class HomeService : IHome
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public HomeLayoutViewModel Layout()
        {
            BrandService brandService = new BrandService();
            HttpCookie Cookie = HttpContext.Current.Request.Cookies["cart"];
            var cartCount = 0;
            var Categories = db.Categories.Where(x => !x.IsDeleted && x.IsPublicationActive && x.Language == "fa").OrderBy(x => x.Name).ToList();
            var comments = db.Comments.Where(x => !x.IsDeleted && x.IsConfirmed && x.Group == "Blog").ToList();
            var BlogCategories = Categories.Where(x => x.Group == "Blog").Select(x => new CategoryIndexViewModel
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
            var ProductCategories = Categories.Where(x => x.Group == "Product" && x.Parents == null || x.Parents == "").Select(x => new CategoryIndexViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Childs = Categories.Where(z => z.Group == "Product" && z.Parents != null && z.Parents.Contains(x.Id)).Select(z => new CategoryIndexViewModel
                {
                    Id = z.Id,
                    Name = z.Name
                }).ToList()
            }).ToList();
            var Pages = db.Pages.Where(x => !x.IsDeleted && x.Language == "fa").Select(x => new SelectListItem
            {
                Value = x.Title,
                Text = x.Title
            }).ToList();
            var blogs = db.Blogs.Where(x => !x.IsDeleted && x.IsPublicationActive && x.IsAtHomeActive && x.Language == "fa").OrderByDescending(x => x.Id).Take(3).ToList();
            var LatestBlogs = blogs.Select(x => new BlogIndexViewModel
            {
                Id = x.Id,
                CommentCount = comments.Where(z => z.RefId == x.Id.ToString()).Count(),
                Review = CountHelper.CalculateCount(x.Review),
                Title = x.Title,
            }).ToList();
            var LatestProducts = db.Products.Where(x => !x.IsDeleted && x.IsPublicationActive && x.IsAtHomeActive && x.Language == "fa").OrderByDescending(x => x.Id).Take(6).Select(x => new ProductIndexViewModel
            {
                Id = x.Id,
                Title = x.Title,
            }).ToList();
            var info = db.Infos.Where(x => !x.IsDeleted && x.Language == "fa").FirstOrDefault();
            if (Cookie != null && Cookie.Value != null && Cookie.Value != "")
            {
                cartCount = Cookie.Value.Split(',').Count();
            }
            return new HomeLayoutViewModel
            {
                ProductCategories = ProductCategories,
                BlogCategories = BlogCategories,
                Pages = Pages,
                LatestBlogs = LatestBlogs,
                LatestProducts = LatestProducts,
                CartCount = cartCount,
                Info = (info != null) ? info : new InfoModel { }
            };
        }

        public async Task<HomeIndexViewModel> Index()
        {
            var now = DateTime.Now;
            SliderService sliderService = new SliderService();
            BoxService boxService = new BoxService();
            TagService tagService = new TagService();
            BrandService brandService = new BrandService();
            var productSettings = await db.ProductSettings.FirstOrDefaultAsync();
            var comments = await db.Comments.Where(x => !x.IsDeleted && x.IsConfirmed && x.Group == "Blog").ToListAsync();
            var products = await db.Products.Where(x => !x.IsDeleted && x.IsPublicationActive && x.IsAtHomeActive && x.Language == "fa").ToListAsync();
            var blogs = await db.Blogs.Where(x => !x.IsDeleted && x.IsPublicationActive && x.IsAtHomeActive && x.Language == "fa").OrderByDescending(x => x.Id).Take(6).ToListAsync();
            ProductIndexViewModel FirstDayProduct = null;
            List<ProductIndexViewModel> CheapProducts = new List<ProductIndexViewModel> { };
            if (productSettings != null)
            {
                if (productSettings.OffProductId > 0 && productSettings.OffStartDate <= now && productSettings.OffEndDate >= now)
                {
                    FirstDayProduct = products.Where(x => x.Id == productSettings.OffProductId).Select(x => new ProductIndexViewModel
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
                    }).FirstOrDefault();
                }
                if (productSettings.CheapProductEndPrice > 0)
                {
                    CheapProducts = products.Where(x => ((x.Off > 0 && (x.OffExpireDate == null || (x.OffExpireDate >= now))) ? (x.Price - x.Off) : x.Price) >= productSettings.CheapProductStartPrice && ((x.Off > 0 && (x.OffExpireDate == null || (x.OffExpireDate >= now))) ? (x.Price - x.Off) : x.Price) <= productSettings.CheapProductEndPrice).OrderByDescending(x => x.Id).Take(12).Select(x => new ProductIndexViewModel
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
                }
            }
            var ProposalProducts = products.Where(x => x.IsProposal).OrderBy(x => x.Id).Take(12).Select(x => new ProductIndexViewModel
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
            var OffProducts = products.Where(x => x.IsSpecial && x.Off > 0 && (x.OffExpireDate == null || (x.OffExpireDate >= now))).OrderByDescending(x => x.Off).ThenByDescending(x => x.Id).Take(12).Select(x => new ProductIndexViewModel
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
            var BuyProducts = products.OrderByDescending(x => x.BuyCount).ThenBy(x => new Guid()).Take(12).Select(x => new ProductIndexViewModel
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
            var LatestProducts = products.OrderByDescending(x => x.Id).Take(12).Select(x => new ProductIndexViewModel
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
            var LatestBlogs = blogs.Select(x => new BlogIndexViewModel
            {
                Id = x.Id,
                Date = DateTimeHelper.ConvertToShamsi(x.Date),
                CommentCount = comments.Where(z => z.RefId == x.Id.ToString()).Count(),
                Review = CountHelper.CalculateCount(x.Review),
                ShortContent = x.ShortContent,
                Title = x.Title,
                Tags = tagService.ReadAllTagsNameByIds(x.Tags)
            }).ToList();
            var Sliders = await sliderService.ReadAllForIndex();
            var OffBox = await boxService.ReadByAddress("Index_OffBox");
            var MainBox = await boxService.ReadByAddress("Index_MainBox");
            var Boxes = await boxService.ReadAllByAddress("Index_Boxes");
            var ProposalImgBox = await boxService.ReadByAddress("Index_ProposalImgBox");
            var BuyImgBox = await boxService.ReadByAddress("Index_BuyImgBox");
            var OffImgBox = await boxService.ReadByAddress("Index_OffImgBox");
            var LatestImgBox = await boxService.ReadByAddress("Index_LatestImgBox");
            var CheapImgBox = await boxService.ReadByAddress("Index_CheapImgBox");
            var Brands = await brandService.ReadAllForIndex(true);
            return new HomeIndexViewModel
            {
                Boxes = Boxes,
                Brands = Brands,
                BuyProducts = BuyProducts,
                CheapProducts = CheapProducts,
                FirstDayProduct = FirstDayProduct,
                LatestBlogs = LatestBlogs,
                LatestProducts = LatestProducts,
                MainBox = MainBox,
                OffBox = OffBox,
                OffProducts = OffProducts,
                ProposalProducts = ProposalProducts,
                Sliders = Sliders,
                BuyImgBox = BuyImgBox,
                CheapImgBox = CheapImgBox,
                LatestImgBox = LatestImgBox,
                OffImgBox = OffImgBox,
                ProposalImgBox = ProposalImgBox
            };
        }

        public async Task<HomeContactUsViewModel> ContactUs()
        {
            var info = await db.Infos.Where(x => !x.IsDeleted && x.Language == "fa").FirstOrDefaultAsync();
            return new HomeContactUsViewModel
            {
                Info = (info != null) ? info : new InfoModel { }
            };
        }

        public async Task<HomeAboutUsViewModel> AboutUs()
        {
            BoxService boxService = new BoxService();
            var MainBox = await boxService.ReadByAddress("AboutUs_MainBox");
            var Boxes = await boxService.ReadAllByAddress("AboutUs_Boxes");
            return new HomeAboutUsViewModel
            {
                Boxes = Boxes,
                MainBox = MainBox
            };
        }
    }
}