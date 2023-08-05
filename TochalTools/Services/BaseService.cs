using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TochalTools.Models;

namespace TochalTools.Services
{
    public interface IBase
    {
        UserDetailsViewModel ReadUserDetails();
        InfoModel ReadWebsiteInfo(string language);
        Task<InfoModel> ReadWebsiteInfoAsync(string language);
        ProfileModel ReadProfileInfo();
        Task<ProfileModel> ReadProfileInfoAsync();
        int GetAllUnConfirmedCommentsCountByGroup(string group);
        int GetAllUnConfirmedOrdersCount();
        Task<int> GetAllUnConfirmedCommentsCountByGroupAsync(string group);
        int GetAllUnReadInboxCount();
        Task<int> GetAllUnReadInboxCountAsync();
    }
    public class BaseService : IBase
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public UserDetailsViewModel ReadUserDetails()
        {
            var role = "Others";
            if (HttpContext.Current.User.Identity.IsAuthenticated && (HttpContext.Current.User.IsInRole("Developer") || HttpContext.Current.User.IsInRole("SuperAdministrator") || HttpContext.Current.User.IsInRole("Administrator")))
            {
                role = "Administrator";
            }
            return new UserDetailsViewModel
            {
                Role = role,
                UserId = (HttpContext.Current.User.Identity.IsAuthenticated) ? HttpContext.Current.User.Identity.GetUserId() : null
            };
        }

        public InfoModel ReadWebsiteInfo(string language)
        {
            return db.Infos.Where(x => !x.IsDeleted && x.Language == language).FirstOrDefault();
        }

        public async Task<InfoModel> ReadWebsiteInfoAsync(string language)
        {
            return await db.Infos.Where(x => !x.IsDeleted && x.Language == language).FirstOrDefaultAsync();
        }

        public ProfileModel ReadProfileInfo()
        {
            return db.Profiles.Find(HttpContext.Current.User.Identity.GetUserId());
        }

        public async Task<ProfileModel> ReadProfileInfoAsync()
        {
            return await db.Profiles.FindAsync(HttpContext.Current.User.Identity.GetUserId());
        }

        public int GetAllUnConfirmedCommentsCountByGroup(string group)
        {
            return db.Comments.Where(x => !x.IsDeleted && !x.IsConfirmed && x.Group == group).Count();
        }

        public int GetAllUnConfirmedOrdersCount()
        {
            return db.Orders.Where(x => !x.IsAdminDeleted && x.IsConfirmed == null ).Count();
        }

        public async Task<int> GetAllUnConfirmedCommentsCountByGroupAsync(string group)
        {
            return await db.Comments.Where(x => !x.IsDeleted && !x.IsConfirmed && x.Group == group).CountAsync();
        }

        public int GetAllUnReadInboxCount()
        {
            var role = "Other";
            var userId = HttpContext.Current.User.Identity.GetUserId();
            if (HttpContext.Current.User.IsInRole("Developer") || HttpContext.Current.User.IsInRole("SuperAdministrator"))
            {
                role = "Administrator";
            }
            return db.Inbox.Where(x => !x.IsReceiverDeleted && !x.IsRead && ((x.ReceiverId != null && x.ReceiverId != "" && x.ReceiverId == userId) || (role == "Administrator" && (x.ReceiverId == null || x.ReceiverId == "")))).Count();
        }

        public async Task<int> GetAllUnReadInboxCountAsync()
        {
            var role = "Other";
            var userId = HttpContext.Current.User.Identity.GetUserId();
            if (HttpContext.Current.User.IsInRole("Developer") || HttpContext.Current.User.IsInRole("SuperAdministrator"))
            {
                role = "Administrator";
            }
            return await db.Inbox.Where(x => !x.IsReceiverDeleted && !x.IsRead && ((x.ReceiverId != null && x.ReceiverId != "" && x.ReceiverId == userId) || (role == "Administrator" && (x.ReceiverId == null || x.ReceiverId == "")))).CountAsync();
        }
    }
}