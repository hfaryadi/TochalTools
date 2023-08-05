using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TochalTools.Models
{
    [Table("Brands")]
    public class BrandModel
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public string UserId { get; set; }
        public bool IsAtHomeActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Date { get; set; }
    }
}