using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class UserController : Controller
    {
        db_unidecorEntities db = new db_unidecorEntities();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult UserRegistraton()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UserRegistraton( tbl_User tblusr)
        {
            tbl_Login tbllogin = new tbl_Login();
            tbllogin.Username = tblusr.Username;
            tbllogin.Password = tblusr.Password;
            tbllogin.IsApprove = true;
            tbllogin.CreateDate = DateTime.Today;
            tbllogin.Isdelete = false;
            tbllogin.IsApprove = true;
            tbllogin.role = 3;
            db.tbl_Login.Add(tbllogin);
            db.SaveChanges();
            int logid = db.tbl_Login.Max(x => x.LogId);
            tblusr.IsDelete = false;
            tblusr.CreateDate = DateTime.Today;
            tblusr.LogId = logid;
            db.tbl_User.Add(tblusr);
            db.SaveChanges();
            Response.Write("<script>alert('Registered successfully')</script>");
            return RedirectToAction("../Home/Login");
           
        }
        public ActionResult Userhome()
        {
            return View();
        }
        public ActionResult ViewProducts()
        {
            ViewBag.Category = db.tbl_Category.ToList(); 
            var model = db.tbl_Products.Where(x => x.Isdelete == false).ToList();
            return View(model);
        }
        public ActionResult ViewProductDetails(int id, int price, string ProductName)
        {
            ViewBag.GalleryItems = db.tbl_Products.Where(x => x.ProductId == id).ToList();
            ViewBag.Item = db.tbl_Products.Where(x => x.ProductId == id).ToList();
           
            var model = db.tbl_Products.Where(x => x.ProductId == id).SingleOrDefault();
            Session["Productid"] = model.ProductId;
            Session["Productname"] = model.ProductName;
            Session["ProvId"] = model.ProviderID;
            return View();
        }
        [HttpPost]
        public ActionResult ViewProductDetails(tbl_Cart tblcrt, int id, string ProductName)
        {
            ViewBag.GalleryItems = db.tbl_Products.Where(x => x.ProductId == id).ToList();
            ViewBag.Item = db.tbl_Products.Where(x => x.ProductId == id).ToList();
            tblcrt.CreateDate = DateTime.Today;
            tblcrt.IsRemove = false;
            tblcrt.ProductId =Convert.ToInt32(Session["Productid"]);
            tblcrt.ProductName =Session["Productname"].ToString();
            tblcrt.ProvId = Convert.ToInt32(Session["ProvId"]);
           
            int  ids = Convert.ToInt32(Session["LogID"]);
            int prices = Convert.ToInt32(tblcrt.Quantity * tblcrt.Price);
            var data = db.tbl_User.Where(x => x.LogId == ids).SingleOrDefault();
            tblcrt.UserId = data.UserId;
            Session["uid"]= data.UserId;
            tblcrt.Price = prices;
            db.tbl_Cart.Add(tblcrt);
            db.SaveChanges();
            return RedirectToAction("ViewCart");
        }
        public ActionResult ViewCart()
        {
            int UserId = Convert.ToInt32(Session["uid"]);

           var model = db.tbl_Cart.Where(x => x.UserId == UserId&&x.IsRemove==false).ToList();
           return View(model);
        }
        public ActionResult Payment()
        {
            int UserId = Convert.ToInt32(Session["uid"]);
            ViewBag.AdrsDetails = db.tbl_User.Where(x => x.UserId == UserId ).ToList();

            return View();
        }
        [HttpPost]
        public ActionResult Payment(tbl_Sales sal,int id)
        {
           int  logid = Convert.ToInt32(Session["LogID"]);
           var datas = db.tbl_Cart.Where(x => x.CartId == id).SingleOrDefault();
           var model = db.tbl_User.Where(x => x.LogId == logid).SingleOrDefault();
           int userid = model.UserId;
           Session["userid"] = model.UserId;
           var data = db.tbl_bank.Where(x => x.UserId == userid).ToList();
           if (data.Count > 0)
           {
               if (data[0].CardNo == sal.CardNo && data[0].CVC == sal.CVC)
               {
                   if (data[0].Balance > datas.Price)
                   {
                       sal.CreateDate = DateTime.Today;
                       sal.Orderstatus = true;
                       sal.PayStatus = true;
                       sal.Quantity = datas.Quantity;
                       sal.Price = datas.Price;
                       sal.TotalAmount = datas.Price;
                       sal.UserId = model.UserId;
                       sal.ProvId = datas.ProvId;
                       sal.ProductId = datas.ProductId;
                       db.tbl_Sales.Add(sal);
                       db.SaveChanges();
                       tbl_Cart tbcrt = new tbl_Cart();
                       tbcrt.IsRemove = true;
                       db.SaveChanges();
                       tbl_Products tblprdt = new tbl_Products();
                       int qnty =Convert.ToInt32(tblprdt.Quantity);
                       tblprdt.Quantity = qnty - 1;
                       db.SaveChanges();
                       int salid = db.tbl_Sales.Max(x => x.SalesId);
                       tbl_Orderstatus tborder = new tbl_Orderstatus();
                       tborder.updatedate = DateTime.Today;
                       tborder.Status = "Order Placed";
                       db.tbl_Orderstatus.Add(tborder);
                       db.SaveChanges();
                       TempData["Successmesssage"] = "Payment Sucessfull";

                       return RedirectToAction("Invoice");

                   }
                   else
                   {
                       Response.Write("<script>alert('Please Check Your Account Details')</script>");

                   }
               }
           }
            return View();
        }
        public ActionResult Invoice(tbl_Sales tbsl)
        {
            int saleid = db.tbl_Sales.Max(x => x.SalesId);
            var model = db.tbl_Sales.Where(a => a.SalesId == saleid).SingleOrDefault();
            ViewBag.SalesDetails = db.tbl_Sales.Where(a => a.SalesId == saleid).ToList();
            return View(model);
        }
        public ActionResult Home()
        {
            return View();
        }
        public ActionResult Logout()
        {
            return RedirectToAction("../Home/Home");
        }
        public ActionResult RemoveFromcart(int id)
        {
            tbl_Cart tblc = db.tbl_Cart.Find(id);
            tblc.IsRemove = true;
            db.SaveChanges();
            return RedirectToAction("ViewCart");
        }
        public ActionResult PurchaseReport()
        {
            int usid = Convert.ToInt32( Session["uSER"]);
            var model = db.tbl_Sales.Where(x => x.UserId == usid).ToList();
            ViewBag.SalesDetails = db.tbl_Sales.Where(x => x.ProvId == usid).ToList();

            return View(model);
        }
        public ActionResult ItemCategories()
        {
            List<tbl_Category> Model = db.tbl_Category.ToList();

            return View(Model);
            
        }
        public ActionResult ItemsByCategory(int? categoryID)
        {
            ViewBag.Category = db.tbl_Category.ToList();
            
            var model = db.tbl_Products.Where(x => x.CategoryId == categoryID && x.Isdelete == false).ToList();
            return View(model);
        }
       
    }
}