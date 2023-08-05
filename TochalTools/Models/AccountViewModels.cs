using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TochalTools.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Phone(ErrorMessage = "لطفا شماره موبایل را به درستی وارد نمایید.")]
        [Display(Name = "موبایل *")]
        [Required(ErrorMessage = "لطفا شماره موبایل را وارد نمایید.")]
        [RegularExpression("\\d+", ErrorMessage = "لطفا فقط عدد وارد نمایید.")]
        public string Mobile { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "کلمه عبور *")]
        [Required(ErrorMessage = "لطفا کلمه عبور را وارد نمایید.")]
        [StringLength(100, ErrorMessage = "کلمه عبور حداقل باید {2} کاراکتر باشد.", MinimumLength = 6)]
        public string Password { get; set; }
        [Display(Name = "مرا به خاطر بسپار؟")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Phone(ErrorMessage = "لطفا شماره موبایل را به درستی وارد نمایید.")]
        [Display(Name = "موبایل *")]
        [Required(ErrorMessage = "لطفا شماره موبایل را وارد نمایید.")]
        [RegularExpression("\\d+", ErrorMessage = "لطفا فقط عدد وارد نمایید.")]
        public string Mobile { get; set; }
        [DataType(DataType.Password, ErrorMessage = "لطفا کلمه عبور را به درستی وارد نمایید.")]
        [Display(Name = "کلمه عبور *")]
        [Required(ErrorMessage = "لطفا کلمه عبور را وارد نمایید.")]
        [StringLength(100, ErrorMessage = "کلمه عبور حداقل باید {2} کاراکتر باشد.", MinimumLength = 6)]
        public string Password { get; set; }
        [DataType(DataType.Password, ErrorMessage = "لطفا کلمه عبور را به درستی وارد نمایید.")]
        [Display(Name = "تکرار کلمه عبور *")]
        [Compare("Password", ErrorMessage = "کلمه عبور و تکرار کلمه عبور یکسان نیستند.")]
        public string ConfirmPassword { get; set; }
        [Display(Name = "رول *")]
        [Required(ErrorMessage = "لطفا رول را انتخاب نمایید.")]
        public string MainRole { get; set; }
        public string[] SubRoles { get; set; }
    }

    public class RegisterCustomeViewModel
    {
        [Phone(ErrorMessage = "لطفا شماره موبایل را به درستی وارد نمایید.")]
        [Display(Name = "موبایل *")]
        [Required(ErrorMessage = "لطفا شماره موبایل را وارد نمایید.")]
        [RegularExpression("\\d+", ErrorMessage = "لطفا فقط عدد وارد نمایید.")]
        public string Mobile { get; set; }
        [DataType(DataType.Password, ErrorMessage = "لطفا کلمه عبور را به درستی وارد نمایید.")]
        [Display(Name = "کلمه عبور *")]
        [Required(ErrorMessage = "لطفا کلمه عبور را وارد نمایید.")]
        [StringLength(100, ErrorMessage = "کلمه عبور حداقل باید {2} کاراکتر باشد.", MinimumLength = 6)]
        public string Password { get; set; }
        [DataType(DataType.Password, ErrorMessage = "لطفا کلمه عبور را به درستی وارد نمایید.")]
        [Display(Name = "تکرار کلمه عبور *")]
        [Compare("Password", ErrorMessage = "کلمه عبور و تکرار کلمه عبور یکسان نیستند.")]
        public string ConfirmPassword { get; set; }
    }

    public class RegisterEditViewModel
    {
        [Display(Name = "شناسه")]
        public string Id { get; set; }
        [Phone(ErrorMessage = "لطفا شماره موبایل را به درستی وارد نمایید.")]
        [Display(Name = "موبایل *")]
        [Required(ErrorMessage = "لطفا شماره موبایل را وارد نمایید.")]
        [RegularExpression("\\d+", ErrorMessage = "لطفا فقط عدد وارد نمایید.")]
        public string Mobile { get; set; }
        [Display(Name = "رول *")]
        [Required(ErrorMessage = "لطفا رول را انتخاب نمایید.")]
        public string MainRole { get; set; }
        public string[] SubRoles { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Phone(ErrorMessage = "لطفا شماره موبایل را به درستی وارد نمایید.")]
        [Display(Name = "موبایل *")]
        [Required(ErrorMessage = "لطفا شماره موبایل را وارد نمایید.")]
        [RegularExpression("\\d+", ErrorMessage = "لطفا فقط عدد وارد نمایید.")]
        public string Mobile { get; set; }
    }
}
