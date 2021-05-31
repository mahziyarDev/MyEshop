using System.ComponentModel.DataAnnotations;

namespace DataLayer
{
    public class Product_commentsMetaData
    {
        
        public int CommentID { get; set; }
        public int ProductID { get; set; }

        [Display(Name = "نام ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Name { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمی باشد")]
        public string Email { get; set; }

        [Display(Name = "وبسایت ")]
        public string Website { get; set; }

        [Display(Name = "متن پیام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Comment { get; set; }
    }
    [MetadataType(typeof(Product_commentsMetaData))]
    public partial class Product_comments
    {

    }
}