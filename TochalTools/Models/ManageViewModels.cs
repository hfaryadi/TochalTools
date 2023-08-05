using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace TochalTools.Models
{
    public class IndexViewModel
    {
        public bool HasPassword { get; set; }
        public IList<UserLoginInfo> Logins { get; set; }
        public string PhoneNumber { get; set; }
        public bool TwoFactor { get; set; }
        public bool BrowserRemembered { get; set; }
    }

    public class ManageLoginsViewModel
    {
        public IList<UserLoginInfo> CurrentLogins { get; set; }
        public IList<AuthenticationDescription> OtherLogins { get; set; }
    }

    public class FactorViewModel
    {
        public string Purpose { get; set; }
    }

    public class SetPasswordViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [DataType(DataType.Password, ErrorMessage = "لطفا کلمه عبور را به درستی وارد نمایید.")]
        [Display(Name = "کلمه عبور فعلی *")]
        [Required(ErrorMessage = "لطفا کلمه عبور فعلی را وارد نمایید.")]
        [StringLength(100, ErrorMessage = "کلمه عبور حداقل باید {2} کاراکتر باشد.", MinimumLength = 6)]
        public string OldPassword { get; set; }

        [DataType(DataType.Password, ErrorMessage = "لطفا کلمه عبور را به درستی وارد نمایید.")]
        [Display(Name = "کلمه عبور جدید *")]
        [Required(ErrorMessage = "لطفا کلمه عبور جدید را وارد نمایید.")]
        [StringLength(100, ErrorMessage = "کلمه عبور حداقل باید {2} کاراکتر باشد.", MinimumLength = 6)]
        public string NewPassword { get; set; }

        [DataType(DataType.Password, ErrorMessage = "لطفا کلمه عبور را به درستی وارد نمایید.")]
        [Display(Name = "تکرار کلمه عبور جدید *")]
        [Compare("NewPassword", ErrorMessage = "کلمه عبور جدید و تکرار کلمه عبور جدید یکسان نیستند.")]
        public string ConfirmPassword { get; set; }
    }

    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string Number { get; set; }
    }

    public class VerifyPhoneNumberViewModel
    {
        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }

    public class ConfigureTwoFactorViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
    }
}