using System.ComponentModel.DataAnnotations;
using System.Security;
using System.Web.Mvc;

namespace DataLayer
{
    public class ProductMetaData
    {
        [Key]
        public int ProductID { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(350)]
        public string Title { get; set; }

        [Display(Name = "توضیح مختصر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(500)]
        [DataType(DataType.MultilineText)]
        public string ShortDescription { get; set; }

        [Display(Name = "متن")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string Text { get; set; }

        [Display(Name = "قیمت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Price { get; set; }

        [Display(Name = "تاریخ ایجاد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DisplayFormat(DataFormatString = "{0: yyyy/MM/dd}")]
        public System.DateTime CreateDate { get; set; }

        [Display(Name = "تصویر")]
        public string ImageName { get; set; }
    }
    [MetadataType(typeof(ProductMetaData))]
    public partial class Product
    {
        
    }
}