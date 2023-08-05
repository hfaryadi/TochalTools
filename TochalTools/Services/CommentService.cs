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
    public interface IComment
    {
        Task<List<CommentIndexViewModel>> ReadAllByGroupAndRefId(string group, string refId);
        Task<List<CommentListViewModel>> ReadAll(string group, bool isConfirmed = true);
        Task<bool> Create(CommentCreateViewModel viewModel);
        Task<bool> Confirm(int commentId);
        Task<bool> Delete(int commentId);
    }
    public class CommentService : IComment
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public async Task<List<CommentIndexViewModel>> ReadAllByGroupAndRefId(string group, string refId)
        {
            var model = await db.Comments.Where(x => !x.IsDeleted && x.IsConfirmed && x.Group == group && x.RefId == refId).OrderByDescending(x => x.Date).ToListAsync();
            var Profiles = await db.Profiles.ToListAsync();
            return model.Where(x => x.CommentId == 0).Select(x => new CommentIndexViewModel
            {
                Id = x.Id,
                Content = x.Content,
                Date = DateTimeHelper.ConvertToShamsi(x.Date, true),
                Name = x.Name,
                UserId = x.UserId,
                Replays = model.Where(z => z.CommentId == x.Id).Select(z => new CommentIndexViewModel
                {
                    Id = z.Id,
                    Content = z.Content,
                    Date = DateTimeHelper.ConvertToShamsi(z.Date, true),
                    Name = z.Name,
                    UserId = z.UserId
                }).ToList()
            }).ToList();
        }

        public async Task<List<CommentListViewModel>> ReadAll(string group, bool isConfirmed = true)
        {
            var model = await db.Comments.Where(x => x.Group == group).OrderByDescending(x => x.Date).ToListAsync();
            var Profiles = await db.Profiles.ToListAsync();
            return model.Where(x => !x.IsDeleted && x.IsConfirmed == isConfirmed).Select(x => new CommentListViewModel
            {
                Id = x.Id,
                Content = x.Content,
                CommentId = x.CommentId,
                CommentName = x.CommentId > 0 ? "پاسخ به " + model.Where(z => z.Id == x.CommentId).Select(z => z.Name).FirstOrDefault() : "",
                Date = DateTimeHelper.ConvertToShamsi(x.Date),
                Group = x.Group,
                GroupName = ModuleHelper.ReadPersianNameByISO(x.Group),
                IsConfirmed = x.IsConfirmed,
                Name = x.Name,
                RefId = x.RefId,
                UserId = x.UserId
            }).ToList();
        }

        public async Task<bool> Create(CommentCreateViewModel viewModel)
        {
            BaseService baseService = new BaseService();
            var userDetails = baseService.ReadUserDetails();
            var isConfirmed = (userDetails.Role == "Administrator") ? true : false;
            CommentModel model = new CommentModel
            {
                CommentId = viewModel.CommentId,
                Content = viewModel.Content,
                Date = DateTime.Now,
                Group = viewModel.Group,
                IsConfirmed = isConfirmed,
                IsDeleted = false,
                Name = viewModel.Name,
                RefId = viewModel.RefId,
                UserId = userDetails.UserId
            };
            db.Comments.Add(model);
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Confirm(int commentId)
        {
            var model = await db.Comments.FindAsync(commentId);
            if (model == null)
            {
                return false;
            }
            model.IsConfirmed = true;
            db.Entry(model).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int commentId)
        {
            var model = await db.Comments.FindAsync(commentId);
            if (model == null)
            {
                return false;
            }
            model.IsDeleted = true;
            db.Entry(model).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return true;
        }
    }
}