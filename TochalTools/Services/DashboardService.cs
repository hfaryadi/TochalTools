using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TochalTools.Models;

namespace TochalTools.Services
{
    public interface IDashboard
    {
        Task<DashboardAdminViewModel> Admin();
        Task<DashboardUserViewModel> User();

    }
    public class DashboardService : IDashboard
    {
        ApplicationDbContext db = new ApplicationDbContext();

        private ApplicationUserManager _userManager;

        public DashboardService()
        {
        }

        public DashboardService(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public async Task<DashboardAdminViewModel> Admin()
        {
            var role = "Others";
            if (HttpContext.Current.User.IsInRole("Developer"))
            {
                role = "Developer";
            }
            var UserIds = UserManager.Users.Select(x => x.Id).ToList();
            var Profiles = await db.Profiles.Where(x => (role == "Developer" || x.Role != "Developer")).ToListAsync();
            var blogCount = await db.Blogs.Where(x => !x.IsDeleted).CountAsync();
            var productCount = await db.Products.Where(x => !x.IsDeleted).CountAsync();
            var orderCount = await db.Orders.Where(x => !x.IsAdminDeleted && !x.IsAdminArchived).CountAsync();
            var userCount = Profiles.Where(x => UserIds != null && UserIds.Contains(x.Id)).Count();
            return new DashboardAdminViewModel
            {
                BlogCount = blogCount,
                OrderCount = orderCount,
                ProductCount = productCount,
                UserCount = userCount
            };
        }

        public async Task<DashboardUserViewModel> User()
        {
            var role = "Others";
            var userId = HttpContext.Current.User.Identity.GetUserId();
            if (HttpContext.Current.User.Identity.IsAuthenticated && (HttpContext.Current.User.IsInRole("Developer") || HttpContext.Current.User.IsInRole("SuperAdministrator") || HttpContext.Current.User.IsInRole("Order")))
            {
                role = "Administrator";
            }
            var model = await db.Orders.Where(x => ((role == "Administrator" && !x.IsAdminDeleted) || (role != "Administrator" && !x.IsUserDeleted)) && (role == "Administrator" || x.UserId == userId)).OrderByDescending(x => x.Id).ToListAsync();
            return new DashboardUserViewModel
            {
                AllOrdersCount = model.Where(x => (role == "Administrator" && !x.IsAdminArchived) || (role != "Administrator" && !x.IsUserArchived)).Count(),
                ConfirmedOrdersCount = model.Where(x => ((role == "Administrator" && !x.IsAdminArchived) || (role != "Administrator" && !x.IsUserArchived)) && x.IsConfirmed == true).Count(),
                RejectedOrdersCount = model.Where(x => ((role == "Administrator" && !x.IsAdminArchived) || (role != "Administrator" && !x.IsUserArchived)) && x.IsConfirmed == false).Count(),
                UnConfirmedOrdersCount = model.Where(x => ((role == "Administrator" && !x.IsAdminArchived) || (role != "Administrator" && !x.IsUserArchived)) && x.IsConfirmed == null).Count(),
            };
        }
    }
}