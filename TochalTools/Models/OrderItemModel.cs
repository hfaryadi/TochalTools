using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TochalTools.Models
{
    [Table("OrderItems")]
    public class OrderItemModel
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public Int64 Price { get; set; }
        public int Count { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public bool IsDeleted { get; set; }
    }
}