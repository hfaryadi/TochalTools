using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TochalTools.Models
{
    public class InboxListViewModel
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string ReceiverId { get; set; }
        public string ReceiverName { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Group { get; set; }
        public string Date { get; set; }
        public Status Status { get; set; }
    }

    public class InboxDetailsViewModel
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Tell { get; set; }
        public string Website { get; set; }
        public string ReceiverId { get; set; }
        public string UserId { get; set; }
        public string Type { get; set; }
        public string GroupName { get; set; }
        public string Date { get; set; }
        public Status Status { get; set; }
    }

    public class InboxCreateViewModel
    {
        [Display(Name = "موضوع *")]
        [Required(ErrorMessage = "لطفا موضوع را وارد نمایید.")]
        public string Subject { get; set; }
        [Display(Name = "متن پیام *")]
        [Required(ErrorMessage = "لطفا متن پیام را وارد نمایید.")]
        public string Content { get; set; }
        [Display(Name = "گیرنده *")]
        [Required(ErrorMessage = "لطفا گیرنده را انتخاب نمایید.")]
        public string ReceiverId { get; set; }
    }

    public class InboxCustomCreateViewModel
    {
        [Display(Name = "موضوع *")]
        [Required(ErrorMessage = "لطفا موضوع را وارد نمایید.")]
        public string Subject { get; set; }
        [Display(Name = "متن پیام *")]
        [Required(ErrorMessage = "لطفا متن پیام را وارد نمایید.")]
        public string Content { get; set; }
        [Display(Name = "نام و نام خانوادگی")]
        [Required(ErrorMessage = "لطفا نام و نام خانوادگی خود را وارد نمایید.")]
        public string Name { get; set; }
        [Display(Name = "ایمیل")]
        public string Email { get; set; }
        [Display(Name = "شماره تماس")]
        public string Tell { get; set; }
        [Display(Name = "آدرس وب سایت")]
        public string Website { get; set; }
        [Display(Name = "گروه *")]
        [Required(ErrorMessage = "لطفا گروه را وارد نمایید.")]
        public string Group { get; set; }
    }
}