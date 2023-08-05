using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TochalTools.Models
{
    [Table("Sitmap")]
    public class SiteMapModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string ChangeFrequency { get; set; }
        public string Priority { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime LastModified { get; set; }
    }
}