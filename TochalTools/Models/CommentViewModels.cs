using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TochalTools.Models
{
    public class CommentIndexViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public string UserId { get; set; }
        public string Date { get; set; }
        public List<CommentIndexViewModel> Replays { get; set; }
    }

    public class CommentListViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public int CommentId { get; set; }
        public string CommentName { get; set; }
        public string Group { get; set; }
        public string GroupName { get; set; }
        public string RefId { get; set; }
        public string UserId { get; set; }
        public bool IsConfirmed { get; set; }
        public string Date { get; set; }
    }

    public class CommentCreateViewModel
    {
        [Display(Name = "نام و نام خانوادگی *")]
        [Required(ErrorMessage = "لطفا نام و نام خانوادگی را وارد نمایید.")]
        public string Name { get; set; }
        [Display(Name = "متن نظر *")]
        [Required(ErrorMessage = "لطفا متن نظر را وارد نمایید.")]
        public string Content { get; set; }
        [Display(Name = "پاسخ به")]
        public int CommentId { get; set; }
        [Display(Name = "گروه")]
        [Required(ErrorMessage = "لطفا گروه را وارد نمایید.")]
        public string Group { get; set; }
        [Display(Name = "مرجع")]
        [Required(ErrorMessage = "لطفا مرجع را وارد نمایید.")]
        public string RefId { get; set; }
    }
}