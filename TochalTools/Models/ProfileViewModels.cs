using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TochalTools.Models
{
    public class ProfileEditViewModel
    {
        [Display(Name = "شناسه")]
        public string Id { get; set; }
        [Display(Name = "نام و نام خانوادگی *")]
        [Required(ErrorMessage = "لطفا نام و نام خانوادگی را وارد نمایید.")]
        public string Name { get; set; }
        [Display(Name = "توضیح کوتاه")]
        public string Description { get; set; }
        [Display(Name = "موبایل *")]
        [Phone(ErrorMessage = "لطفا شماره موبایل را به درستی وارد نمایید.")]
        [Required(ErrorMessage = "لطفا شماره موبایل را وارد نمایید.")]
        [RegularExpression("\\d+", ErrorMessage = "لطفا فقط عدد وارد نمایید.")]
        public string Mobile { get; set; }
        [Display(Name = "تلفن ثابت")]
        [Phone(ErrorMessage = "لطفا شماره تلفن را به درستی وارد نمایید.")]
        [RegularExpression("\\d+", ErrorMessage = "لطفا فقط عدد وارد نمایید.")]
        public string Tell { get; set; }
        [Display(Name = "فکس")]
        [Phone(ErrorMessage = "لطفا شماره فکس را به درستی وارد نمایید.")]
        [RegularExpression("\\d+", ErrorMessage = "لطفا فقط عدد وارد نمایید.")]
        public string Fax { get; set; }
        [Display(Name = "ایمیل")]
        [EmailAddress(ErrorMessage = "لطفا ایمیل را به درستی وارد نمایید.")]
        public string Email { get; set; }
        [Display(Name = "وب سایت")]
        public string Website { get; set; }
        [Display(Name = "کشور *")]
        [Required(ErrorMessage = "لطفا کشور را انتخاب نمایید.")]
        public int CountryId { get; set; }
        [Display(Name = "استان *")]
        [Required(ErrorMessage = "لطفا استان را انتخاب نمایید.")]
        public int StateId { get; set; }
        [Display(Name = "شهر *")]
        [Required(ErrorMessage = "لطفا شهر را انتخاب نمایید.")]
        public int CityId { get; set; }
        [Display(Name = "کد پستی")]
        [RegularExpression("\\d+", ErrorMessage = "لطفا فقط عدد وارد نمایید.")]
        public string PostalCode { get; set; }
        [Display(Name = "آدرس *")]
        [Required(ErrorMessage = "لطفا آدرس را وارد نمایید.")]
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
        public List<SelectListItem> CountriesList { get; set; }
        public List<SelectListItem> StatesList { get; set; }
        public List<SelectListItem> CitiesList { get; set; }
    }
}