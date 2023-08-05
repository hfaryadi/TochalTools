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
    [Authorize(Roles = "Developer,SuperAdministrator,Administrator,Order,User")]
    public class OrderController : Controller
    {
        OrderService orderService = new OrderService();

        public async Task<ActionResult> List(string id)
        {
            ViewBag.Id = id;
            return View(await orderService.ReadAll(id));
        }

        public async Task<ActionResult> Details(int id)
        {
            var model = await orderService.ReadById(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        [AllowAnonymous]
        public async Task<ActionResult> Print(int id)
        {
            var model = await orderService.ReadById(id, true);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        [AllowAnonymous]
        public async Task<ActionResult> Create(string id = null)
        {
            AddressService addressService = new AddressService();
            OrderCreateViewModel viewModel = new OrderCreateViewModel
            {
                CountriesList = await addressService.ReadAllCountriesForSelect()
            };
            ViewBag.Id = id;
            return View(viewModel);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Name,Mobile,Tell,ReceiverName,CountryId,StateId,CityId,PostalCode,Address,SendType")] OrderCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                HttpCookie Cookie = Request.Cookies["cart"];
                if (Cookie != null && Cookie.Value != null && Cookie.Value != "")
                {
                    var printId = await orderService.Create(viewModel, Cookie.Value);
                    if (printId > 0)
                    {
                        Response.Cookies["cart"].Expires = DateTime.Now.AddDays(-1);
                        return RedirectToAction("Print", new { id = printId });
                    }
                }
            }
            return RedirectToAction("Create", new { id = "error" });
        }

        [HttpPost]
        [Authorize(Roles = "Developer,SuperAdministrator,Order")]
        public async Task<JsonResult> Confirm(int id, string type)
        {
            bool? confirm = null;
            if (type != null && type != "" && type.ToLower() == "confirm")
            {
                confirm = true;
            }
            else if (type != null && type != "" && type.ToLower() == "reject")
            {
                confirm = false;
            }
            return Json(await orderService.Confirm(id, confirm));
        }

        [HttpPost]
        public async Task<JsonResult> Archive(int id)
        {
            return Json(await orderService.Archive(id));
        }

        //[HttpPost]
        //public async Task<JsonResult> Delete(int id)
        //{
        //    return Json(await orderService.Delete(id));
        //}
    }
}