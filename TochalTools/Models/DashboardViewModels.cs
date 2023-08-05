using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TochalTools.Models
{
    public class DashboardAdminViewModel
    {
        public int BlogCount { get; set; }
        public int ProductCount { get; set; }
        public int OrderCount { get; set; }
        public int UserCount { get; set; }
    }

    public class DashboardUserViewModel
    {
        public int AllOrdersCount { get; set; }
        public int ConfirmedOrdersCount { get; set; }
        public int UnConfirmedOrdersCount { get; set; }
        public int RejectedOrdersCount { get; set; }
    }
}