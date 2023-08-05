using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TochalTools.Models
{
    public class SmsListViewModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string Groups { get; set; }
        public bool IsArchived { get; set; }
        public string Date { get; set; }
    }

    public class SmsCreateViewModel
    {
        [Display(Name = "متن پیامک *")]
        [Required(ErrorMessage = "لطفا متن پیامک را وارد نمایید.")]
        public string Content { get; set; }
        [Display(Name = "اضافه کردن شماره")]
        public string Mobiles { get; set; }
        public bool IsNewsLetters { get; set; }
        public bool IsUsers { get; set; }
    }
}