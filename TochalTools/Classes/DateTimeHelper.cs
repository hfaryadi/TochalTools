using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace TochalTools.Classes
{
    public static class DateTimeHelper
    {
        public static string ConvertToShamsi(DateTime dateTime, bool isIncludeTime = false)
        {
            PersianCalendar pc = new PersianCalendar();
            if (isIncludeTime)
            {
                int hour = pc.GetHour(dateTime);
                int minute = pc.GetMinute(dateTime);
                string Hour = "00";
                string Minute = "00";
                if (hour < 10)
                    Hour = string.Format("0{0}", hour);
                else
                    Hour = hour.ToString();
                if (minute < 10)
                    Minute = string.Format("0{0}", minute);
                else
                    Minute = minute.ToString();
                return string.Format("{0}/{1}/{2} - {3}:{4}", pc.GetYear(dateTime), pc.GetMonth(dateTime), pc.GetDayOfMonth(dateTime), Hour, Minute);
            }
            return string.Format("{0}/{1}/{2}", pc.GetYear(dateTime), pc.GetMonth(dateTime), pc.GetDayOfMonth(dateTime));
        }

        public static DateTime ConvertToDateTime(string date, string time = null)
        {
            PersianCalendar pc = new PersianCalendar();
            var dates = date.Split('/');
            var hour = 0;
            var minute = 0;
            if (time != null && time != "")
            {
                var times = time.Split(':');
                hour = Convert.ToInt32(times[0]);
                minute = Convert.ToInt32(times[1]);
            }
            return pc.ToDateTime(Convert.ToInt32(dates[0]), Convert.ToInt32(dates[1]), Convert.ToInt32(dates[2]), hour, minute, 0, 0);
        }

        public static string GetMonthName(string month)
        {
            switch(month)
            {
                case "1":
                    return "فروردین";
                case "2":
                    return "اردیبهشت";
                case "3":
                    return "خرداد";
                case "4":
                    return "تیر";
                case "5":
                    return "مرداد";
                case "6":
                    return "شهریور";
                case "7":
                    return "مهر";
                case "8":
                    return "آبان";
                case "9":
                    return "آذر";
                case "10":
                    return "دی";
                case "11":
                    return "بهمن";
                case "12":
                    return "اسفند";
                default:
                    return "";
            }
        }
    }
}