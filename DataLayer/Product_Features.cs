//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class Product_Features
    {
        public int PF_ID { get; set; }
        public int ProductID { get; set; }
        public int FeatureID { get; set; }
        public string Value { get; set; }
    
        public virtual Features Features { get; set; }
        public virtual Product Product { get; set; }
    }
}
