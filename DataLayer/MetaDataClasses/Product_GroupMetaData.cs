using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Product_GroupsMetaData
    {
        [Key]
        public int GroupID { get; set; }

        [Display(Name = "عنوان گروه")]
        [Required(ErrorMessage = "لطفا {0} را پر کنید")]
        public string GroupTitle { get; set; }
        public Nullable<int> ParentID { get; set; }
    }

    [MetadataType(typeof(Product_GroupsMetaData))]
    public partial class Product_Groups
    {

    }
}
