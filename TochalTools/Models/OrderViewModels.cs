using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TochalTools.Models
{

    public class OrderListViewModel
    {
        public int Id { get; set; }
        public int PrintId { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string ReceiverName { get; set; }
        public bool? IsConfirmed { get; set; }
        public bool IsAdminArchived { get; set; }
        public bool IsUserArchived { get; set; }
        public string Date { get; set; }
        public Status Status { get; set; }
    }

    public class OrderDetailsViewModel
    {
        public int Id { get; set; }
        public int PrintId { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Tell { get; set; }
        public string ReceiverName { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Address { get; set; }
        public string SendType { get; set; }
        public Int64 Price { get; set; }
        public string Date { get; set; }
        public Status Status { get; set; }
    }

    public class OrderCreateViewModel
    {
        [Display(Name = "نام و نام خانوادگی *")]
        [Required(ErrorMessage = "لطفا نام و نام خانوادگی را وارد نمایید.")]
        public string Name { get; set; }
        [Phone(ErrorMessage = "لطفا شماره موبایل را به درستی وارد نمایید.")]
        [Display(Name = "موبایل *")]
        [Required(ErrorMessage = "لطفا شماره موبایل را وارد نمایید.")]
        [RegularExpression("\\d+", ErrorMessage = "لطفا فقط عدد وارد نمایید.")]
        public string Mobile { get; set; }
        [Display(Name = "تلفن ثابت")]
        public string Tell { get; set; }
        [Display(Name = "نام تحویل گیرنده *")]
        [Required(ErrorMessage = "لطفا نام تحویل گیرنده را وارد نمایید.")]
        public string ReceiverName { get; set; }
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
        [Display(Name = "نوع ارسال *")]
        [Required(ErrorMessage = "لطفا نوع ارسال را انتخاب نمایید.")]
        public string SendType { get; set; }
        public List<SelectListItem> CountriesList { get; set; }

    }

    public class OrderListPageViewModel
    {
        public List<OrderListViewModel> Orders { get; set; }
        public int AllOrdersCount { get; set; }
        public int ConfirmedOrdersCount { get; set; }
        public int UnConfirmedOrdersCount { get; set; }
        public int RejectedOrdersCount { get; set; }
    }

    public class OrderDetailsPageViewModel
    {
        public InfoModel Info { get; set; }
        public OrderDetailsViewModel Order { get; set; }
        public List<OrderItemListViewModel> OrderItems { get; set; }
    }
}