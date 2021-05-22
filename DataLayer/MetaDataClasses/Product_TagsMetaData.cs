using System.ComponentModel.DataAnnotations;

namespace DataLayer
{
    public class Product_TagsMetaData
    {

        public int TagID { get; set; }

        public int ProductID { get; set; }

        public string Tag { get; set; }
    }
    [MetadataType(typeof(Product_TagsMetaData))]
    public partial class Product_Tags
    {
        
    }
}