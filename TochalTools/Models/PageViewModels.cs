using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TochalTools.Models
{
    public class PageListViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Language { get; set; }
        public string Date { get; set; }
    }
    public class PageDetailsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string OptimizationTitle { get; set; }
        public string OptimizationDescription { get; set; }
        public string OptimizationKeywords { get; set; }
    }
    public class PageCreateViewModel
    {
        [Display(Name = "عنوان صفحه *")]
        [Required(ErrorMessage = "لطفا عنوان صفحه را وارد نمایید.")]
        public string Title { get; set; }
        [AllowHtml]
        [Display(Name = "متن صفحه *")]
        [Required(ErrorMessage = "لطفا متن صفحه را وارد نمایید.")]
        public string Content { get; set; }
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
    public class PageEditViewModel
    {
        [Display(Name = "شناسه")]
        public int Id { get; set; }
        [Display(Name = "عنوان صفحه *")]
        [Required(ErrorMessage = "لطفا عنوان صفحه را وارد نمایید.")]
        public string Title { get; set; }
        [AllowHtml]
        [Display(Name = "متن صفحه *")]
        [Required(ErrorMessage = "لطفا متن صفحه را وارد نمایید.")]
        public string Content { get; set; }
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
}