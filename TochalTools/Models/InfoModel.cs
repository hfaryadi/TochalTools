using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TochalTools.Models
{
    [Table("Infos")]
    public class InfoModel
    {
        [Key]
        public int Id { get; set; }
        public string WebsiteTitle { get; set; }
        public string CompanyName { get; set; }
        public string WorkingHours { get; set; }
        public string Description { get; set; }
        public string FirstTell { get; set; }
        public string SecondTell { get; set; }
        public string FirstMobile { get; set; }
        public string SecondMobile { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string WebsiteUrl { get; set; }
        public int CountryId { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }
        public string PostalCode { get; set; }
        public string Address { get; set; }
        public string Telegram { get; set; }
        public string Instagram { get; set; }
        public string Twitter { get; set; }
        public string Facebook { get; set; }
        public string GooglePlus { get; set; }
        public string Linkedin { get; set; }
        public string Language { get; set; }
        public string OptimizationTitle { get; set; }
        public string OptimizationKeywords { get; set; }
        public string OptimizationDescription { get; set; }
        public bool IsDeleted { get; set; }
    }
}