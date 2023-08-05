using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TochalTools.Classes
{
    public static class InboxHelper
    {
        public static string ReadGroupByISO(string iso)
        {
            switch (iso)
            {
                case "Direct":
                    return "پیام مستقیم";
                case "ContactUs":
                    return "تماس با ما";
                default:
                    return "";
            }
        }
    }
}