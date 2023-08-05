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
    public interface ITag
    {
        Task<List<TagListViewModel>> ReadAll();
        Task<TagDetailsPageViewModel> ReadById(int tagId);
        Task<int> Create(TagCreateViewModel viewModel);
        Task<TagEditViewModel> ReadByIdForEdit(int tagId);
        Task<bool> Edit(TagEditViewModel viewModel);
        Task<bool> Delete(int tagId);
        Task<List<SelectListItem>> ReadAllByGroupForSelect(string group);
        List<SelectListItem> ReadAllTagsNameByIds(string ids);
        Task<List<SelectListItem>> ReadAllTagsNameByIdsAsync(string ids);
    }
    public class TagService : ITag
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public async Task<List<TagListViewModel>> ReadAll()
        {
            BaseService baseService = new BaseService();
            var userDetails = baseService.ReadUserDetails();
            var Profiles = await db.Profiles.ToListAsync();
            var model = await db.Tags.Where(x => !x.IsDeleted && (userDetails.Role == "Administrator" || userDetails.UserId == x.UserId)).OrderByDescending(x => x.Id).ToListAsync();
            return model.Select(x => new TagListViewModel
            {
                Id = x.Id,
                Group = TagHelper.ReadGroupByValue(x.Group),
                Language = LanguageHelper.ReadLanguageByValue(x.Language),
                Review = CountHelper.CalculateCount(x.Review),
                Title = x.Title,
                Update = DateTimeHelper.ConvertToShamsi(x.Update),
                UserId = x.UserId,
                UserName = Profiles.Where(z => z.Id == x.UserId).Select(z => z.Name).FirstOrDefault()
            }).ToList();
        }

        public async Task<TagDetailsPageViewModel> ReadById(int tagId)
        {
            var model = await db.Tags.FindAsync(tagId);
            if (model == null || model.IsDeleted)
            {
                return null;
            }
            model.Review++;
            db.Entry(model).State = EntityState.Modified;
            await db.SaveChangesAsync();
            TagDetailsViewModel tag = new TagDetailsViewModel
            {
                Id = model.Id,
                Group = TagHelper.ReadGroupByValue(model.Group),
                Description = model.Description,
                OptimizationDescription = model.OptimizationDescription,
                OptimizationKeywords = model.OptimizationKeywords,
                OptimizationTitle = model.OptimizationTitle,
                Review = CountHelper.CalculateCount(model.Review),
                Title = model.Title
            };
            return new TagDetailsPageViewModel
            {
                Tag = tag
            };
        }

        public async Task<int> Create(TagCreateViewModel viewModel)
        {
            var now = DateTime.Now;
            TagModel model = new TagModel
            {
                Group = viewModel.Group,
                Date = now,
                Description = viewModel.Description,
                IsDeleted = false,
                Language = viewModel.Language,
                OptimizationDescription = viewModel.OptimizationDescription,
                OptimizationKeywords = viewModel.OptimizationKeywords,
                OptimizationTitle = viewModel.OptimizationTitle,
                Review = 0,
                Title = viewModel.Title,
                Update = now,
                UserId = HttpContext.Current.User.Identity.GetUserId()
            };
            db.Tags.Add(model);
            await db.SaveChangesAsync();
            return model.Id;
        }

        public async Task<TagEditViewModel> ReadByIdForEdit(int tagId)
        {
            BaseService baseService = new BaseService();
            var userDetails = baseService.ReadUserDetails();
            var model = await db.Tags.FindAsync(tagId);
            if (model == null || (userDetails.Role != "Administrator" && userDetails.UserId != model.UserId))
            {
                return null;
            }
            return new TagEditViewModel
            {
                Id = model.Id,
                Group = model.Group,
                Description = model.Description,
                Language = model.Language,
                OptimizationDescription = model.OptimizationDescription,
                OptimizationKeywords = model.OptimizationKeywords,
                OptimizationTitle = model.OptimizationTitle,
                Title = model.Title
            };
        }

        public async Task<bool> Edit(TagEditViewModel viewModel)
        {
            BaseService baseService = new BaseService();
            var userDetails = baseService.ReadUserDetails();
            var model = await db.Tags.FindAsync(viewModel.Id);
            if (model == null || (userDetails.Role != "Administrator" && userDetails.UserId != model.UserId))
            {
                return false;
            }
            model.Group = viewModel.Group;
            model.Description = viewModel.Description;
            model.Language = viewModel.Language;
            model.OptimizationDescription = viewModel.OptimizationDescription;
            model.OptimizationKeywords = viewModel.OptimizationKeywords;
            model.OptimizationTitle = viewModel.OptimizationTitle;
            model.Title = viewModel.Title;
            model.Update = DateTime.Now;
            db.Entry(model).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int tagId)
        {
            BaseService baseService = new BaseService();
            var userDetails = baseService.ReadUserDetails();
            var model = await db.Tags.FindAsync(tagId);
            if (model == null || (userDetails.Role != "Administrator" && userDetails.UserId != model.UserId))
            {
                return false;
            }
            model.IsDeleted = true;
            db.Entry(model).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<List<SelectListItem>> ReadAllByGroupForSelect(string group)
        {
            return await db.Tags.Where(x => !x.IsDeleted && x.Group != null && x.Group == group).OrderBy(x => x.Title).Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Title
            }).ToListAsync();
        }

        public List<SelectListItem> ReadAllTagsNameByIds(string ids)
        {
            List<SelectListItem> tags = new List<SelectListItem>();
            if (ids != null)
            {
                var Tags = db.Tags.ToList();
                var Ids = ids.Split(',');
                foreach (var item in Ids)
                {
                    SelectListItem tag = new SelectListItem
                    {
                        Value = item,
                        Text = Tags.Where(x => x.Id.ToString() == item).Select(x => x.Title).FirstOrDefault()
                    };
                    tags.Add(tag);
                }
            }
            return tags;
        }

        public async Task<List<SelectListItem>> ReadAllTagsNameByIdsAsync(string ids)
        {
            List<SelectListItem> tags = new List<SelectListItem>();
            if (ids != null)
            {
                var Tags = await db.Tags.ToListAsync();
                var Ids = ids.Split(',');
                foreach (var item in Ids)
                {
                    SelectListItem tag = new SelectListItem
                    {
                        Value = item,
                        Text = Tags.Where(x => x.Id.ToString() == item).Select(x => x.Title).FirstOrDefault()
                    };
                    tags.Add(tag);
                }
            }
            return tags;
        }
    }
}