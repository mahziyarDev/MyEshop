using System.ComponentModel.DataAnnotations;
using DataLayer;

namespace DataLayer
{
    public class Product_Selected_GroupsMetaData
    {
        public int PG_ID { get; set; }
        public int ProductID { get; set; }
        public int GroupID { get; set; }

    }

    [MetadataType(typeof(Product_Selected_GroupsMetaData))]
    public partial class Product_Selected_Groups
    {
    
    }
}