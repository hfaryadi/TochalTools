using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TochalTools.Models
{
    [Table("NewsLetters")]
    public class NewsLetterModel
    {
        [Key]
        public int Id { get; set; }
        public string Mobile { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Date { get; set; }
    }
}