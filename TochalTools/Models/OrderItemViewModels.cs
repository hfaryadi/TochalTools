using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TochalTools.Models
{
    public class OrderItemListViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Int64 Price { get; set; }
        public int Count { get; set; }
        public Int64 TotalPrice { get; set; }
        public int ProductId { get; set; }
    }
}