using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TochalTools.Models
{
    public class SiteMapIndexViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public Location.eChangeFrequency ChangeFrequency { get; set; }
        public string Priority { get; set; }
        public DateTime LastModified { get; set; }
    }

    public class SiteMapListViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string ChangeFrequency { get; set; }
        public string Priority { get; set; }
        public DateTime LastModified { get; set; }
    }

    public class SiteMapCreateViewModel
    {
        [Display(Name = "عنوان *")]
        [Required(ErrorMessage = "لطفا عنوان را وارد نمایید.")]
        public string Title { get; set; }
        [Display(Name = "لینک *")]
        [Required(ErrorMessage = "لطفا لینک را وارد نمایید.")]
        public string Url { get; set; }
        [Display(Name = "فرکانس *")]
        [Required(ErrorMessage = "لطفا فرکانس را انتخاب نمایید.")]
        public string ChangeFrequency { get; set; }
        [Display(Name = "اولویت *")]
        [Required(ErrorMessage = "لطفا اولویت را انتخاب نمایید.")]
        public string Priority { get; set; }
    }

    public class SiteMapEditViewModel
    {
        [Display(Name = "شناسه")]
        public int Id { get; set; }
        [Display(Name = "عنوان *")]
        [Required(ErrorMessage = "لطفا عنوان را وارد نمایید.")]
        public string Title { get; set; }
        [Display(Name = "لینک *")]
        [Required(ErrorMessage = "لطفا لینک را وارد نمایید.")]
        public string Url { get; set; }
        [Display(Name = "فرکانس *")]
        [Required(ErrorMessage = "لطفا فرکانس را انتخاب نمایید.")]
        public string ChangeFrequency { get; set; }
        [Display(Name = "اولویت *")]
        [Required(ErrorMessage = "لطفا اولویت را انتخاب نمایید.")]
        public string Priority { get; set; }
    }
}