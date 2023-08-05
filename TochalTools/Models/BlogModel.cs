using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TochalTools.Models
{
    [Table("Blogs")]
    public class BlogModel
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortContent { get; set; }
        public string FullContent { get; set; }
        public string Categories { get; set; }
        public string Tags { get; set; }
        public string Language { get; set; }
        public Int64 Review { get; set; }
        public string OptimizationTitle { get; set; }
        public string OptimizationKeywords { get; set; }
        public string OptimizationDescription { get; set; }
        public string UserId { get; set; }
        public bool IsPublicationActive { get; set; }
        public bool IsAtHomeActive { get; set; }
        public bool IsCommentsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Date { get; set; }
        public DateTime Update { get; set; }
    }
}