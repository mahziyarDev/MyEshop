using System.ComponentModel.DataAnnotations;

namespace DataLayer
{
    public class OrdersMetaData
    {
       
    }

    [MetadataType(typeof(OrdersMetaData))]
    public partial class Orders
    {

    }
}