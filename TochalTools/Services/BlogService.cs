using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TochalTools.Classes;
using TochalTools.Models;
using PagedList;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace TochalTools.Services
{
    public interface IBlog
    {
        Task<BlogIndexPageViewModel> ReadAllForIndex(string category, string search, int page, int pageSize);
        Task<List<BlogListViewModel>> ReadAll();
        Task<BlogDetailsPageViewModel> ReadById(int blogId, int popularSize, int similarSize);
        Task<int> Create(BlogCreateViewModel viewModel);
        Task<BlogEditViewModel> ReadByIdForEdit(int blogId);
        Task<bool> Edit(BlogEditViewModel viewModel);
        Task<bool> Delete(int blogId);
    }
    public class BlogService : IBlog
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public async Task<BlogIndexPageViewModel> ReadAllForIndex(string category, string search, int page, int pageSize)
        {
            TagService tagService = new TagService();
            var Comments = await db.Comments.Where(x => !x.IsDeleted && x.IsConfirmed && x.Group == "Blog").ToListAsync();
            var model = await db.Blogs.Where(x => !x.IsDeleted && x.IsPublicationActive && x.Language == "fa" && (search == null || x.Title.Contains(search) || x.ShortContent.Contains(search) || x.FullContent.Contains(search))).OrderByDescending(x => x.Id).ToListAsync();
            var Category = new SelectListItem { Value = "Default", Text = "بدون دسته بندی" };
            if (category != null)
            {
                CategoryService categoryService = new CategoryService();
                var categoryIds = await categoryService.ReadCategoriesIdByName("Blog", category);
                model = model.Where(x => x.Categories != null && x.Categories.Contains(categoryIds)).ToList();
                Category.Value = await categoryService.ReadIdByName("Blog", category);
                Category.Text = category;
            }
            else if (search != null)
            {
                Category.Text = search;
            }
            var PageCount = Convert.ToDecimal(model.Count()) / Convert.ToDecimal(pageSize);
            var pageCount = Convert.ToInt32(Math.Ceiling(PageCount));
            var blogs = model.Select(x => new BlogIndexViewModel
            {
                Id = x.Id,
                Date = DateTimeHelper.ConvertToShamsi(x.Date),
                CommentCount = Comments.Where(z => z.RefId == x.Id.ToString()).Count(),
                Review = CountHelper.CalculateCount(x.Review),
                ShortContent = x.ShortContent,
                Title = x.Title,
                Tags = tagService.ReadAllTagsNameByIds(x.Tags)
        }).ToPagedList(page, pageSize);
            return new BlogIndexPageViewModel
            {
                Blogs = blogs,
                MainCategory = (category != null) ? category : "",
                Category = Category,
                Page = page,
                PageCount = pageCount,
                Search = (search != null) ? search : ""
            };
        }

        public async Task<List<BlogListViewModel>> ReadAll()
        {
            BaseService baseService = new BaseService();
            var userDetails = baseService.ReadUserDetails();
            var model = await db.Blogs.Where(x => !x.IsDeleted && (userDetails.Role == "Administrator" || userDetails.UserId == x.UserId)).OrderByDescending(x => x.Id).ToListAsync();
            var Profiles = await db.Profiles.ToListAsync();
            return model.Select(x => new BlogListViewModel
            {
                Id = x.Id,
                IsAtHomeActive = (x.IsAtHomeActive) ? "checked" : "",
                IsPublicationActive = (x.IsPublicationActive) ? "checked" : "",
                Language = LanguageHelper.ReadLanguageByValue(x.Language),
                Review = CountHelper.CalculateCount(x.Review),
                Title = x.Title,
                Update = DateTimeHelper.ConvertToShamsi(x.Update),
                UserId = x.UserId,
                UserName = Profiles.Where(z => z.Id == x.UserId).Select(z => z.Name).FirstOrDefault()
            }).ToList();
        }

        public async Task<BlogDetailsPageViewModel> ReadById(int blogId, int popularSize, int similarSize)
        {
            var model = await db.Blogs.FindAsync(blogId);
            if (model == null || model.IsDeleted || !model.IsPublicationActive)
            {
                return null;
            }
            model.Review++;
            db.Entry(model).State = EntityState.Modified;
            await db.SaveChangesAsync();
            CategoryService categoryService = new CategoryService();
            TagService tagService = new TagService();
            CommentService commentService = new CommentService();
            var Comments = await db.Comments.Where(x => !x.IsDeleted && x.IsConfirmed && x.Group == "Blog").ToListAsync();
            var tags = await tagService.ReadAllTagsNameByIdsAsync(model.Tags);
            var blog = new BlogDetailsViewModel
            {
                Id = model.Id,
                CommentCount = Comments.Where(x => x.RefId == blogId.ToString()).Count(),
                Date = DateTimeHelper.ConvertToShamsi(model.Date),
                FullContent = model.FullContent,
                IsCommentsActive = model.IsCommentsActive,
                OptimizationDescription = model.OptimizationDescription,
                OptimizationKeywords = model.OptimizationKeywords,
                OptimizationTitle = model.OptimizationTitle,
                Review = CountHelper.CalculateCount(model.Review),
                Tags = tags,
                Title = model.Title,
            };
            blog.Comments = await commentService.ReadAllByGroupAndRefId("Blog", blogId.ToString());
            var categories = await categoryService.ReadAllByGroupForSelect("Blog", true, true);
            var PopularBlogs = await db.Blogs.Where(x => !x.IsDeleted && x.IsPublicationActive && x.Language == "fa" && x.Review > 0).OrderByDescending(x => x.Review).Take(popularSize).ToListAsync();
            var popularBlogs = PopularBlogs.Select(x => new BlogIndexViewModel
            {
                Id = x.Id,
                CommentCount = Comments.Where(z => z.RefId == x.Id.ToString()).Count(),
                Review = CountHelper.CalculateCount(x.Review),
                Title = x.Title
            }).ToList();

            var SimilarBlogs = await db.Blogs.Where(x => !x.IsDeleted && x.IsPublicationActive && x.Language == "fa" && x.Categories != null && x.Categories.Contains(model.Categories) && x.Id != blogId).OrderBy(x => Guid.NewGuid()).Take(similarSize).ToListAsync();
            var similarBlogs = SimilarBlogs.Select(x => new BlogIndexViewModel
            {
                Id = x.Id,
                CommentCount = Comments.Where(z => z.RefId == x.Id.ToString()).Count(),
                Review = CountHelper.CalculateCount(x.Review),
                Title = x.Title
            }).ToList();
            return new BlogDetailsPageViewModel
            {
                Blog = blog,
                Categories = categories,
                PopularBlogs = popularBlogs,
                SimilarBlogs = similarBlogs
            };
        }

        public async Task<int> Create(BlogCreateViewModel viewModel)
        {
            var now = DateTime.Now;
            BlogModel model = new BlogModel
            {
                Categories = viewModel.Categories,
                Date = now,
                FullContent = viewModel.FullContent,
                IsAtHomeActive = viewModel.IsAtHomeActive,
                IsCommentsActive = viewModel.IsCommentsActive,
                IsDeleted = false,
                IsPublicationActive = viewModel.IsPublicationActive,
                Language = viewModel.Language,
                OptimizationDescription = viewModel.OptimizationDescription,
                OptimizationKeywords = viewModel.OptimizationKeywords,
                OptimizationTitle = viewModel.OptimizationTitle,
                Review = 0,
                ShortContent = viewModel.ShortContent,
                Tags = viewModel.Tags,
                Title = viewModel.Title,
                Update = now,
                UserId = HttpContext.Current.User.Identity.GetUserId()
            };
            db.Blogs.Add(model);
            await db.SaveChangesAsync();
            return model.Id;
        }

        public async Task<BlogEditViewModel> ReadByIdForEdit(int blogId)
        {
            BaseService baseService = new BaseService();
            var userDetails = baseService.ReadUserDetails();
            var model = await db.Blogs.FindAsync(blogId);
            if (model == null || (userDetails.Role != "Administrator" && userDetails.UserId != model.UserId))
            {
                return null;
            }
            CategoryService categoryService = new CategoryService();
            TagService tagService = new TagService();
            return new BlogEditViewModel
            {
                Id = model.Id,
                Categories = model.Categories,
                FullContent = model.FullContent,
                IsAtHomeActive = model.IsAtHomeActive,
                IsCommentsActive = model.IsCommentsActive,
                IsPublicationActive = model.IsPublicationActive,
                Language = model.Language,
                OptimizationDescription = model.OptimizationDescription,
                OptimizationKeywords = model.OptimizationKeywords,
                OptimizationTitle = model.OptimizationTitle,
                ShortContent = model.ShortContent,
                Tags = model.Tags,
                Title = model.Title,
                CategoriesList = await categoryService.ReadAllByGroupForSelect("Blog"),
                TagsList = await tagService.ReadAllByGroupForSelect("Blog")
            };
        }

        public async Task<bool> Edit(BlogEditViewModel viewModel)
        {
            BaseService baseService = new BaseService();
            var userDetails = baseService.ReadUserDetails();
            var model = await db.Blogs.FindAsync(viewModel.Id);
            if (model == null || (userDetails.Role != "Administrator" && userDetails.UserId != model.UserId))
            {
                return false;
            }
            model.Categories = viewModel.Categories;
            model.FullContent = viewModel.FullContent;
            model.IsAtHomeActive = viewModel.IsAtHomeActive;
            model.IsCommentsActive = viewModel.IsCommentsActive;
            model.IsPublicationActive = viewModel.IsPublicationActive;
            model.Language = viewModel.Language;
            model.OptimizationDescription = viewModel.OptimizationDescription;
            model.OptimizationKeywords = viewModel.OptimizationKeywords;
            model.OptimizationTitle = viewModel.OptimizationTitle;
            model.ShortContent = viewModel.ShortContent;
            model.Tags = viewModel.Tags;
            model.Title = viewModel.Title;
            model.Update = DateTime.Now;
            db.Entry(model).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int blogId)
        {
            BaseService baseService = new BaseService();
            var userDetails = baseService.ReadUserDetails();
            var model = await db.Blogs.FindAsync(blogId);
            if (model == null || (userDetails.Role != "Administrator" && userDetails.UserId != model.UserId))
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