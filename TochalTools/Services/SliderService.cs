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
    public interface ISlider
    {
        Task<List<SliderIndexViewModel>> ReadAllForIndex();
        Task<List<SliderListViewModel>> ReadAll();
        Task<int> Create(SliderCreateViewModel viewModel);
        Task<SliderEditViewModel> ReadByIdForEdit(int sliderId);
        Task<bool> Edit(SliderEditViewModel viewModel);
        Task<bool> Delete(int sliderId);
    }
    public class SliderService : ISlider
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public async Task<List<SliderIndexViewModel>> ReadAllForIndex()
        {
            return await db.Sliders.Where(x => !x.IsDeleted && x.Language == "fa").OrderBy(x => x.Priority).Select(x => new SliderIndexViewModel
            {
                Id = x.Id,
                Description = x.Description,
                Title = x.Title,
                Link = x.Link
            }).ToListAsync();
        }

        public async Task<List<SliderListViewModel>> ReadAll()
        {
            var model = await db.Sliders.Where(x => !x.IsDeleted).OrderBy(x => x.Priority).ToListAsync();
            return model.Select(x => new SliderListViewModel
            {
                Id = x.Id,
                Language = LanguageHelper.ReadLanguageByValue(x.Language),
                Priority = x.Priority,
                Title = x.Title,
                Update = DateTimeHelper.ConvertToShamsi(x.Update),
                Link = x.Link
            }).ToList();
        }

        public async Task<int> Create(SliderCreateViewModel viewModel)
        {
            var now = DateTime.Now;
            SliderModel model = new SliderModel
            {
                Date = now,
                Description = viewModel.Description,
                IsDeleted = false,
                Language = viewModel.Language,
                Priority = viewModel.Priority,
                Title = viewModel.Title,
                Update = now,
                Link = viewModel.Link
            };
            db.Sliders.Add(model);
            await db.SaveChangesAsync();
            return model.Id;
        }

        public async Task<SliderEditViewModel> ReadByIdForEdit(int sliderId)
        {
            var model = await db.Sliders.FindAsync(sliderId);
            if (model == null)
            {
                return null;
            }
            return new SliderEditViewModel
            {
                Id = model.Id,
                Description = model.Description,
                Language = model.Language,
                Priority = model.Priority,
                Title = model.Title,
                Link = model.Link
            };
        }

        public async Task<bool> Edit(SliderEditViewModel viewModel)
        {
            var model = await db.Sliders.FindAsync(viewModel.Id);
            if (model == null)
            {
                return false;
            }
            model.Description = viewModel.Description;
            model.Language = viewModel.Language;
            model.Priority = viewModel.Priority;
            model.Title = viewModel.Title;
            model.Link = viewModel.Link;
            model.Update = DateTime.Now;
            db.Entry(model).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int sliderId)
        {
            var model = await db.Sliders.FindAsync(sliderId);
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
    }
}