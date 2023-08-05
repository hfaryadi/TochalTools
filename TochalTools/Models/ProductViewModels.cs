using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TochalTools.Models
{
    public class ProductIndexViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string FileId { get; set; }
        public Int64 Price { get; set; }
        public Int64 Off { get; set; }
        public decimal Percent { get; set; }
        public Int64 TotalPrice { get; set; }
        public Int64 Rate { get; set; }
        public bool IsSpecial { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsPercentShow { get; set; }
        public DateTime? OffExpireDate { get; set; }
    }

    public class ProductListViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string QT { get; set; }
        public string TotalPrice { get; set; }
        public string Language { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string IsPublicationActive { get; set; }
        public string IsAtHomeActive { get; set; }
        public string Update { get; set; }
    }

    public class ProductDetailsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }
        public string MoreDescription { get; set; }
        public string Code { get; set; }
        public string BrandName { get; set; }
        public string FileId { get; set; }
        public Int64 Price { get; set; }
        public Int64 Off { get; set; }
        public Int64 TotalPrice { get; set; }
        public Int64 Rate { get; set; }
        public string OptimizationTitle { get; set; }
        public string OptimizationKeywords { get; set; }
        public string OptimizationDescription { get; set; }
        public bool IsCommentsActive { get; set; }
        public bool IsSpecial { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime? OffExpireDate { get; set; }
        public List<string> Images { get; set; }
        public List<CommentIndexViewModel> Comments { get; set; }
        public List<SelectListItem> Tags { get; set; }
        public List<ProductIndexViewModel> SimilarProducts { get; set; }
    }

    public class ProductCreateViewModel
    {
        [Display(Name = "عنوان محصول *")]
        [Required(ErrorMessage = "لطفا عنوان محصول را وارد نمایید.")]
        public string Title { get; set; }
        [AllowHtml]
        [Display(Name = "توضیح محصول")]
        public string ShortDescription { get; set; }
        [AllowHtml]
        [Display(Name = "مشخصات فنی")]
        public string FullDescription { get; set; }
        [AllowHtml]
        [Display(Name = "توضیح کامل")]
        public string MoreDescription { get; set; }
        [Display(Name = "کد محصول")]
        public string Code { get; set; }
        [Display(Name = "دسته بندی *")]
        [Required(ErrorMessage = "لطفا دسته بندی را انتخاب نمایید.")]
        public string Categories { get; set; }
        [Display(Name = "برچسب")]
        public string Tags { get; set; }
        [Display(Name = "برند")]
        public int? BrandId { get; set; }
        [Display(Name = "زبان *")]
        [Required(ErrorMessage = "لطفا زبان را انتخاب نمایید.")]
        public string Language { get; set; }
        public string FileId { get; set; }
        [Display(Name = "تعداد *")]
        [Required(ErrorMessage = "لطفا تعداد را وارد نمایید.")]
        public Int64 QT { get; set; }
        [Display(Name = "مبلغ(تومان) *")]
        [Required(ErrorMessage = "لطفا مبلغ را وارد نمایید.")]
        public Int64 Price { get; set; }
        [Display(Name = "تخفیف(تومان) *")]
        [Required(ErrorMessage = "لطفا تخفیف را وارد نمایید.")]
        public Int64 Off { get; set; }
        [Display(Name = "عنوان")]
        public string OptimizationTitle { get; set; }
        [Display(Name = "کلمات کلیدی")]
        public string OptimizationKeywords { get; set; }
        [Display(Name = "توضیح مختصر")]
        public string OptimizationDescription { get; set; }
        [Display(Name = "انتشار")]
        public bool IsPublicationActive { get; set; }
        [Display(Name = "نمایش در صفحه نخست")]
        public bool IsAtHomeActive { get; set; }
        [Display(Name = "نمایش نظرات")]
        public bool IsCommentsActive { get; set; }
        [Display(Name = "محصول ویژه")]
        public bool IsSpecial { get; set; }
        [Display(Name = "پیشنهاد شگفت انگیز")]
        public bool IsProposal { get; set; }
        [Display(Name = "نمایش درصد تخفیف")]
        public bool IsPercentShow { get; set; }
        [Display(Name = "انقضای تخفیف (تاریخ)")]
        public string OffExpireDate { get; set; }
        [Display(Name = "انقضای تخفیف (زمان)")]
        public string OffExpireTime { get; set; }
        public List<SelectListItem> CategoriesList { get; set; }
        public List<SelectListItem> TagsList { get; set; }
        public List<SelectListItem> BrandsList { get; set; }
    }

    public class ProductEditViewModel
    {
        [Display(Name = "شناسه")]
        public int Id { get; set; }
        [Display(Name = "عنوان محصول *")]
        [Required(ErrorMessage = "لطفا عنوان محصول را وارد نمایید.")]
        public string Title { get; set; }
        [AllowHtml]
        [Display(Name = "توضیح محصول")]
        public string ShortDescription { get; set; }
        [AllowHtml]
        [Display(Name = "مشخصات فنی")]
        public string FullDescription { get; set; }
        [AllowHtml]
        [Display(Name = "توضیح کامل")]
        public string MoreDescription { get; set; }
        [Display(Name = "کد محصول")]
        public string Code { get; set; }
        [Display(Name = "دسته بندی *")]
        [Required(ErrorMessage = "لطفا دسته بندی را انتخاب نمایید.")]
        public string Categories { get; set; }
        [Display(Name = "برچسب")]
        public string Tags { get; set; }
        [Display(Name = "برند")]
        public int? BrandId { get; set; }
        [Display(Name = "زبان *")]
        [Required(ErrorMessage = "لطفا زبان را انتخاب نمایید.")]
        public string Language { get; set; }
        public string FileId { get; set; }
        [Display(Name = "تعداد *")]
        [Required(ErrorMessage = "لطفا تعداد را وارد نمایید.")]
        public Int64 QT { get; set; }
        [Display(Name = "مبلغ(تومان) *")]
        [Required(ErrorMessage = "لطفا مبلغ را وارد نمایید.")]
        public Int64 Price { get; set; }
        [Display(Name = "تخفیف(تومان) *")]
        [Required(ErrorMessage = "لطفا تخفیف را وارد نمایید.")]
        public Int64 Off { get; set; }
        [Display(Name = "عنوان")]
        public string OptimizationTitle { get; set; }
        [Display(Name = "کلمات کلیدی")]
        public string OptimizationKeywords { get; set; }
        [Display(Name = "توضیح مختصر")]
        public string OptimizationDescription { get; set; }
        [Display(Name = "انتشار")]
        public bool IsPublicationActive { get; set; }
        [Display(Name = "نمایش در صفحه نخست")]
        public bool IsAtHomeActive { get; set; }
        [Display(Name = "نمایش نظرات")]
        public bool IsCommentsActive { get; set; }
        [Display(Name = "محصول ویژه")]
        public bool IsSpecial { get; set; }
        [Display(Name = "پیشنهاد شگفت انگیز")]
        public bool IsProposal { get; set; }
        [Display(Name = "نمایش درصد تخفیف")]
        public bool IsPercentShow { get; set; }
        [Display(Name = "انقضای تخفیف (تاریخ)")]
        public string OffExpireDate { get; set; }
        [Display(Name = "انقضای تخفیف (زمان)")]
        public string OffExpireTime { get; set; }
        public List<SelectListItem> ImagesList { get; set; }
        public List<SelectListItem> CategoriesList { get; set; }
        public List<SelectListItem> TagsList { get; set; }
        public List<SelectListItem> BrandsList { get; set; }
    }

    public class ProductIndexPageViewModel
    {
        public IEnumerable<ProductIndexViewModel> Products { get; set; }
        public string MainCategory { get; set; }
        public string SubCategory { get; set; }
        public SelectListItem Category { get; set; }
        public string Search { get; set; }
        public int PageCount { get; set; }
        public int Page { get; set; }
        public string Type { get; set; }
        public string Brand { get; set; }
    }

    public class ProductCartViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string FileId { get; set; }
        public int Count { get; set; }
        public Int64 TotalPrice { get; set; }
        public Int64 FinalPrice { get; set; }
        public bool IsAvailable { get; set; }
    }

    public class ProductCartPageViewModel
    {
        public List<ProductCartViewModel> Products { get; set; }
        public Int64 Price { get; set; }
    }

    public class ProductSettingsEditViewModel
    {
        [Display(Name = "شناسه")]
        public int Id { get; set; }
        [Display(Name = "محصول تخفیف خورده")]
        public int? OffProductId { get; set; }
        [Display(Name = "تاریخ شروع")]
        public string OffStartDate { get; set; }
        [Display(Name = "زمان شروع")]
        public string OffStartTime { get; set; }
        [Display(Name = "تاریخ پایان")]
        public string OffEndDate { get; set; }
        [Display(Name = "زمان پایان")]
        public string OffEndTime { get; set; }
        [Display(Name = "تاریخ شروع")]
        public string ProposalStartDate { get; set; }
        [Display(Name = "زمان شروع")]
        public string ProposalStartTime { get; set; }
        [Display(Name = "تاریخ پایان")]
        public string ProposalEndDate { get; set; }
        [Display(Name = "زمان پایان")]
        public string ProposalEndTime { get; set; }
        [Display(Name = "شروع قیمت از *")]
        [Required(ErrorMessage = "لطفا شروع قیمت را وارد نمایید.")]
        public Int64 CheapProductStartPrice { get; set; }
        [Display(Name = "تا قیمت *")]
        [Required(ErrorMessage = "لطفا پایان قیمت را وارد نمایید.")]
        public Int64 CheapProductEndPrice { get; set; }
        public List<SelectListItem> ProductsList { get; set; }
    }
}