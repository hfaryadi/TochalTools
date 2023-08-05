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
    public interface IPage
    {
        Task<List<PageListViewModel>> ReadAll();
        Task<PageDetailsViewModel> ReadByTitle(string title);
        Task<int> Create(PageCreateViewModel viewModel);
        Task<PageEditViewModel> ReadByIdForEdit(int pageId);
        Task<bool> Edit(PageEditViewModel viewModel);
        Task<bool> Delete(int pageId);
    }
    public class PageService : IPage
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public async Task<List<PageListViewModel>> ReadAll()
        {
            var model = await db.Pages.Where(x => !x.IsDeleted && x.Language == "fa").OrderByDescending(x => x.Id).ToListAsync();
            return model.Select(x => new PageListViewModel
            {
                Id = x.Id,
                Date = DateTimeHelper.ConvertToShamsi(x.Date),
                Language = LanguageHelper.ReadLanguageByValue(x.Language),
                Title = x.Title
            }).ToList();
        }

        public async Task<PageDetailsViewModel> ReadByTitle(string title)
        {
            var model = await db.Pages.Where(x => !x.IsDeleted && x.Language == "fa" && x.Title == title).FirstOrDefaultAsync();
            if (model == null)
            {
                return null;
            }
            return new PageDetailsViewModel
            {
                Id = model.Id,
                Content = model.Content,
                OptimizationDescription = model.OptimizationDescription,
                OptimizationKeywords = model.OptimizationKeywords,
                OptimizationTitle = model.OptimizationTitle,
                Title = model.Title
            };
        }

        public async Task<int> Create(PageCreateViewModel viewModel)
        {
            var page = await db.Pages.Where(x => !x.IsDeleted && x.Language == "fa" && x.Title == viewModel.Title).FirstOrDefaultAsync();
            if (page != null)
            {
                return 0;
            }
            PageModel model = new PageModel
            {
                Content = viewModel.Content,
                Date = DateTime.Now,
                IsDeleted = false,
                Language = viewModel.Language,
                OptimizationDescription = viewModel.OptimizationDescription,
                OptimizationKeywords = viewModel.OptimizationKeywords,
                OptimizationTitle = viewModel.OptimizationTitle,
                Title = viewModel.Title
            };
            db.Pages.Add(model);
            await db.SaveChangesAsync();
            return model.Id;
        }

        public async Task<PageEditViewModel> ReadByIdForEdit(int pageId)
        {
            var model = await db.Pages.Where(x => x.Id == pageId).FirstOrDefaultAsync();
            if (model == null)
            {
                return null;
            }
            return new PageEditViewModel
            {
                Id = model.Id,
                Content = model.Content,
                Language = model.Language,
                OptimizationDescription = model.OptimizationDescription,
                OptimizationKeywords = model.OptimizationKeywords,
                OptimizationTitle = model.OptimizationTitle,
                Title = model.Title
            };
        }

        public async Task<bool> Edit(PageEditViewModel viewModel)
        {
            var page = await db.Pages.Where(x => !x.IsDeleted && x.Language == "fa" && x.Title == viewModel.Title && x.Id != viewModel.Id).FirstOrDefaultAsync();
            var model = await db.Pages.FindAsync(viewModel.Id);
            if (page != null || model == null)
            {
                return false;
            }
            model.Content = viewModel.Content;
            model.Language = viewModel.Language;
            model.OptimizationDescription = viewModel.OptimizationDescription;
            model.OptimizationKeywords = viewModel.OptimizationKeywords;
            model.OptimizationTitle = viewModel.OptimizationTitle;
            model.Title = viewModel.Title;
            db.Entry(model).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int pageId)
        {
            var model = await db.Pages.FindAsync(pageId);
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