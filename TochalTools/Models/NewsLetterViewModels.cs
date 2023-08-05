using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TochalTools.Models
{
    public class NewsLetterListViewModel
    {
        public int Id { get; set; }
        public string Mobile { get; set; }
        public string Date { get; set; }
    }

    public class NewsLetterCreateViewModel
    {
        [Phone(ErrorMessage = "لطفا شماره موبایل را به درستی وارد نمایید.")]
        [Display(Name = "موبایل *")]
        [Required(ErrorMessage = "لطفا شماره موبایل را وارد نمایید.")]
        [RegularExpression("\\d+", ErrorMessage = "لطفا فقط عدد وارد نمایید.")]
        public string Mobile { get; set; }
    }
}