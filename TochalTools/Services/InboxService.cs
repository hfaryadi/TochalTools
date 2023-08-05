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
    public interface IInbox
    {
        Task<List<InboxListViewModel>> ReadAll(string type);
        Task<InboxDetailsViewModel> ReadById(int inboxId);
        Task Create(InboxCreateViewModel viewModel);
        Task<bool> CustomCreate(InboxCustomCreateViewModel viewModel);
        Task<bool> Delete(int inboxId);
    }
    public class InboxService : IInbox
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public async Task<List<InboxListViewModel>> ReadAll(string type)
        {
            var role = "Other";
            var userId = HttpContext.Current.User.Identity.GetUserId();
            if (HttpContext.Current.User.IsInRole("Developer") || HttpContext.Current.User.IsInRole("SuperAdministrator"))
            {
                role = "Administrator";
            }
            var model = new List<InboxModel>();
            var Profiles = await db.Profiles.ToListAsync();
            if (type.ToLower() == "inbox")
            {
                model = await db.Inbox.Where(x => !x.IsReceiverDeleted && ((x.ReceiverId != null && x.ReceiverId != "" && x.ReceiverId == userId) || (role == "Administrator" && (x.ReceiverId == null || x.ReceiverId == "")))).OrderByDescending(x => x.Date).ToListAsync();
            }
            else if (type.ToLower() == "outbox")
            {
                model = await db.Inbox.Where(x => !x.IsUserDeleted && x.UserId != null && x.UserId != "" && x.UserId == userId).OrderByDescending(x => x.Date).ToListAsync();
            }
            return model.Select(x => new InboxListViewModel
            {
                Id = x.Id,
                Date = DateTimeHelper.ConvertToShamsi(x.Date, true),
                Group = InboxHelper.ReadGroupByISO(x.Group),
                ReceiverId = x.ReceiverId,
                Subject = x.Subject,
                UserId = x.UserId,
                ReceiverName = Profiles.Where(z => z.Id == x.ReceiverId).Select(z => z.Name).FirstOrDefault(),
                Name = (x.UserId != null && x.UserId != "") ? ((Profiles.Where(z => z.Id == x.UserId).Select(z => z.Name).FirstOrDefault() != null && Profiles.Where(z => z.Id == x.UserId).Select(z => z.Name).FirstOrDefault() != "") ? Profiles.Where(z => z.Id == x.UserId).Select(z => z.Name).FirstOrDefault() : "بدون نام") : x.Name,
                Status = new Status
                {
                    Class = (x.IsRead) ? "success" : "danger",
                    Text = (x.IsRead) ? "خوانده شده" : "خوانده نشده"
                }
            }).ToList();
        }

        public async Task<InboxDetailsViewModel> ReadById(int inboxId)
        {
            var role = "Other";
            var userId = HttpContext.Current.User.Identity.GetUserId();
            if (HttpContext.Current.User.IsInRole("Developer") || HttpContext.Current.User.IsInRole("SuperAdministrator"))
            {
                role = "Administrator";
            }
            var model = await db.Inbox.FindAsync(inboxId);
            if (model != null && (((model.ReceiverId != null && model.ReceiverId != "" && model.ReceiverId == userId) || (model.UserId != null && model.UserId != "" && model.UserId == userId)) || (role == "Administrator" && (model.ReceiverId == null || model.ReceiverId == ""))))
            {
                if (model.ReceiverId == userId || (role == "Administrator" && (model.ReceiverId == null || model.ReceiverId == "")))
                {
                    model.IsRead = true;
                    db.Entry(model).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return new InboxDetailsViewModel
                    {
                        Id = model.Id,
                        Content = model.Content,
                        Date = DateTimeHelper.ConvertToShamsi(model.Date, true),
                        Email = (model.UserId != null && model.UserId != "") ? await db.Profiles.Where(x => x.Id == model.UserId).Select(x => x.Email).FirstOrDefaultAsync() : model.Email,
                        Type = "ارسال کننده",
                        GroupName = InboxHelper.ReadGroupByISO(model.Group),
                        Name = (model.UserId != null && model.UserId != "") ? ((await db.Profiles.Where(x => x.Id == model.UserId).Select(x => x.Name).FirstOrDefaultAsync() != null && await db.Profiles.Where(x => x.Id == model.UserId).Select(x => x.Name).FirstOrDefaultAsync() != "") ? await db.Profiles.Where(x => x.Id == model.UserId).Select(x => x.Name).FirstOrDefaultAsync() : "بدون نام") : model.Name,
                        ReceiverId = model.ReceiverId,
                        Subject = model.Subject,
                        Tell = (model.UserId != null && model.UserId != "") ? await db.Profiles.Where(x => x.Id == model.UserId).Select(x => x.Tell).FirstOrDefaultAsync() : model.Tell,
                        UserId = model.UserId,
                        Website = (model.UserId != null && model.UserId != "") ? await db.Profiles.Where(x => x.Id == model.UserId).Select(x => x.Website).FirstOrDefaultAsync() : model.Website,
                        Status = new Status
                        {
                            Class = (model.IsRead) ? "success" : "danger",
                            Text = (model.IsRead) ? "خوانده شده" : "خوانده نشده"
                        }
                    };
                }
                else
                {
                    return new InboxDetailsViewModel
                    {
                        Id = model.Id,
                        Content = model.Content,
                        Date = DateTimeHelper.ConvertToShamsi(model.Date, true),
                        Email = (model.ReceiverId != null && model.ReceiverId != "") ? await db.Profiles.Where(x => x.Id == model.ReceiverId).Select(x => x.Email).FirstOrDefaultAsync() :"",
                        Type = "دریافت کننده",
                        GroupName = InboxHelper.ReadGroupByISO(model.Group),
                        Name = (model.ReceiverId != null && model.ReceiverId != "") ? await db.Profiles.Where(x => x.Id == model.ReceiverId).Select(x => x.Name).FirstOrDefaultAsync() : "",
                        ReceiverId = model.ReceiverId,
                        Subject = model.Subject,
                        Tell = (model.ReceiverId != null && model.ReceiverId != "") ? await db.Profiles.Where(x => x.Id == model.ReceiverId).Select(x => x.Tell).FirstOrDefaultAsync() : "",
                        UserId = model.UserId,
                        Website = (model.ReceiverId != null && model.ReceiverId != "") ? await db.Profiles.Where(x => x.Id == model.ReceiverId).Select(x => x.Website).FirstOrDefaultAsync() : "",
                        Status = new Status
                        {
                            Class = (model.IsRead) ? "success" : "danger",
                            Text = (model.IsRead) ? "خوانده شده" : "خوانده نشده"
                        }
                    };
                }
            }
            return null;
        }

        public async Task Create(InboxCreateViewModel viewModel)
        {
            var now = DateTime.Now;
            var userId = HttpContext.Current.User.Identity.GetUserId();
            var receivers = viewModel.ReceiverId.Split(',');
            foreach (var item in receivers)
            {
                InboxModel model = new InboxModel
                {
                    Content = viewModel.Content,
                    Date = now,
                    Group = "Direct",
                    IsRead = false,
                    IsReceiverDeleted = false,
                    IsUserDeleted = false,
                    ReceiverId = item,
                    Subject = viewModel.Subject,
                    UserId = userId
                };
                db.Inbox.Add(model);
            }
            await db.SaveChangesAsync();
        }

        public async Task<bool> CustomCreate(InboxCustomCreateViewModel viewModel)
        {
            BaseService baseService = new BaseService();
            var userDetails = baseService.ReadUserDetails();
            InboxModel model = new InboxModel
            {
                Content = viewModel.Content,
                Date = DateTime.Now,
                Email = (userDetails.UserId != null && userDetails.UserId != "") ? null : viewModel.Email,
                Group = viewModel.Group,
                IsRead = false,
                IsReceiverDeleted = false,
                IsUserDeleted = false,
                Name = (userDetails.UserId != null && userDetails.UserId != "") ? null : viewModel.Name,
                Subject = viewModel.Subject,
                Tell = (userDetails.UserId != null && userDetails.UserId != "") ? null : viewModel.Tell,
                UserId = userDetails.UserId,
                Website = (userDetails.UserId != null && userDetails.UserId != "") ? null : viewModel.Website
            };
            db.Inbox.Add(model);
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int inboxId)
        {
            bool isDeleted = false;
            var role = "Other";
            var userId = HttpContext.Current.User.Identity.GetUserId();
            if (HttpContext.Current.User.IsInRole("Developer") || HttpContext.Current.User.IsInRole("SuperAdministrator"))
            {
                role = "Administrator";
            }
            var model = await db.Inbox.FindAsync(inboxId);
            if (model != null && (((model.ReceiverId != null && model.ReceiverId != "" && model.ReceiverId == userId) || (model.UserId != null && model.UserId != "" && model.UserId == userId)) || (role == "Administrator" && (model.ReceiverId == null || model.ReceiverId == ""))))
            {
                if (model.UserId == userId)
                {
                    model.IsUserDeleted = true;
                    isDeleted = true;
                }
                if (model.ReceiverId == userId || (role == "Administrator" && (model.ReceiverId == null || model.ReceiverId == "")))
                {
                    model.IsReceiverDeleted = true;
                    isDeleted = true;
                }
                if (isDeleted)
                {
                    db.Entry(model).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                }
            }
            return isDeleted;
        }
    }
}