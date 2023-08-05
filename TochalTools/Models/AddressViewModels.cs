using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TochalTools.Models
{
    public class CountryListViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class CountryCreateViewModel
    {
        [Required(ErrorMessage = "لطفا نام کشور را وارد نمایید.")]
        [Display(Name = "نام کشور *")]
        public string Name { get; set; }
    }

    public class CountryEditViewModel
    {
        [Display(Name = "شناسه")]
        public int Id { get; set; }
        [Required(ErrorMessage = "لطفا نام کشور را وارد نمایید.")]
        [Display(Name = "نام کشور *")]
        public string Name { get; set; }
    }

    public class StateListViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class StateListPageViewModel
    {
        public List<StateListViewModel> States { get; set; }
        public string CountryName { get; set; }
    }

    public class StateCreateViewModel
    {
        [Required(ErrorMessage = "لطفا نام استان را وارد نمایید.")]
        [Display(Name = "نام استان *")]
        public string Name { get; set; }
        [Display(Name = "شناسه کشور")]
        public int CountryId { get; set; }
    }

    public class StateEditViewModel
    {
        [Display(Name = "شناسه")]
        public int Id { get; set; }
        [Required(ErrorMessage = "لطفا نام استان را وارد نمایید.")]
        [Display(Name = "نام استان *")]
        public string Name { get; set; }
        [Display(Name = "شناسه کشور")]
        public int CountryId { get; set; }
    }

    public class CityListViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class CityListPageViewModel
    {
        public List<CityListViewModel> Cities { get; set; }
        public string StateName { get; set; }
    }

    public class CityCreateViewModel
    {
        [Required(ErrorMessage = "لطفا نام شهر را وارد نمایید.")]
        [Display(Name = "نام شهر *")]
        public string Name { get; set; }
        [Display(Name = "شناسه استان")]
        public int StateId { get; set; }
    }

    public class CityEditViewModel
    {
        [Display(Name = "شناسه")]
        public int Id { get; set; }
        [Required(ErrorMessage = "لطفا نام شهر را وارد نمایید.")]
        [Display(Name = "نام شهر *")]
        public string Name { get; set; }
        [Display(Name = "شناسه استان")]
        public int StateId { get; set; }
    }

}