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
    public interface IBox
    {
        Task<BoxIndexViewModel> ReadByAddress(string address);
        Task<List<BoxIndexViewModel>> ReadAllByAddress(string address);
        Task<List<BoxListViewModel>> ReadAll();
        Task<int> Create(BoxCreateViewModel viewModel);
        Task<BoxEditViewModel> ReadByIdForEdit(int boxId);
        Task<bool> Edit(BoxEditViewModel viewModel);
        Task<bool> Delete(int boxId);
    }
    public class BoxService : IBox
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public async Task<BoxIndexViewModel> ReadByAddress(string address)
        {
            return await db.Boxes.Where(x => !x.IsDeleted && x.Address == address).Select(x => new BoxIndexViewModel
            {
                Id = x.Id,
                Content = x.Content,
                Icon = x.Icon,
                Link = x.Link,
                Title = x.Title
            }).FirstOrDefaultAsync();
        }

        public async Task<List<BoxIndexViewModel>> ReadAllByAddress(string address)
        {
            return await db.Boxes.Where(x => !x.IsDeleted && x.Address.StartsWith(address)).OrderBy(x => x.Priority).Select(x => new BoxIndexViewModel
            {
                Id = x.Id,
                Content = x.Content,
                Icon = x.Icon,
                Link = x.Link,
                Title = x.Title
            }).ToListAsync();
        }

        public async Task<List<BoxListViewModel>> ReadAll()
        {
            var model = await db.Boxes.Where(x => !x.IsDeleted).OrderBy(x => x.Address).ToListAsync();
            return model.Select(x => new BoxListViewModel
            {
                Id = x.Id,
                Address = x.Address,
                Language = LanguageHelper.ReadLanguageByValue(x.Language),
                Priority = x.Priority,
                Title = x.Title,
                Update = DateTimeHelper.ConvertToShamsi(x.Update)
            }).ToList();
        }

        public async Task<int> Create(BoxCreateViewModel viewModel)
        {
            var now = DateTime.Now;
            if (HttpContext.Current.User.IsInRole("Developer"))
            {
                var box = await db.Boxes.Where(x => !x.IsDeleted && x.Address == viewModel.Address).FirstOrDefaultAsync();
                if (box != null)
                {
                    return 0;
                }
                BoxModel model = new BoxModel
                {
                    Address = viewModel.Address,
                    Content = viewModel.Content,
                    Date = now,
                    Icon = viewModel.Icon,
                    IsDeleted = false,
                    Language = viewModel.Language,
                    Link = viewModel.Link,
                    Priority = viewModel.Priority,
                    Title = viewModel.Title,
                    Update = now
                };
                db.Boxes.Add(model);
                await db.SaveChangesAsync();
                return model.Id;
            }
            return 0;
        }

        public async Task<BoxEditViewModel> ReadByIdForEdit(int boxId)
        {
            var model = await db.Boxes.FindAsync(boxId);
            if (model == null)
            {
                return null;
            }
            return new BoxEditViewModel
            {
                Id = model.Id,
                Address = model.Address,
                Content = model.Content,
                Icon = model.Icon,
                Language = model.Language,
                Link = model.Link,
                Priority = model.Priority,
                Title = model.Title
            };
        }

        public async Task<bool> Edit(BoxEditViewModel viewModel)
        {
            var model = await db.Boxes.FindAsync(viewModel.Id);
            if (model == null)
            {
                return false;
            }
            model.Content = viewModel.Content;
            model.Icon = viewModel.Icon;
            model.Language = viewModel.Language;
            model.Link = viewModel.Link;
            model.Priority = viewModel.Priority;
            model.Title = viewModel.Title;
            model.Update = DateTime.Now;
            db.Entry(model).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int boxId)
        {
            if (HttpContext.Current.User.IsInRole("Developer"))
            {
                var model = await db.Boxes.FindAsync(boxId);
                if (model == null)
                {
                    return false;
                }
                model.IsDeleted = true;
                model.Update = DateTime.Now;
                db.Entry(model).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}