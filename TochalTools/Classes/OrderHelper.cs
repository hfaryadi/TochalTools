using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TochalTools.Classes
{
    public static class OrderHelper
    {
        public static List<SelectListItem> ReadAllSendTypesForSelect()
        {
            return new List<SelectListItem> {
                new SelectListItem { Value = "باربری", Text = "باربری"},
                new SelectListItem { Value = "تیپاکس", Text = "تیپاکس"}
            };
        }
    }
}