using System.ComponentModel.DataAnnotations;

namespace DataLayer
{
    public class SliderMetaData
    {
        [Key]
        public int SlideID { get; set; }

        [Display(Name = "عنوان")]
        public string Title { get; set; }

        [Display(Name = "تصویر")]
        public string ImageName { get; set; }

        [Display(Name = "تاریخ شروع")]
        [DisplayFormat(DataFormatString = "{0: yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public System.DateTime StartDate { get; set; }

        [Display(Name = "تاریخ پایان")]
        [DisplayFormat(DataFormatString = "{0: yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public System.DateTime EndDate { get; set; }

        [Display(Name = "وضعیت")]
        public bool IsActive { get; set; }

        [Display(Name = "ادرس اینترنتی")]
        public string Link { get; set; }
    }

    [MetadataType(typeof(SliderMetaData))]
    public partial class Slider
    {

    }
}