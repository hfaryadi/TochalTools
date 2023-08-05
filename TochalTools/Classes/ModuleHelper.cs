using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace TochalTools.Classes
{
    public static class ModuleHelper
    {
        public static string ReadPersianNameByISO(string iso)
        {
            switch (iso)
            {
                case "Address":
                    return "آدرس ها";
                case "Blog":
                    return "مطالب";
                case "Box":
                    return "محتواها";
                case "Brand":
                    return "برندها";
                case "Category":
                    return "دسته بندی ها";
                case "Comment":
                    return "نظرات";
                case "Customer":
                    return "مشتریان";
                case "FAQ":
                    return "سوالات متداول";
                case "Info":
                    return "اطلاعات سایت";
                case "Page":
                    return "صفحات";
                case "Portfolio":
                    return "نمونه کارها";
                case "Product":
                    return "محصولات";
                case "Tag":
                    return "برچسب ها";
                case "Testimonial":
                    return "نظرات مشتریان";
                case "Slider":
                    return "اسلایدرها";
                default:
                    return "";
            }
        }
    }
}