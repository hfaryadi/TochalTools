using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace TochalTools.Classes
{
    public static class StorageHelper
    {
        public static void SaveFile(HttpPostedFileBase file, string fileType, string moduleName, string fileName, string fileExtension)
        {
            if (file != null && file.ContentLength > 0)
            {
                if (!Directory.Exists(HostingEnvironment.MapPath("/Content/" + fileType + "/" + moduleName)))
                {
                    try
                    {
                        Directory.CreateDirectory(HostingEnvironment.MapPath("/Content/" + fileType + "/" + moduleName));
                    }
                    catch { }
                }
                if (File.Exists(HostingEnvironment.MapPath("/Content/" + fileType + "/" + moduleName + "/" + fileName + fileExtension)))
                {
                    try
                    {
                        File.Delete(HostingEnvironment.MapPath("/Content/" + fileType + "/" + moduleName + "/" + fileName + fileExtension));
                    }
                    catch { }
                }
                try
                {
                    file.SaveAs(HostingEnvironment.MapPath("/Content/" + fileType + "/" + moduleName + "/" + fileName + fileExtension));
                }
                catch { }
            }
        }

        public static string SaveFileByFiler(HttpPostedFileBase file, string fileType, string moduleName, string fileName, string fileExtension)
        {
            if (file != null && file.ContentLength > 0)
            {
                string strMappath = "/Content/" + fileType + "/" + moduleName + "/";
                if (!Directory.Exists(HostingEnvironment.MapPath(strMappath)))
                {
                    try
                    {
                        Directory.CreateDirectory(HostingEnvironment.MapPath(strMappath));
                    }
                    catch { }
                }
                List<string> filesList = Directory.GetFiles(HostingEnvironment.MapPath(strMappath), fileName + "_" + "*" + fileExtension, SearchOption.TopDirectoryOnly).Select(Path.GetFileNameWithoutExtension).ToList();
                int i = 0;
                var iStr = "00";
                foreach (var item in filesList)
                {
                    int input = int.Parse(item.Split('_')[1]);
                    if (input > i)
                        i = input;
                }
                i++;
                if (i < 10)
                {
                    iStr = "0" + i;
                }
                else
                {
                    iStr = i.ToString();
                }
                try
                {
                    file.SaveAs(HostingEnvironment.MapPath(strMappath + fileName + "_" + iStr + fileExtension));
                }
                catch { }
                return iStr;
            }
            return "Error";
        }

        public static void DeleteFile(string fileType, string moduleName, string fileName, string fileExtension, string counter)
        {
            string strMappath = "/Content/" + fileType + "/" + moduleName + "/";
            if (!Directory.Exists(HostingEnvironment.MapPath(strMappath)))
            {
                try
                {
                    Directory.CreateDirectory(HostingEnvironment.MapPath(strMappath));
                }
                catch { }
            }
            try
            {
                File.Delete(HostingEnvironment.MapPath(strMappath + fileName + "_" + counter + fileExtension));
            }
            catch { }
        }

        public static void ImageResizer(int width, int height, string fileType, string moduleName, string fileName, string fileExtension)
        {
            if (File.Exists(HostingEnvironment.MapPath("/Content/" + fileType + "/" + moduleName + "/" + fileName + "." + fileExtension)))
            {
                new System.Web.Helpers.WebImage(@"~/Content/" + fileType + "/" + moduleName + "/" + fileName + "." + fileExtension)
                        .Resize(width, height, false, true)
                        .Crop(1, 1)
                        .Write();
            }
            else
            {
                new System.Web.Helpers.WebImage(@"~/Content/" + fileType + "/" + moduleName + "/" + "Default.jpg")
                    .Resize(width, height, false, true)
                    .Crop(1, 1)
                    .Write();
            }
        }
    }
}