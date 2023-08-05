using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TochalTools.Classes;

namespace TochalTools.Models
{
    public class RoleViewModel
    {
        public RoleViewModel() { }
        public RoleViewModel(ApplicationRole role)
        {
            Id = role.Id;
            Name = role.Name;
            PersianName = RoleHelper.GetPersianRoleName(role.Name);
        }
        [Display(Name = "شناسه")]
        public string Id { get; set; }
        [Required(ErrorMessage = "لطفا نام را وارد نمایید.")]
        [Display(Name = "نام رول *")]
        public string Name { get; set; }
        [Display(Name = "نام فارسی")]
        public string PersianName { get; set; }
    }
}