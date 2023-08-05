using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TochalTools.Models
{
    public class SliderIndexViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
    }

    public class SliderListViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public int Priority { get; set; }
        public string Language { get; set; }
        public string Update { get; set; }
    }

    public class SliderCreateViewModel
    {
        [Display(Name = "عنوان *")]
        [Required(ErrorMessage = "لطفا عنوان را وارد نمایید.")]
        public string Title { get; set; }
        [Display(Name = "محتوای اسلایدر")]
        public string Description { get; set; }
        [Display(Name = "لینک")]
        public string Link { get; set; }
        [Display(Name = "اولویت")]
        public int Priority { get; set; }
        [Display(Name = "زبان *")]
        [Required(ErrorMessage = "لطفا زبان را انتخاب نمایید.")]
        public string Language { get; set; }
    }

    public class SliderEditViewModel
    {
        [Display(Name = "شناسه")]
        public int Id { get; set; }
        [Display(Name = "عنوان *")]
        [Required(ErrorMessage = "لطفا عنوان را وارد نمایید.")]
        public string Title { get; set; }
        [Display(Name = "محتوای اسلایدر")]
        public string Description { get; set; }
        [Display(Name = "لینک")]
        public string Link { get; set; }
        [Display(Name = "اولویت")]
        public int Priority { get; set; }
        [Display(Name = "زبان *")]
        [Required(ErrorMessage = "لطفا زبان را انتخاب نمایید.")]
        public string Language { get; set; }
    }
}