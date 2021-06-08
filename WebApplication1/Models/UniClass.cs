using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class UniClass
    {

    }
    public partial class tbl_User
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public partial class tbl_Providers
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public partial class tbl_Products
    {
        public HttpPostedFileBase uploadimage { get; set; }

    }
    public partial class tbl_Sales
    {
        public int CVC { get; set; }
        public DateTime Expiry { get; set; }
        public string CardNo { get; set; }
    }
}