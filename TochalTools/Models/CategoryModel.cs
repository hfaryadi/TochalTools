using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TochalTools.Models
{
    [Table("Categories")]
    public class CategoryModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Parents { get; set; }
        public string Description { get; set; }
        public string Group { get; set; }
        public string Language { get; set; }
        public bool IsPublicationActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Date { get; set; }
    }
}