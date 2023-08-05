using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TochalTools.Models
{
    public class MarketerListViewModel
    {
        public int Id { get; set; }
        public int TrackingCode { get; set; }
        public string Name { get; set; }
        public string Tell { get; set; }
        public string Age { get; set; }
        public string CountryName { get; set; }
        public string StateName { get; set; }
        public string CityName { get; set; }
        public bool IsArchived { get; set; }
        public string Date { get; set; }
    }
    public class MarketerCreateViewModel
    {
        [Display(Name = "نام و نام خانوادگی *")]
        [Required(ErrorMessage = "لطفا نام و نام خانوادگی خود را وارد نمایید.")]
        public string Name { get; set; }
        [Display(Name = "شماره تماس *")]
        [Required(ErrorMessage = "لطفا شماره تماس خود را وارد نمایید.")]
        public string Tell { get; set; }
        [Display(Name = "سن *")]
        [Required(ErrorMessage = "لطفا سن خود را وارد نمایید.")]
        public string Age { get; set; }
        [Display(Name = "کشور *")]
        [Required(ErrorMessage = "لطفا کشور را انتخاب نمایید.")]
        public int CountryId { get; set; }
        [Display(Name = "استان *")]
        [Required(ErrorMessage = "لطفا استان را انتخاب نمایید.")]
        public int StateId { get; set; }
        [Display(Name = "شهر *")]
        [Required(ErrorMessage = "لطفا شهر را انتخاب نمایید.")]
        public int CityId { get; set; }
        [Display(Name = "آدرس *")]
        [Required(ErrorMessage = "لطفا آدرس محل سکونت خود را وارد نمایید.")]
        public string Address { get; set; }
        [Display(Name = "توضیح کوتاه")]
        public string Description { get; set; }
        public List<SelectListItem> CountriesList { get; set; }
    }

    public class MarketerDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Tell { get; set; }
        public string Age { get; set; }
        public string CountryName { get; set; }
        public string StateName { get; set; }
        public string CityName { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public int TrackingCode { get; set; }
        public string Date { get; set; }
    }
}