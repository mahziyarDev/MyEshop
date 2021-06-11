using System.ComponentModel.DataAnnotations;

namespace DataLayer
{
    public class OrderDetailsMetaData
    {
    
    }

    [MetadataType(typeof(OrderDetailsMetaData))]
    public partial class OrderDetails
    {

    }
}