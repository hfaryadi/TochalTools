using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TochalTools.Classes;
using TochalTools.Models;

namespace TochalTools.Services
{
    public interface IOrder
    {
        Task<OrderListPageViewModel> ReadAll(string type = null);
        Task<OrderDetailsPageViewModel> ReadById(int id, bool isPrintId = false);
        Task<int> Create(OrderCreateViewModel viewModel, string cookie);
        Task<bool> Confirm(int orderId, bool? confirm);
        Task<bool> Archive(int orderId);
        Task<bool> Delete(int orderId);
    }
    public class OrderService : IOrder
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public async Task<OrderListPageViewModel> ReadAll(string type = null)
        {
            var role = "Others";
            var userId = HttpContext.Current.User.Identity.GetUserId();
            if (HttpContext.Current.User.Identity.IsAuthenticated && (HttpContext.Current.User.IsInRole("Developer") || HttpContext.Current.User.IsInRole("SuperAdministrator") || HttpContext.Current.User.IsInRole("Order")))
            {
                role = "Administrator";
            }
            var model = await db.Orders.Where(x => ((role == "Administrator" && !x.IsAdminDeleted) || (role != "Administrator" && !x.IsUserDeleted)) && (role == "Administrator" || x.UserId == userId)).OrderByDescending(x => x.Id).ToListAsync();
            var viewModel = new OrderListPageViewModel
            {
                AllOrdersCount = model.Where(x => (role == "Administrator" && !x.IsAdminArchived) || (role != "Administrator" && !x.IsUserArchived)).Count(),
                ConfirmedOrdersCount = model.Where(x => ((role == "Administrator" && !x.IsAdminArchived) || (role != "Administrator" && !x.IsUserArchived)) && x.IsConfirmed == true).Count(),
                RejectedOrdersCount = model.Where(x => ((role == "Administrator" && !x.IsAdminArchived) || (role != "Administrator" && !x.IsUserArchived)) && x.IsConfirmed == false).Count(),
                UnConfirmedOrdersCount = model.Where(x => ((role == "Administrator" && !x.IsAdminArchived) || (role != "Administrator" && !x.IsUserArchived)) && x.IsConfirmed == null).Count(),
            };
            if (type != null && type != "")
            {
                switch(type.ToLower())
                {
                    case "confirmed":
                        model = model.Where(x => ((role == "Administrator" && !x.IsAdminArchived) || (role != "Administrator" && !x.IsUserArchived)) && x.IsConfirmed == true).ToList();
                        break;
                    case "rejected":
                        model = model.Where(x => ((role == "Administrator" && !x.IsAdminArchived) || (role != "Administrator" && !x.IsUserArchived)) && x.IsConfirmed == false).ToList();
                        break;
                    case "unconfirmed":
                        model = model.Where(x => ((role == "Administrator" && !x.IsAdminArchived) || (role != "Administrator" && !x.IsUserArchived)) && x.IsConfirmed == null).ToList();
                        break;
                    case "archived":
                        model = model.Where(x => (role == "Administrator" && x.IsAdminArchived) || (role != "Administrator" && x.IsUserArchived)).ToList();
                        break;
                    case "myorders":
                        model = model.Where(x => ((role == "Administrator" && !x.IsAdminArchived) || (role != "Administrator" && !x.IsUserArchived)) && x.UserId == userId).ToList();
                        break;
                }
            }
            else
            {
                model = model.Where(x => (role == "Administrator" && !x.IsAdminArchived) || (role != "Administrator" && !x.IsUserArchived)).ToList();
            }
            var Orders = model.Select(x => new OrderListViewModel
            {
                Id = x.Id,
                Date = DateTimeHelper.ConvertToShamsi(x.Date),
                IsAdminArchived = x.IsAdminArchived,
                IsUserArchived = x.IsUserArchived,
                IsConfirmed = x.IsConfirmed,
                Mobile = x.Mobile,
                Name = x.Name,
                PrintId = x.PrintId,
                ReceiverName = x.ReceiverName,
                Status = new Status
                {
                    Class = (x.IsConfirmed == true) ? "success" : ((x.IsConfirmed == false) ? "danger" : "warning"),
                    Text = (x.IsConfirmed == true) ? "تایید شده" : ((x.IsConfirmed == false) ? "رد شده" : "در انتظار تایید")
                }
            }).ToList();
            viewModel.Orders = Orders;
            return viewModel;
        }

        public async Task<OrderDetailsPageViewModel> ReadById(int id, bool isPrintId = false)
        {
            OrderModel model = null;
            if (isPrintId)
            {
                if (!HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    model = await db.Orders.Where(x => !x.IsUserDeleted && x.PrintId == id).FirstOrDefaultAsync();
                }
                else
                {
                    var role = "Others";
                    if (HttpContext.Current.User.Identity.IsAuthenticated && (HttpContext.Current.User.IsInRole("Developer") || HttpContext.Current.User.IsInRole("SuperAdministrator") || HttpContext.Current.User.IsInRole("Order")))
                    {
                        role = "Administrator";
                    }
                    model = await db.Orders.Where(x => ((role == "Administrator" && !x.IsAdminDeleted) || (role != "Administrator" && !x.IsUserDeleted)) && x.PrintId == id).FirstOrDefaultAsync();
                }
            }
            else
            {
                var role = "Others";
                var userId = HttpContext.Current.User.Identity.GetUserId();
                if (HttpContext.Current.User.Identity.IsAuthenticated && (HttpContext.Current.User.IsInRole("Developer") || HttpContext.Current.User.IsInRole("SuperAdministrator") || HttpContext.Current.User.IsInRole("Order")))
                {
                    role = "Administrator";
                }
                model = await db.Orders.Where(x => ((role == "Administrator" && !x.IsAdminDeleted) || (role != "Administrator" && !x.IsUserDeleted)) && x.Id == id && (role == "Administrator" || x.UserId == userId)).FirstOrDefaultAsync();
            }
            if (model == null)
            {
                return null;
            }
            Int64 Price = 0;
            var OrderItems = await db.OrderItems.Where(x => !x.IsDeleted && x.OrderId == model.Id).OrderBy(x => x.Title).Select(x => new OrderItemListViewModel
            {
                Id = x.Id,
                Count = x.Count,
                Price = x.Price,
                ProductId = x.ProductId,
                Title = x.Title,
                TotalPrice = x.Price * x.Count
            }).ToListAsync();
            foreach (var item in OrderItems)
            {
                Price = Price + item.TotalPrice;
            }
            var Order = new OrderDetailsViewModel
            {
                Id = model.Id,
                Address = model.Address,
                City = await db.Cities.Where(x => x.Id == model.CityId).Select(x => x.Name).FirstOrDefaultAsync(),
                Country = await db.Countries.Where(x => x.Id == model.CountryId).Select(x => x.Name).FirstOrDefaultAsync(),
                Date = DateTimeHelper.ConvertToShamsi(model.Date, true),
                Mobile = model.Mobile,
                Name = model.Name,
                PostalCode = model.PostalCode,
                Price = Price,
                PrintId = model.PrintId,
                ReceiverName = model.ReceiverName,
                SendType = model.SendType,
                State = await db.States.Where(x => x.Id == model.StateId).Select(x => x.Name).FirstOrDefaultAsync(),
                Status = new Status
                {
                    Class = (model.IsConfirmed == true) ? "success" : ((model.IsConfirmed == false) ? "danger" : "warning"),
                    Text = (model.IsConfirmed == true) ? "تایید شده" : ((model.IsConfirmed == false) ? "رد شده" : "در انتظار تایید")
                },
                Tell = model.Tell
            };
            var info = db.Infos.Where(x => !x.IsDeleted && x.Language == "fa").FirstOrDefault();
            return new OrderDetailsPageViewModel
            {
                Order = Order,
                OrderItems = OrderItems,
                Info = (info != null) ? info : new InfoModel { }
            };
        }

        public async Task<int> Create(OrderCreateViewModel viewModel, string cookie)
        {
            if (cookie == null || cookie == "")
            {
                return 0;
            }
            var now = DateTime.Now;
            List<OrderItemModel> list = new List<OrderItemModel>();
            var Products = await db.Products.Where(x => !x.IsDeleted && x.IsPublicationActive && x.QT > 0).ToListAsync();
            var products = cookie.Split(',');
            foreach (var item in products)
            {
                var prds = item.Split('-');
                var productId = Convert.ToInt32(prds[0]);
                var count = Convert.ToInt32(prds[1]);
                var product = Products.Where(x => x.Id == productId).FirstOrDefault();
                if (product == null && count > 0)
                {
                    return 0;
                }
                if (count > 0)
                {
                    var Item = new OrderItemModel
                    {
                        Count = count,
                        IsDeleted = false,
                        Price = (product.Off > 0 && (product.OffExpireDate == null || (product.OffExpireDate >= now))) ? (product.Price - product.Off) : product.Price,
                        ProductId = product.Id,
                        Title = product.Title
                    };
                    list.Add(Item);
                }
            }
            if (list.Count() > 0)
            {
                var isCreated = false;
                string userId = null;
                string password = "";
                ProfileService profileService = new ProfileService();
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    userId = HttpContext.Current.User.Identity.GetUserId();
                }
                else
                {
                    
                    var UserId = await profileService.ReadUserIdByMobile(viewModel.Mobile);
                    if (UserId != null && UserId != "")
                    {
                        userId = UserId;
                    }
                    else
                    {
                        var userInfo = await profileService.CreateUserForAspAndProfile(viewModel.Mobile, viewModel.Name, viewModel.Tell, viewModel.CountryId, viewModel.StateId, viewModel.CityId, viewModel.PostalCode, viewModel.Address);
                        if (userInfo != null)
                        {
                            userId = userInfo.Value;
                            password = userInfo.Text;
                            isCreated = true;
                        }
                    }
                }
                Random rnd = new Random();
                bool e = false;
                int PrintId = 0;
                while(!e)
                {
                    PrintId = rnd.Next(100000, 999999);
                    var Exist = await db.Orders.Where(x => x.PrintId == PrintId).FirstOrDefaultAsync();
                    if (Exist == null)
                    {
                        e = true;
                    }
                }
                OrderModel model = new OrderModel
                {
                    Address = viewModel.Address,
                    CityId = viewModel.CityId,
                    CountryId = viewModel.CountryId,
                    Date = DateTime.Now,
                    IsAdminArchived = false,
                    IsUserArchived = false, 
                    IsConfirmed = null,
                    IsAdminDeleted = false,
                    IsUserDeleted = false,
                    Mobile = viewModel.Mobile,
                    Name = viewModel.Name,
                    PostalCode = viewModel.PostalCode,
                    PrintId = PrintId,
                    ReceiverName = viewModel.ReceiverName,
                    SendType = viewModel.SendType,
                    StateId = viewModel.StateId,
                    Tell = viewModel.Tell,
                    UserId = userId
                };
                db.Orders.Add(model);
                await db.SaveChangesAsync();
                foreach (var item in list)
                {
                    item.OrderId = model.Id;
                }
                db.OrderItems.AddRange(list);
                await db.SaveChangesAsync();
                SmsHelper sms = new SmsHelper();
                var mobile = new[] { viewModel.Mobile };
                var content = "مشتری گرامی" + System.Environment.NewLine +
                "سفارش شما با شماره پیگیری " + model.PrintId + " ثبت شد و در حال بررسی می باشد." + System.Environment.NewLine +
                "توچال تولز";
                if (isCreated)
                {
                    content = "مشتری گرامی" + System.Environment.NewLine +
                    "سفارش شما با شماره پیگیری " + model.PrintId + " ثبت شد و در حال بررسی می باشد." + System.Environment.NewLine +
                    "همچنین شما می توانید با ورود به پنل کاربری سفارشات خود را مدیریت نمایید." + System.Environment.NewLine +
                    "نام کاربری: " + viewModel.Mobile + System.Environment.NewLine +
                    "کلمه عبور: " + password + System.Environment.NewLine +
                    "توچال تولز";
                }
                sms.SendSMS(mobile, content);
                var adminMobiles = await profileService.ReadAllMobilesByRoleForSelect("SuperAdministrator,Order");
                if (adminMobiles != null)
                {
                    var adminContent = "سفارش جدید در سایت ثبت شد." + System.Environment.NewLine +
                    "شماره سفارش: " + model.PrintId + System.Environment.NewLine +
                    "توچال تولز";
                    sms.SendSMS(adminMobiles, adminContent);
                }
                return model.PrintId;
            }
            return 0;
        }

        public async Task<bool> Confirm(int orderId, bool? confirm)
        {
            var role = "Others";
            var userId = HttpContext.Current.User.Identity.GetUserId();
            if (HttpContext.Current.User.Identity.IsAuthenticated && (HttpContext.Current.User.IsInRole("Developer") || HttpContext.Current.User.IsInRole("SuperAdministrator") || HttpContext.Current.User.IsInRole("Order")))
            {
                role = "Administrator";
            }
            var model = await db.Orders.FindAsync(orderId);
            if (model == null || role != "Administrator")
            {
                return false;
            }
            model.IsConfirmed = confirm;
            db.Entry(model).State = EntityState.Modified;
            var content = "";
            if (confirm == true)
            {
                var OrderItems = await db.OrderItems.Where(x => !x.IsDeleted && x.OrderId == model.Id).ToListAsync();
                var Products = await db.Products.ToListAsync();
                foreach (var item in OrderItems)
                {
                    var product = Products.Where(x => x.Id == item.ProductId).FirstOrDefault();
                    if (product != null)
                    {
                        product.BuyCount++;
                        product.QT = product.QT - item.Count;
                        if (product.QT < 0)
                        {
                            product.QT = 0;
                        }
                        db.Entry(product).State = EntityState.Modified;
                    }
                }
                content = "مشتری گرامی" + System.Environment.NewLine +
                "سفارش شما با شماره پیگیری " + model.PrintId + " تایید شد و آماده ارسال می باشد." + System.Environment.NewLine +
                "توچال تولز";
            }
            else if (confirm == false)
            {
                content = "مشتری گرامی" + System.Environment.NewLine +
                "سفارش شما با شماره پیگیری " + model.PrintId + " رد شد. برای کسب اطلاعات بیشتر با کارشناسان ما تماس حاصل نمایید." + System.Environment.NewLine +
                "توچال تولز";
            }
            await db.SaveChangesAsync();
            if (content != "")
            {
                ProfileService profileService = new ProfileService();
                var mobile = await profileService.ReadMobileById(model.UserId);
                if (mobile != null && mobile != "")
                {
                    var mobiles = new[] { mobile };
                    SmsHelper sms = new SmsHelper();
                    sms.SendSMS(mobiles, content);
                }
            }
            return true;
        }

        public async Task<bool> Archive(int orderId)
        {
            bool isArchived = false;
            var role = "Others";
            var userId = HttpContext.Current.User.Identity.GetUserId();
            if (HttpContext.Current.User.Identity.IsAuthenticated && (HttpContext.Current.User.IsInRole("Developer") || HttpContext.Current.User.IsInRole("SuperAdministrator") || HttpContext.Current.User.IsInRole("Order")))
            {
                role = "Administrator";
            }
            var model = await db.Orders.FindAsync(orderId);
            if (model == null || (role != "Administrator" && model.UserId != userId))
            {
                return false;
            }
            if (role == "Administrator")
            {
                model.IsAdminArchived = true;
                isArchived = true;
            }
            if (model.UserId == userId)
            {
                model.IsUserArchived = true;
                isArchived = true;
            }
            if (isArchived)
            {
                db.Entry(model).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
            return isArchived;
        }

        public async Task<bool> Delete(int orderId)
        {
            bool isDeleted = false;
            var role = "Others";
            var userId = HttpContext.Current.User.Identity.GetUserId();
            if (HttpContext.Current.User.Identity.IsAuthenticated && (HttpContext.Current.User.IsInRole("Developer") || HttpContext.Current.User.IsInRole("SuperAdministrator") || HttpContext.Current.User.IsInRole("Order")))
            {
                role = "Administrator";
            }
            var model = await db.Orders.FindAsync(orderId);
            if (model == null || (role != "Administrator" && model.UserId != userId))
            {
                return false;
            }
            if (role == "Administrator")
            {
                model.IsAdminDeleted = true;
                isDeleted = true;
            }
            if (model.UserId == userId)
            {
                model.IsUserDeleted = true;
                isDeleted = true;
            }
            if (isDeleted)
            {
                db.Entry(model).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
            return isDeleted;
        }
    }
}