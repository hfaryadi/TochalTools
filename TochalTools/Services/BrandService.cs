using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TochalTools.Classes;
using TochalTools.Models;

namespace TochalTools.Services
{
    public interface IBrand
    {
        Task<List<BrandIndexViewModel>> ReadAllForIndex(bool? includeHomeActive = null);
        Task<List<BrandListViewModel>> ReadAll();
        Task<int> Create(BrandCreateViewModel viewModel);
        Task<BrandEditViewModel> ReadByIdForEdit(int brandId);
        Task<bool> Edit(BrandEditViewModel viewModel);
        Task<bool> Delete(int brandId);
        Task<List<SelectListItem>> ReadAllForSelect();
        Task<string> ReadTitleById(int brandId);
    }
    public class BrandService : IBrand
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public async Task<List<BrandIndexViewModel>> ReadAllForIndex(bool? includeHomeActive = null)
        {
            return await db.Brands.Where(x => !x.IsDeleted && x.Language == "fa" && (includeHomeActive == null || (x.IsAtHomeActive == includeHomeActive))).OrderByDescending(x => x.Id).Select(x => new BrandIndexViewModel
            {
                Id = x.Id,
                Title = x.Title
            }).ToListAsync();
        }

        public async Task<List<BrandListViewModel>> ReadAll()
        {
            BaseService baseService = new BaseService();
            var userDetails = baseService.ReadUserDetails();
            var model = await db.Brands.Where(x => !x.IsDeleted && (userDetails.Role == "Administrator" || userDetails.UserId == x.UserId)).OrderByDescending(x => x.Id).ToListAsync();
            return model.Select(x => new BrandListViewModel
            {
                Id = x.Id,
                Date = DateTimeHelper.ConvertToShamsi(x.Date),
                IsAtHomeActive = (x.IsAtHomeActive) ? "checked" : "",
                Language = LanguageHelper.ReadLanguageByValue(x.Language),
                Title = x.Title
            }).ToList();
        }

        public async Task<int> Create(BrandCreateViewModel viewModel)
        {
            BrandModel model = new BrandModel
            {
                Date = DateTime.Now,
                Description = viewModel.Description,
                IsAtHomeActive = viewModel.IsAtHomeActive,
                IsDeleted = false,
                Language = viewModel.Language,
                Title = viewModel.Title,
                UserId = HttpContext.Current.User.Identity.GetUserId()
            };
            db.Brands.Add(model);
            await db.SaveChangesAsync();
            return model.Id;
        }

        public async Task<BrandEditViewModel> ReadByIdForEdit(int brandId)
        {
            BaseService baseService = new BaseService();
            var userDetails = baseService.ReadUserDetails();
            var model = await db.Brands.FindAsync(brandId);
            if (model == null || (userDetails.Role != "Administrator" && userDetails.UserId != model.UserId))
            {
                return null;
            }
            return new BrandEditViewModel
            {
                Id = model.Id,
                Description = model.Description,
                IsAtHomeActive = model.IsAtHomeActive,
                Language = model.Language,
                Title = model.Title
            };
        }

        public async Task<bool> Edit(BrandEditViewModel viewModel)
        {
            BaseService baseService = new BaseService();
            var userDetails = baseService.ReadUserDetails();
            var model = await db.Brands.FindAsync(viewModel.Id);
            if (model == null || (userDetails.Role != "Administrator" && userDetails.UserId != model.UserId))
            {
                return false;
            }
            model.Description = viewModel.Description;
            model.IsAtHomeActive = viewModel.IsAtHomeActive;
            model.Language = viewModel.Language;
            model.Title = viewModel.Title;
            db.Entry(model).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int brandId)
        {
            BaseService baseService = new BaseService();
            var userDetails = baseService.ReadUserDetails();
            var model = await db.Brands.FindAsync(brandId);
            if (model == null || (userDetails.Role != "Administrator" && userDetails.UserId != model.UserId))
            {
                return false;
            }
            model.IsDeleted = true;
            db.Entry(model).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<List<SelectListItem>> ReadAllForSelect()
        {
            return await db.Brands.Where(x => !x.IsDeleted).OrderBy(x => x.Title).Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Title
            }).ToListAsync();
        }

        public async Task<string> ReadTitleById(int brandId)
        {
            return await db.Brands.Where(x => !x.IsDeleted && brandId > 0 && x.Id == brandId).Select(x => x.Title).FirstOrDefaultAsync();
        }
    }
}