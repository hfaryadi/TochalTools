using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TochalTools.Classes
{
    public static class RoleHelper
    {

        public static List<SelectListItem> ReadAllRolesForSelect(bool? isMainRole = null)
        {
            if (isMainRole == null)
            {
                return new List<SelectListItem> {
                    new SelectListItem { Value = "SuperAdministrator", Text = "مدیر کل"},
                    new SelectListItem { Value = "Administrator", Text = "ادمین"},
                    new SelectListItem { Value = "User", Text = "کاربر"},
                    new SelectListItem { Value = "Blog", Text = "مدیریت مطالب"},
                    new SelectListItem { Value = "Brand", Text = "مدیریت برندها"},
                    new SelectListItem { Value = "Category", Text = "مدیریت دسته بندی ها"},
                    new SelectListItem { Value = "Comment", Text = "مدیریت نظرات"},
                    new SelectListItem { Value = "Customer", Text = "مدیریت مشتریان"},
                    new SelectListItem { Value = "FAQ", Text = "مدیریت سوالات متداول"},
                    new SelectListItem { Value = "Marketer", Text = "مدیریت بازاریابان"},
                    new SelectListItem { Value = "Order", Text = "مدیریت سفارشات"},
                    new SelectListItem { Value = "Portfolio", Text = "مدیریت نمونه کارها"},
                    new SelectListItem { Value = "Product", Text = "مدیریت محصولات"},
                    new SelectListItem { Value = "Seo", Text = "مدیریت سئو"},
                    new SelectListItem { Value = "Sms", Text = "مدیریت پیامک ها"},
                    new SelectListItem { Value = "Tag", Text = "مدیریت برچسب ها"},
                    new SelectListItem { Value = "Testimonial", Text = "مدیریت نظرات مشتریان"}
                };
            }
            else if (isMainRole == true)
            {
                return new List<SelectListItem>
                {
                    new SelectListItem { Value = "SuperAdministrator", Text = "مدیر کل"},
                    new SelectListItem { Value = "Administrator", Text = "ادمین"},
                    new SelectListItem { Value = "User", Text = "کاربر"},
                };
            }
            else
            {
                return new List<SelectListItem>
                {
                    new SelectListItem { Value = "Blog", Text = "مدیریت مطالب"},
                    new SelectListItem { Value = "Brand", Text = "مدیریت برندها"},
                    new SelectListItem { Value = "Category", Text = "مدیریت دسته بندی ها"},
                    new SelectListItem { Value = "Comment", Text = "مدیریت نظرات"},
                    new SelectListItem { Value = "Customer", Text = "مدیریت مشتریان"},
                    new SelectListItem { Value = "FAQ", Text = "مدیریت سوالات متداول"},
                    new SelectListItem { Value = "Marketer", Text = "مدیریت بازاریابان"},
                    new SelectListItem { Value = "Order", Text = "مدیریت سفارشات"},
                    new SelectListItem { Value = "Portfolio", Text = "مدیریت نمونه کارها"},
                    new SelectListItem { Value = "Product", Text = "مدیریت محصولات"},
                    new SelectListItem { Value = "Seo", Text = "مدیریت سئو"},
                    new SelectListItem { Value = "Sms", Text = "مدیریت پیامک ها"},
                    new SelectListItem { Value = "Tag", Text = "مدیریت برچسب ها"},
                    new SelectListItem { Value = "Testimonial", Text = "مدیریت نظرات مشتریان"}
                };
            }
        }

        public static string GetPersianRoleName(string Name)
        {
            switch (Name)
            {
                case "Developer":
                    return "توسعه دهنده";
                case "SuperAdministrator":
                    return "مدیر کل";
                case "Administrator":
                    return "ادمین";
                case "User":
                    return "کاربر";
                case "Blog":
                    return "مدیریت مطالب";
                case "Brand":
                    return "مدیریت برندها";
                case "Category":
                    return "مدیریت دسته بندی ها";
                case "Comment":
                    return "مدیریت نظرات";
                case "Customer":
                    return "مدیریت مشتریان";
                case "FAQ":
                    return "مدیریت سوالات متداول";
                case "Marketer":
                    return "مدیریت بازاریابان";
                case "Order":
                    return "مدیریت سفارشات";
                case "Portfolio":
                    return "مدیریت نمونه کارها";
                case "Product":
                    return "مدیریت محصولات";
                case "Seo":
                    return "مدیریت سئو";
                case "Sms":
                    return "مدیریت پیامک ها";
                case "Tag":
                    return "مدیریت برچسب ها";
                case "Testimonial":
                    return "مدیریت نظرات مشتریان";
                default:
                    return "";
            }
        }
    }
}