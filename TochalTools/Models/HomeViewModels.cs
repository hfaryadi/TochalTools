using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TochalTools.Models
{
    public class HomeLayoutViewModel
    {
        public InfoModel Info { get; set; }
        public List<CategoryIndexViewModel> ProductCategories { get; set; }
        public List<CategoryIndexViewModel> BlogCategories { get; set; }
        public List<SelectListItem> Pages { get; set; }
        public List<BlogIndexViewModel> LatestBlogs { get; set; }
        public List<ProductIndexViewModel> LatestProducts { get; set; }
        public int CartCount { get; set; }
    }

    public class HomeIndexViewModel
    {
        public List<SliderIndexViewModel> Sliders { get; set; }
        public BoxIndexViewModel OffBox { get; set; }
        public BoxIndexViewModel ProposalImgBox { get; set; }
        public BoxIndexViewModel BuyImgBox { get; set; }
        public BoxIndexViewModel OffImgBox { get; set; }
        public BoxIndexViewModel LatestImgBox { get; set; }
        public BoxIndexViewModel CheapImgBox { get; set; }
        public ProductIndexViewModel FirstDayProduct { get; set; }
        public List<ProductIndexViewModel> CheapProducts { get; set; }
        public List<ProductIndexViewModel> ProposalProducts { get; set; }
        public List<ProductIndexViewModel> OffProducts { get; set; }
        public List<ProductIndexViewModel> BuyProducts { get; set; }
        public List<ProductIndexViewModel> LatestProducts { get; set; }
        public List<BrandIndexViewModel> Brands { get; set; }
        public BoxIndexViewModel MainBox { get; set; }
        public List<BoxIndexViewModel> Boxes { get; set; }
        public List<BlogIndexViewModel> LatestBlogs { get; set; }
    }

    public class HomeContactUsViewModel
    {
        public InfoModel Info { get; set; }
    }

    public class HomeAboutUsViewModel
    {
        public BoxIndexViewModel MainBox { get; set; }
        public List<BoxIndexViewModel> Boxes { get; set; }
    }
}