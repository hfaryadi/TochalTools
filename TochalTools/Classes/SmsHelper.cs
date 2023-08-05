using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TochalTools.Classes
{
    public class SmsHelper
    {
        public void SendSMS(string[] mobileNumber, string content)
        {
            PARSGREEN.API.SMS.Send.SendSMS sendSMS = new PARSGREEN.API.SMS.Send.SendSMS();
            int _successCount = 0;
            string[] _ReturnStr = null;
            sendSMS.SendGroupSMS("9FFCAC5A-D54E-4900-95F9-72DC79BBEC5F", string.Empty, mobileNumber, content, false, string.Empty, ref _successCount, ref _ReturnStr);
        }
    }
}