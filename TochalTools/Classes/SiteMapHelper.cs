using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TochalTools.Classes
{
    public static class SiteMapHelper
    {
        public static List<SelectListItem> ReadSiteMapPriority()
        {
            return new List<SelectListItem> {
                new SelectListItem { Value = "0", Text = "0"},
                new SelectListItem { Value = "0.1", Text = "0.1"},
                new SelectListItem { Value = "0.2", Text = "0.2"},
                new SelectListItem { Value = "0.3", Text = "0.3"},
                new SelectListItem { Value = "0.4", Text = "0.4"},
                new SelectListItem { Value = "0.5", Text = "0.5"},
                new SelectListItem { Value = "0.6", Text = "0.6"},
                new SelectListItem { Value = "0.7", Text = "0.7"},
                new SelectListItem { Value = "0.8", Text = "0.8"},
                new SelectListItem { Value = "0.9", Text = "0.9"},
                new SelectListItem { Value = "1", Text = "1"}
            };
        }

        public static List<SelectListItem> ReadSiteMapFrequency()
        {
            return new List<SelectListItem> {
                new SelectListItem { Value = "hourly", Text = "ساعتی"},
                new SelectListItem { Value = "daily", Text = "روزانه"},
                new SelectListItem { Value = "weekly", Text = "هفتگی"},
                new SelectListItem { Value = "monthly", Text = "ماهانه"},
                new SelectListItem { Value = "yearly", Text = "سالانه"},
                new SelectListItem { Value = "always", Text = "همیشه"},
                new SelectListItem { Value = "never", Text = "هیچوقت"},
            };
        }
        public static Location.eChangeFrequency ReturnFrequency(string text)
        {
            switch (text)
            {
                case "hourly":
                    return Location.eChangeFrequency.hourly;
                case "daily":
                    return Location.eChangeFrequency.daily;
                case "weekly":
                    return Location.eChangeFrequency.weekly;
                case "monthly":
                    return Location.eChangeFrequency.monthly;
                case "yearly":
                    return Location.eChangeFrequency.yearly;
                case "always":
                    return Location.eChangeFrequency.always;
                case "never":
                    return Location.eChangeFrequency.never;
                default:
                    return Location.eChangeFrequency.always;
            }
        }
        public static string ReturnPersian(string text)
        {
            switch (text)
            {
                case "hourly":
                    return "ساعتی";
                case "daily":
                    return "روزانه";
                case "weekly":
                    return "هفتگی";
                case "monthly":
                    return "ماهانه";
                case "yearly":
                    return "سالانه";
                case "always":
                    return "همیشه";
                case "never":
                    return "هیچوقت";
                default:
                    return "هفتگی";
            }
        }
    }
}