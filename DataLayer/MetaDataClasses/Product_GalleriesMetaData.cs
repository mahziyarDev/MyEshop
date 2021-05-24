﻿using System.ComponentModel.DataAnnotations;

namespace DataLayer
{
    public class Product_GalleriesMetaData
    {
        [Key]
        public int GalleryID { get; set; }
        [Display(Name = "کالا")]
        public int ProductID { get; set; }
        [Display(Name = "تصویر")]
        public string ImageName { get; set; }
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Title { get; set; }
    }
    [MetadataType(typeof(Product_GalleriesMetaData))]
    public partial class Product_Galleries
    {
        
    }
}