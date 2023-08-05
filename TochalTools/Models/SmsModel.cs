using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TochalTools.Models
{
    [Table("Smses")]
    public class SmsModel
    {
        [Key]
        public int Id { get; set; }
        public string Content { get; set; }
        public string Groups { get; set; }
        public bool IsArchived { get; set; }
        public bool IsDeletet { get; set; }
        public DateTime Date { get; set; }
    }
}