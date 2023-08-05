using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TochalTools.Models
{
    [Table("Comments")]
    public class CommentModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public int CommentId { get; set; }
        public string Group { get; set; }
        public string RefId { get; set; }
        public string UserId { get; set; }
        public bool IsConfirmed { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Date { get; set; }
    }
}