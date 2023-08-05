using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TochalTools.Models
{
    public class UserDetailsViewModel
    {
        public string Role { get; set; }
        public string UserId { get; set; }
    }

    public class Status
    {
        public string Text { get; set; }
        public string Class { get; set; }
    }
}