using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TochalTools.Models
{
    public class BlogIndexViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortContent { get; set; }
        public string Review { get; set; }
        public int CommentCount { get; set; }
        public List<SelectListItem> Tags { get; set; }
        public string Date { get; set; }
    }

    public class BlogListViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Language { get; set; }
        public string Review { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string IsPublicationActive { get; set; }
        public string IsAtHomeActive { get; set; }
        public string Update { get; set; }
    }

    public class BlogDetailsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string FullContent { get; set; }
        public string Review { get; set; }
        public int CommentCount { get; set; }
        public string OptimizationTitle { get; set; }
        public string OptimizationKeywords { get; set; }
        public string OptimizationDescription { get; set; }
        public bool IsCommentsActive { get; set; }
        public string Date { get; set; }
        public List<CommentIndexViewModel> Comments { get; set; }
        public List<SelectListItem> Tags { get; set; }
    }

    public class BlogCreateViewModel
    {
        [Display(Name = "عنوان مطلب *")]
        [Required(ErrorMessage = "لطفا عنوان مطلب را وارد نمایید.")]
        public string Title { get; set; }
        [AllowHtml]
        [Display(Name = "متن کوتاه *")]
        [Required(ErrorMessage = "لطفا متن کوتاه را وارد نمایید.")]
        public string ShortContent { get; set; }
        [AllowHtml]
        [Display(Name = "متن کامل *")]
        [Required(ErrorMessage = "لطفا متن کامل را وارد نمایید.")]
        public string FullContent { get; set; }
        [Display(Name = "دسته بندی *")]
        [Required(ErrorMessage = "لطفا دسته بندی را انتخاب نمایید.")]
        public string Categories { get; set; }
        [Display(Name = "برچسب")]
        public string Tags { get; set; }
        [Display(Name = "زبان *")]
        [Required(ErrorMessage = "لطفا زبان را انتخاب نمایید.")]
        public string Language { get; set; }
        [Display(Name = "عنوان")]
        public string OptimizationTitle { get; set; }
        [Display(Name = "کلمات کلیدی")]
        public string OptimizationKeywords { get; set; }
        [Display(Name = "توضیح مختصر")]
        public string OptimizationDescription { get; set; }
        [Display(Name = "انتشار")]
        public bool IsPublicationActive { get; set; }
        [Display(Name = "نمایش در صفحه نخست")]
        public bool IsAtHomeActive { get; set; }
        [Display(Name = "نمایش نظرات")]
        public bool IsCommentsActive { get; set; }
        public List<SelectListItem> CategoriesList { get; set; }
        public List<SelectListItem> TagsList { get; set; }
    }

    public class BlogEditViewModel
    {
        [Display(Name = "شناسه")]
        public int Id { get; set; }
        [Display(Name = "عنوان مطلب *")]
        [Required(ErrorMessage = "لطفا عنوان مطلب را وارد نمایید.")]
        public string Title { get; set; }
        [AllowHtml]
        [Display(Name = "متن کوتاه *")]
        [Required(ErrorMessage = "لطفا متن کوتاه را وارد نمایید.")]
        public string ShortContent { get; set; }
        [AllowHtml]
        [Display(Name = "متن کامل *")]
        [Required(ErrorMessage = "لطفا متن کامل را وارد نمایید.")]
        public string FullContent { get; set; }
        [Display(Name = "دسته بندی *")]
        [Required(ErrorMessage = "لطفا دسته بندی را انتخاب نمایید.")]
        public string Categories { get; set; }
        [Display(Name = "برچسب")]
        public string Tags { get; set; }
        [Display(Name = "زبان *")]
        [Required(ErrorMessage = "لطفا زبان را انتخاب نمایید.")]
        public string Language { get; set; }
        [Display(Name = "عنوان")]
        public string OptimizationTitle { get; set; }
        [Display(Name = "کلمات کلیدی")]
        public string OptimizationKeywords { get; set; }
        [Display(Name = "توضیح مختصر")]
        public string OptimizationDescription { get; set; }
        [Display(Name = "انتشار")]
        public bool IsPublicationActive { get; set; }
        [Display(Name = "نمایش در صفحه نخست")]
        public bool IsAtHomeActive { get; set; }
        [Display(Name = "نمایش نظرات")]
        public bool IsCommentsActive { get; set; }
        public List<SelectListItem> CategoriesList { get; set; }
        public List<SelectListItem> TagsList { get; set; }
    }

    public class BlogIndexPageViewModel
    {
        public IEnumerable<BlogIndexViewModel> Blogs { get; set; }
        public string MainCategory { get; set; }
        public SelectListItem Category { get; set; }
        public string Search { get; set; }
        public int PageCount { get; set; }
        public int Page { get; set; }
    }

    public class BlogDetailsPageViewModel
    {
        public BlogDetailsViewModel Blog { get; set; }
        public List<SelectListItem> Categories { get; set; }
        public List<BlogIndexViewModel> SimilarBlogs { get; set; }
        public List<BlogIndexViewModel> PopularBlogs { get; set; }
    }
}