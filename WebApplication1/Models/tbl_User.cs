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
    
    public partial class tbl_User
    {
        public tbl_User()
        {
            this.tbl_Cart = new HashSet<tbl_Cart>();
            this.tbl_Sales = new HashSet<tbl_Sales>();
        }
    
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public Nullable<int> LogId { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<bool> IsDelete { get; set; }
    
        public virtual tbl_Login tbl_Login { get; set; }
        public virtual ICollection<tbl_Cart> tbl_Cart { get; set; }
        public virtual ICollection<tbl_Sales> tbl_Sales { get; set; }
    }
}
