using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TochalTools.Models
{
    public class BrandIndexViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }

    public class BrandListViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Language { get; set; }
        public string IsAtHomeActive { get; set; }
        public string Date { get; set; }
    }

    public class BrandCreateViewModel
    {
        [Display(Name = "نام برند *")]
        [Required(ErrorMessage = "لطفا نام برند را وارد نمایید.")]
        public string Title { get; set; }
        [Display(Name = "توضیح مختصر")]
        public string Description { get; set; }
        [Display(Name = "زبان *")]
        [Required(ErrorMessage = "لطفا زبان را انتخاب نمایید.")]
        public string Language { get; set; }
        [Display(Name = "صفحه نخست")]
        public bool IsAtHomeActive { get; set; }
    }

    public class BrandEditViewModel
    {
        [Display(Name = "شناسه")]
        public int Id { get; set; }
        [Display(Name = "نام برند *")]
        [Required(ErrorMessage = "لطفا نام برند را وارد نمایید.")]
        public string Title { get; set; }
        [Display(Name = "توضیح مختصر")]
        public string Description { get; set; }
        [Display(Name = "زبان *")]
        [Required(ErrorMessage = "لطفا زبان را انتخاب نمایید.")]
        public string Language { get; set; }
        [Display(Name = "صفحه نخست")]
        public bool IsAtHomeActive { get; set; }
    }
}