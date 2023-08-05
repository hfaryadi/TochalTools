using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TochalTools.Models
{
    [Table("Tags")]
    public class TagModel
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Group { get; set; }
        public string Language { get; set; }
        public Int64 Review { get; set; }
        public string OptimizationTitle { get; set; }
        public string OptimizationKeywords { get; set; }
        public string OptimizationDescription { get; set; }
        public string UserId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Date { get; set; }
        public DateTime Update { get; set; }
    }
}