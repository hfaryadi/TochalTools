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
    public class HomeController : Controller
    {
        HomeService homeService = new HomeService();
        public async Task<ActionResult> Index()
        {
            return View(await homeService.Index());
        }

        public async Task<ActionResult> Cart()
        {
            ProductCartPageViewModel model = new ProductCartPageViewModel { Price = 0, Products = new List<ProductCartViewModel> { } };
            HttpCookie Cookie = Request.Cookies["cart"];
            if (Cookie != null && Cookie.Value != null && Cookie.Value != "")
            {
                ProductService productService = new ProductService();
                model = await productService.ReadAllForCart(Cookie.Value);
                string cookie = "";
                foreach (var item in model.Products)
                {
                    cookie = cookie + item.Id + "-" + item.Count + ",";
                }
                if (cookie != null && cookie != "")
                {
                    cookie = cookie.Remove(cookie.Length - 1, 1);
                }
                Cookie.Value = cookie;
                Response.Cookies.Add(Cookie);
            }
            return View(model);
        }

        public async Task<ActionResult> ContactUs()
        {
            return View(await homeService.ContactUs());
        }

        public async Task<ActionResult> AboutUs()
        {
            return View(await homeService.AboutUs());
        }

        [HttpPost]
        public async Task<ActionResult> UpdateCart()
        {
            ProductCartPageViewModel model = new ProductCartPageViewModel { Price = 0, Products = new List<ProductCartViewModel> { } };
            HttpCookie Cookie = Request.Cookies["cart"];
            if (Cookie != null && Cookie.Value != null && Cookie.Value != "")
            {
                ProductService productService = new ProductService();
                model = await productService.ReadAllForCart(Cookie.Value);
                string cookie = "";
                foreach (var item in model.Products)
                {
                    cookie = cookie + item.Id + "-" + item.Count + ",";
                }
                if (cookie != null && cookie != "")
                {
                    cookie = cookie.Remove(cookie.Length - 1, 1);
                }
                Cookie.Value = cookie;
                Response.Cookies.Add(Cookie);
            }
            return PartialView("/Views/Home/_Cart.cshtml", model);
        }

        [HttpPost]
        public JsonResult UpdateCartCount()
        {
            HttpCookie Cookie = Request.Cookies["cart"];
            if (Cookie != null && Cookie.Value != null && Cookie.Value != "")
            {
                return Json(Cookie.Value.Split(',').Count(), JsonRequestBehavior.AllowGet);
            }
            return Json(0, JsonRequestBehavior.AllowGet);
        }
    }
}
