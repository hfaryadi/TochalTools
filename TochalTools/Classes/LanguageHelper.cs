using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TochalTools.Classes
{
    public static class LanguageHelper
    {
        public static List<SelectListItem> ReadAllLanguagesForSelect()
        {
            return new List<SelectListItem> {
                new SelectListItem { Value = "fa", Text = "فارسی"},
                new SelectListItem { Value = "en", Text = "انگلیسی"}
            };
        }
        public static string ReadLanguageByText(string text)
        {
            switch (text)
            {
                case "فارسی":
                    return "fa";
                case "انگلیسی":
                    return "en";
                default:
                    return "";
            }
        }

        public static string ReadLanguageByValue(string value)
        {
            switch (value)
            {
                case "fa":
                    return "فارسی";
                case "en":
                    return "انگلیسی";
                default:
                    return "";
            }
        }
    }
}