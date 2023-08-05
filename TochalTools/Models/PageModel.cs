using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TochalTools.Models
{
    [Table("Pages")]
    public class PageModel
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        [AllowHtml]
        public string Content { get; set; }
        public string Language { get; set; }
        public string OptimizationTitle { get; set; }
        public string OptimizationDescription { get; set; }
        public string OptimizationKeywords { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Date { get; set; }
    }
}