//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_Orderstatus
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public Nullable<int> SaleId { get; set; }
        public Nullable<System.DateTime> updatedate { get; set; }
    
        public virtual tbl_Sales tbl_Sales { get; set; }
    }
}
