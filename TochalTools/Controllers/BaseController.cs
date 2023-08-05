using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TochalTools.Classes;

namespace TochalTools.Controllers
{
    public class BaseController : Controller
    {
        public void ImageResizer(int width, int height, string fileType, string moduleName, string fileName, string fileExtension)
        {
            StorageHelper.ImageResizer(width, height, fileType, moduleName, fileName, fileExtension);
        }

        [HttpPost]
        public ActionResult UploadImageByCkeditor(HttpPostedFileBase upload, string CKEditorFuncNum, string CKEditor,
           string langCode)
        {
            string vImagePath = String.Empty;
            string vMessage = String.Empty;
            string vFilePath = String.Empty;
            string vOutput = String.Empty;
            var now = DateTime.Now.ToString("yyyyMMdd-HHMMssff");
            var vFileName = now + Path.GetExtension(upload.FileName).ToLower();
            try
            {
                vImagePath = Url.Content("/Content/Images/Editor/" + vFileName);
                StorageHelper.SaveFile(upload, "Images", "Editor", now, Path.GetExtension(upload.FileName).ToLower());
                vMessage = "تصویر با مفقیت ذخیره شد";
            }
            catch
            {
                vMessage = "There was an issue uploading";
            }
            vOutput = @"<html><body><script>window.parent.CKEDITOR.tools.callFunction(" + CKEditorFuncNum + ", \"" + vImagePath + "\", \"" + vMessage + "\");</script></body></html>";
            return Content(vOutput);
        }

        public JsonResult UploadFileByFiler(string fileType, string moduleName, string fileName, string fileExtension)
        {
            HttpPostedFileBase file = Request.Files[0];
            return Json(StorageHelper.SaveFileByFiler(file, fileType, moduleName, fileName, fileExtension), JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteFile(string fileType, string moduleName, string fileName, string fileExtension, string counter)
        {
            StorageHelper.DeleteFile(fileType, moduleName, fileName, fileExtension, counter);
            return Json(fileName, JsonRequestBehavior.AllowGet);
        }
    }
}