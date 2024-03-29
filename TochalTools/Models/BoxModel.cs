﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TochalTools.Models
{
    [Table("Boxes")]
    public class BoxModel
    {
        [Key]
        public int Id { get; set; }
        public string Address { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Icon { get; set; }
        public string Link { get; set; }
        public string Language { get; set; }
        public int Priority { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Date { get; set; }
        public DateTime Update { get; set; }
    }
}