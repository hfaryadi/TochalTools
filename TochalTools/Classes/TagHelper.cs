using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TochalTools.Classes
{
    public static class TagHelper
    {
        public static List<SelectListItem> ReadAllGroupsForSelect()
        {
            return new List<SelectListItem> {
                new SelectListItem { Value = "Blog", Text = "مطالب"},
                new SelectListItem { Value = "Product", Text = "محصولات"}
            };
        }

        public static string ReadGroupByText(string text)
        {
            switch (text)
            {
                case "مطالب":
                    return "Blog";
                case "محصولات":
                    return "Product";
                default:
                    return "";
            }
        }

        public static string ReadGroupByValue(string value)
        {
            switch (value)
            {
                case "Blog":
                    return "مطالب";
                case "Product":
                    return "محصولات";
                default:
                    return "";
            }
        }
    }
}