using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TochalTools.Models
{
    public class BoxIndexViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Icon { get; set; }
        public string Link { get; set; }
    }

    public class BoxListViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public int Priority { get; set; }
        public string Language { get; set; }
        public string Update { get; set; }
    }

    public class BoxCreateViewModel
    {
        [Display(Name = "عنوان *")]
        [Required(ErrorMessage = "لطفا عنوان محتوا را وارد نمایید.")]
        public string Title { get; set; }
        [Display(Name = "آدرس *")]
        [Required(ErrorMessage = "لطفا آدرس محتوا را وارد نمایید.")]
        public string Address { get; set; }
        [AllowHtml]
        [Display(Name = "محتوا")]
        public string Content { get; set; }
        [Display(Name = "آیکون")]
        public string Icon { get; set; }
        [Display(Name = "لینک")]
        public string Link { get; set; }
        [Display(Name = "اولویت *")]
        [Required(ErrorMessage = "لطفا اولویت را انتخاب نمایید.")]
        public int Priority { get; set; }
        [Display(Name = "زبان")]
        [Required(ErrorMessage = "لطفا زبان را انتخاب نمایید.")]
        public string Language { get; set; }
    }

    public class BoxEditViewModel
    {
        [Display(Name = "شناسه")]
        public int Id { get; set; }
        [Display(Name = "عنوان *")]
        [Required(ErrorMessage = "لطفا عنوان محتوا را وارد نمایید.")]
        public string Title { get; set; }
        [Display(Name = "آدرس *")]
        [Required(ErrorMessage = "لطفا آدرس محتوا را وارد نمایید.")]
        public string Address { get; set; }
        [AllowHtml]
        [Display(Name = "محتوا")]
        public string Content { get; set; }
        [Display(Name = "آیکون")]
        public string Icon { get; set; }
        [Display(Name = "لینک")]
        public string Link { get; set; }
        [Display(Name = "اولویت *")]
        [Required(ErrorMessage = "لطفا اولویت را انتخاب نمایید.")]
        public int Priority { get; set; }
        [Display(Name = "زبان")]
        [Required(ErrorMessage = "لطفا زبان را انتخاب نمایید.")]
        public string Language { get; set; }
    }
}