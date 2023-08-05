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
    public interface ICategory
    {
        Task<List<CategoryListViewModel>> ReadAll(string group);
        Task<string> Create(CategoryCreateViewModel viewModel);
        Task<CategoryEditViewModel> ReadByIdForEdit(string categoryId);
        Task Edit(CategoryEditViewModel viewModel);
        Task<bool> Delete(string categoryId);
        Task<List<SelectListItem>> ReadAllByGroupForSelect(string group, bool? isParents = null, bool? isPublication = null);
        Task<List<SelectListItem>> ReadAllByGroupAndParentForSelect(string group, string parent);
        Task<string> ReadCategoriesIdByName(string group, string name);
        Task<List<SelectListItem>> ReadAllCategoriesNameByIds(string ids);
        Task<string> ReadIdByName(string group, string name);
    }
    public class CategoryService : ICategory
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public async Task<List<CategoryListViewModel>> ReadAll(string group)
        {
            var model = await db.Categories.Where(x => !x.IsDeleted && x.Group == group).OrderBy(x => x.Name).ToListAsync();
            var Categories = await db.Categories.ToListAsync();
            var viewModel = model.Select(x => new CategoryListViewModel
            {
                Id = x.Id,
                IsPublicationActive = (x.IsPublicationActive) ? "checked" : "",
                Language = LanguageHelper.ReadLanguageByValue(x.Language),
                Name = x.Name,
                Parents = x.Parents,
            }).ToList();
            foreach (var item in viewModel)
            {
                var parents = "";
                if (item.Parents != null)
                {
                    var prnts = item.Parents.Split(',');
                    foreach (var jtem in prnts)
                    {
                        var parentName = Categories.Where(x => x.Id == jtem).Select(x => x.Name).FirstOrDefault();
                        if (parentName != null)
                        {
                            parents = parents + parentName + ", ";
                        }
                    }
                }
                if (parents != "")
                {
                    parents = parents.Remove(parents.Length - 2, 2);
                }
                item.Parents = parents;
            }
            return viewModel;
        }

        public async Task<string> Create(CategoryCreateViewModel viewModel)
        {
            var category = await db.Categories.Where(x => !x.IsDeleted && x.Name == viewModel.Name).FirstOrDefaultAsync();
            if (category == null)
            {
                CategoryModel model = new CategoryModel
                {
                    Id = Guid.NewGuid().ToString(),
                    Date = DateTime.Now,
                    Description = viewModel.Description,
                    Group = viewModel.Group,
                    IsDeleted = false,
                    IsPublicationActive = viewModel.IsPublicationActive,
                    Language = viewModel.Language,
                    Name = viewModel.Name,
                    Parents = viewModel.Parents
                };
                db.Categories.Add(model);
                await db.SaveChangesAsync();
                return model.Id;
            }
            return "Duplicate";
        }

        public async Task<CategoryEditViewModel> ReadByIdForEdit(string categoryId)
        {
            var model = await db.Categories.FindAsync(categoryId);
            return new CategoryEditViewModel
            {
                Id = model.Id,
                Description = model.Description,
                Group = model.Group,
                IsPublicationActive = model.IsPublicationActive,
                Language = model.Language,
                Name = model.Name,
                Parents = model.Parents,
                CategoriesList = await ReadAllByGroupForSelect(model.Group)
            };
        }

        public async Task Edit(CategoryEditViewModel viewModel)
        {
            var category = await db.Categories.Where(x => !x.IsDeleted && x.Id != viewModel.Id && x.Name == viewModel.Name).FirstOrDefaultAsync();
            var model = await db.Categories.FindAsync(viewModel.Id);
            model.Description = viewModel.Description;
            model.IsPublicationActive = viewModel.IsPublicationActive;
            model.Language = viewModel.Language;
            if (category == null)
            {
                model.Name = viewModel.Name;
            }
            model.Parents = viewModel.Parents;
            db.Entry(model).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

        public async Task<bool> Delete(string categoryId)
        {
            var model = await db.Categories.FindAsync(categoryId);
            if (model == null)
            {
                return false;
            }
            model.IsDeleted = true;
            db.Entry(model).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<List<SelectListItem>> ReadAllByGroupForSelect(string group, bool? isParents = null, bool? isPublication = null)
        {
            return await db.Categories.Where(x => !x.IsDeleted && x.Group == group && (isParents == null || (isParents == true && (x.Parents == null || x.Parents == "")) || (isParents == false && x.Parents != null && x.Parents != "")) && (isPublication == null || x.IsPublicationActive == isPublication)).OrderBy(x => x.Name).Select(x => new SelectListItem
            {
                Value = x.Id,
                Text = x.Name
            }).ToListAsync();
        }

        public async Task<List<SelectListItem>> ReadAllByGroupAndParentForSelect(string group, string parent)
        {
            return await db.Categories.Where(x => !x.IsDeleted && x.Group == group && x.Parents != null && x.Parents.Contains(parent)).OrderBy(x => x.Name).Select(x => new SelectListItem
            {
                Value = x.Id,
                Text = x.Name
            }).ToListAsync();
        }

        public async Task<string> ReadCategoriesIdByName(string group, string name)
        {
            var Categories = await db.Categories.Where(x => x.IsPublicationActive && x.Group == group).ToListAsync();
            var CategoryIds = "";
            var categories = name.Split(',');
            foreach (var item in categories)
            {
                var id = Categories.Where(x => x.Name == item).Select(x => x.Id).FirstOrDefault();
                if (id != null)
                {
                    CategoryIds = CategoryIds + id + ",";
                }
            }
            if (CategoryIds != "")
            {
                CategoryIds = CategoryIds.Remove(CategoryIds.Length - 1, 1);
            }
            return CategoryIds;
        }

        public async Task<List<SelectListItem>> ReadAllCategoriesNameByIds(string ids)
        {
            List<SelectListItem> categories = new List<SelectListItem>();
            if (ids != null)
            {
                var Categories = await db.Categories.ToListAsync();
                var Ids = ids.Split(',');
                foreach (var item in Ids)
                {
                    SelectListItem category = new SelectListItem
                    {
                        Value = item,
                        Text = Categories.Where(x => x.Id == item).Select(x => x.Name).FirstOrDefault()
                    };
                    categories.Add(category);
                }
            }
            return categories;
        }

        public async Task<string> ReadIdByName(string group, string name)
        {
            return await db.Categories.Where(x => !x.IsDeleted && x.Group == group && x.Name == name).Select(x => x.Id).FirstOrDefaultAsync();
        }
    }
}