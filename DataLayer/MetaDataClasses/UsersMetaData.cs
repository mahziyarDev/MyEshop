using System.ComponentModel.DataAnnotations;

namespace DataLayer
{
    public class UsersMetaData
    {

        public int UserID { get; set; }

        [Display(Name = "عنوان نقش")]
        public int RoleID { get; set; }

        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را پر کنید")]
        public string UserName { get; set; }

        [Display(Name = "ایمیل کاربر")]
        [Required(ErrorMessage = "لطفا {0} را پر کنید")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name ="رمز عبور")]
        [Required(ErrorMessage = "لطفا {0} را پر کنید")]
        public string Password { get; set; }
        [Display(Name ="کد فعالسازی")]
        public string ActiveCode { get; set; }

        [Display(Name ="وضعیت کاربر")]
        public bool IsActive { get; set; }
        
        [Display(Name ="تاریخ ثبت نام")]
        public System.DateTime RegisterDate { get; set; }
    }
    [MetadataType(typeof(UsersMetaData))]
    public partial class Users
    {
        
    }
}