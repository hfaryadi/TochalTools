using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TochalTools.Models
{
    [Table("Products")]
    public class ProductModel
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }
        public string MoreDescription { get; set; }
        public string Code { get; set; }
        public string Categories { get; set; }
        public string Tags { get; set; }
        public int BrandId { get; set; }
        public string Language { get; set; }
        public string FileId { get; set; }
        public Int64 QT { get; set; }
        public Int64 Price { get; set; }
        public Int64 Off { get; set; }
        public Int64 BuyCount { get; set; }
        public Int64 Rate { get; set; }
        public Int64 RateCount { get; set; }
        public string OptimizationTitle { get; set; }
        public string OptimizationKeywords { get; set; }
        public string OptimizationDescription { get; set; }
        public string UserId { get; set; }
        public bool IsPublicationActive { get; set; }
        public bool IsAtHomeActive { get; set; }
        public bool IsCommentsActive { get; set; }
        public bool IsSpecial { get; set; }
        public bool IsProposal { get; set; }
        public bool IsPercentShow { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? OffExpireDate { get; set; }
        public DateTime Date { get; set; }
        public DateTime Update { get; set; }
    }

    [Table("ProductSettings")]
    public class ProductSettingsModel
    {
        [Key]
        public int Id { get; set; }
        public int OffProductId { get; set; }
        public DateTime? OffStartDate { get; set; }
        public DateTime? OffEndDate { get; set; }
        public DateTime? ProposalStartDate { get; set; }
        public DateTime? ProposalEndDate { get; set; }
        public Int64 CheapProductStartPrice { get; set; }
        public Int64 CheapProductEndPrice { get; set; }
    }
}