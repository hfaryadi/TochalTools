using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TochalTools.Models
{
    public class TagListViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Group { get; set; }
        public string Language { get; set; }
        public string Review { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Update { get; set; }
    }

    public class TagDetailsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Group { get; set; }
        public string Review { get; set; }
        public string OptimizationTitle { get; set; }
        public string OptimizationKeywords { get; set; }
        public string OptimizationDescription { get; set; }
    }

    public class TagCreateViewModel
    {
        [Display(Name = "عنوان برچسب *")]
        [Required(ErrorMessage = "لطفا عنوان برچسب را وارد نمایید.")]
        public string Title { get; set; }
        [Display(Name = "توضیح")]
        public string Description { get; set; }
        [Display(Name = "گروه *")]
        [Required(ErrorMessage = "لطفا گروه را انتخاب نمایید.")]
        public string Group { get; set; }
        [Display(Name = "زبان *")]
        [Required(ErrorMessage = "لطفا زبان را انتخاب نمایید.")]
        public string Language { get; set; }
        [Display(Name = "عنوان")]
        public string OptimizationTitle { get; set; }
        [Display(Name = "کلمات کلیدی")]
        public string OptimizationKeywords { get; set; }
        [Display(Name = "توضیح مختصر")]
        public string OptimizationDescription { get; set; }
    }

    public class TagEditViewModel
    {
        [Display(Name = "شناسه")]
        public int Id { get; set; }
        [Display(Name = "عنوان برچسب *")]
        [Required(ErrorMessage = "لطفا عنوان برچسب را وارد نمایید.")]
        public string Title { get; set; }
        [Display(Name = "توضیح")]
        public string Description { get; set; }
        [Display(Name = "گروه *")]
        [Required(ErrorMessage = "لطفا گروه را انتخاب نمایید.")]
        public string Group { get; set; }
        [Display(Name = "زبان *")]
        [Required(ErrorMessage = "لطفا زبان را انتخاب نمایید.")]
        public string Language { get; set; }
        [Display(Name = "عنوان")]
        public string OptimizationTitle { get; set; }
        [Display(Name = "کلمات کلیدی")]
        public string OptimizationKeywords { get; set; }
        [Display(Name = "توضیح مختصر")]
        public string OptimizationDescription { get; set; }
    }

    public class TagDetailsPageViewModel
    {
        public TagDetailsViewModel Tag { get; set; }
    }
}