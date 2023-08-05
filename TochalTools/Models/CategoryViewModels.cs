using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TochalTools.Models
{
    public class CategoryIndexViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<CategoryIndexViewModel> Childs { get; set; }
    }
    public class CategoryListViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Parents { get; set; }
        public string Language { get; set; }
        public string IsPublicationActive { get; set; }
    }

    public class CategoryCreateViewModel
    {
        [Display(Name = "نام دسته بندی *")]
        [Required(ErrorMessage = "لطفا نام دسته بندی را وارد نمایید.")]
        public string Name { get; set; }
        [Display(Name = "دسته بندی")]
        public string Parents { get; set; }
        [Display(Name = "توضیح")]
        public string Description { get; set; }
        public string Group { get; set; }
        [Display(Name = "زبان *")]
        [Required(ErrorMessage = "لطفا زبان را انتخاب نمایید.")]
        public string Language { get; set; }
        [Display(Name = "انتشار")]
        public bool IsPublicationActive { get; set; }
        public List<SelectListItem> CategoriesList { get; set; }
    }

    public class CategoryEditViewModel
    {
        [Display(Name = "شناسه")]
        public string Id { get; set; }
        [Display(Name = "نام دسته بندی *")]
        [Required(ErrorMessage = "لطفا نام دسته بندی را وارد نمایید.")]
        public string Name { get; set; }
        [Display(Name = "دسته بندی")]
        public string Parents { get; set; }
        [Display(Name = "توضیح")]
        public string Description { get; set; }
        public string Group { get; set; }
        [Display(Name = "زبان *")]
        [Required(ErrorMessage = "لطفا زبان را انتخاب نمایید.")]
        public string Language { get; set; }
        [Display(Name = "انتشار")]
        public bool IsPublicationActive { get; set; }
        public List<SelectListItem> CategoriesList { get; set; }
    }

    public class CategorySiteMapViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<CategorySiteMapViewModel> Childs { get; set; }
        public DateTime Date { get; set; }
    }
}