using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TochalTools.Classes
{
    public static class CountHelper
    {
        public static string CalculateCount(Int64 count)
        {
            string Value = count.ToString();
            if (count > 999 && count < 1000000)
            {
                float Count = count / 1000;
                Value = (Math.Round(Count, 1)).ToString() + " K";
            }
            else if (count > 999999)
            {
                float Count = count / 1000000;
                Value = (Math.Round(Count, 2)).ToString() + " M";
            }
            return Value;
        }

        public static decimal GetPercent(Int64 price, Int64 off)
        {
            if (price > 0 && off > 0)
            {
                decimal percent = price / 100;
                return Math.Round((off / percent), 0);
            }
            return 0;
        }
    }
}