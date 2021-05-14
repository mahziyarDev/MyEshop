using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;

namespace DataLayer.ViewModel
{
    public class RegisterViewModel
    {
        [Display(Name = "نام کاربر")]
        [Required(ErrorMessage = "لطفا {0} را پر کنید")]
        public string UserName { get; set; }

        [Display(Name = "ایمیل کاربر")]
        [Required(ErrorMessage = "لطفا {0} را پر کنید")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده صحیح نمی باشد")]
        public string Email { get; set; }

        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "لطفا {0} را پر کنید")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "تکرار رمز عبور")]
        [Required(ErrorMessage = "لطفا {0} را پر کنید")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage = "کلمه های عبور مغایرت دارند")]
        public string RePassword { get; set; }
    }

    public class LoginViewModel
    {
        [Display(Name = "ایمیل کاربر")]
        [Required(ErrorMessage = "لطفا {0} را پر کنید")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده صحیح نمی باشد")]
        public string Email { get; set; }

        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "لطفا {0} را پر کنید")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "مرا به خاطر بسپار")]
        public bool RememberMe { get; set; }
    }
    public class ForgotPassword
    {
        [Display(Name = "ایمیل کاربر")]
        [Required(ErrorMessage = "لطفا {0} را پر کنید")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده صحیح نمی باشد")]
        public string Email { get; set; }
    }

    public class RecoveryPasswordViewModel
    {
        [Display(Name = " رمز عبور جدید")]
        [Required(ErrorMessage = "لطفا {0} را پر کنید")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "تکرار رمز عبور جدید")]
        [Required(ErrorMessage = "لطفا {0} را پر کنید")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="کلمه های عبو با هم مغایرت دارند")]
        public string RePassword { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Display(Name = "کلمه عبور فعلی")]
        [Required(ErrorMessage = "لطفا {0} را پر کنید")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [Display(Name = " رمز عبور جدید")]
        [Required(ErrorMessage = "لطفا {0} را پر کنید")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "تکرار رمز عبور جدید")]
        [Required(ErrorMessage = "لطفا {0} را پر کنید")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "کلمه های عبو با هم مغایرت دارند")]
        public string RePassword { get; set; }
    }
}