using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TochalTools.Models
{
    public class InfoListViewModel
    {
        public int Id { get; set; }
        public string WebsiteTitle { get; set; }
        public string CompanyName { get; set; }
        public string FirstTell { get; set; }
        public string FirstMobile { get; set; }
        public string Language { get; set; }
    }

    public class InfoCreateViewModel
    {
        [Display(Name = "نام وب سایت")]
        public string WebsiteTitle { get; set; }
        [Display(Name = "نام شرکت")]
        public string CompanyName { get; set; }
        [Display(Name = "ساعات کاری")]
        public string WorkingHours { get; set; }
        [Display(Name = "درباره شرکت")]
        public string Description { get; set; }
        [Display(Name = "تلفن 1")]
        public string FirstTell { get; set; }
        [Display(Name = "تلفن 2")]
        public string SecondTell { get; set; }
        [Display(Name = "موبایل 1")]
        public string FirstMobile { get; set; }
        [Display(Name = "موبایل 2")]
        public string SecondMobile { get; set; }
        [Display(Name = "فکس")]
        public string Fax { get; set; }
        [Display(Name = "ایمیل")]
        [EmailAddress(ErrorMessage = "لطفا ایمیل را به درستی وارد نمایید.")]
        public string Email { get; set; }
        [Display(Name = "آدرس وب سایت")]
        public string WebsiteUrl { get; set; }
        [Display(Name = "کشور")]
        public int? CountryId { get; set; }
        [Display(Name = "استان")]
        public int? StateId { get; set; }
        [Display(Name = "شهر")]
        public int? CityId { get; set; }
        [Display(Name = "کد پستی")]
        public string PostalCode { get; set; }
        [Display(Name = "آدرس")]
        public string Address { get; set; }
        [Display(Name = "تلگرام")]
        public string Telegram { get; set; }
        [Display(Name = "اینستاگرام")]
        public string Instagram { get; set; }
        [Display(Name = "توییتر")]
        public string Twitter { get; set; }
        [Display(Name = "فیسبوک")]
        public string Facebook { get; set; }
        [Display(Name = "گوگل پلاس")]
        public string GooglePlus { get; set; }
        [Display(Name = "لینکدین")]
        public string Linkedin { get; set; }
        [Display(Name = "زبان *")]
        [Required(ErrorMessage = "لطفا زبان را انتخاب نمایید.")]
        public string Language { get; set; }
        [Display(Name = "عنوان")]
        public string OptimizationTitle { get; set; }
        [Display(Name = "کلمات کلیدی")]
        public string OptimizationKeywords { get; set; }
        [Display(Name = "توضیح مختصر")]
        public string OptimizationDescription { get; set; }
        public List<SelectListItem> CountriesList { get; set; }
        public List<SelectListItem> StatesList { get; set; }
        public List<SelectListItem> CitiesList { get; set; }
    }

    public class InfoEditViewModel
    {
        [Display(Name = "شناسه")]
        public int Id { get; set; }
        [Display(Name ="نام وب سایت")]
        public string WebsiteTitle { get; set; }
        [Display(Name = "نام شرکت")]
        public string CompanyName { get; set; }
        [Display(Name = "ساعات کاری")]
        public string WorkingHours { get; set; }
        [Display(Name = "درباره شرکت")]
        public string Description { get; set; }
        [Display(Name = "تلفن 1")]
        public string FirstTell { get; set; }
        [Display(Name = "تلفن 2")]
        public string SecondTell { get; set; }
        [Display(Name = "موبایل 1")]
        public string FirstMobile { get; set; }
        [Display(Name = "موبایل 2")]
        public string SecondMobile { get; set; }
        [Display(Name = "فکس")]
        public string Fax { get; set; }
        [Display(Name = "ایمیل")]
        [EmailAddress(ErrorMessage = "لطفا ایمیل را به درستی وارد نمایید.")]
        public string Email { get; set; }
        [Display(Name = "آدرس وب سایت")]
        public string WebsiteUrl { get; set; }
        [Display(Name = "کشور")]
        public int? CountryId { get; set; }
        [Display(Name = "استان")]
        public int? StateId { get; set; }
        [Display(Name = "شهر")]
        public int? CityId { get; set; }
        [Display(Name = "کد پستی")]
        public string PostalCode { get; set; }
        [Display(Name = "آدرس")]
        public string Address { get; set; }
        [Display(Name = "تلگرام")]
        public string Telegram { get; set; }
        [Display(Name = "اینستاگرام")]
        public string Instagram { get; set; }
        [Display(Name = "واتس آپ")]
        public string Twitter { get; set; }
        [Display(Name = "فیسبوک")]
        public string Facebook { get; set; }
        [Display(Name = "گوگل پلاس")]
        public string GooglePlus { get; set; }
        [Display(Name = "لینکدین")]
        public string Linkedin { get; set; }
        [Display(Name = "زبان *")]
        [Required(ErrorMessage = "لطفا زبان را انتخاب نمایید.")]
        public string Language { get; set; }
        [Display(Name = "عنوان")]
        public string OptimizationTitle { get; set; }
        [Display(Name = "کلمات کلیدی")]
        public string OptimizationKeywords { get; set; }
        [Display(Name = "توضیح مختصر")]
        public string OptimizationDescription { get; set; }
        public List<SelectListItem> CountriesList { get; set; }
        public List<SelectListItem> StatesList { get; set; }
        public List<SelectListItem> CitiesList { get; set; }
    }
}