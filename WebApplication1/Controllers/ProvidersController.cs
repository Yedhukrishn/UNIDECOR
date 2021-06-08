using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
namespace WebApplication1.Controllers
{
    public class ProvidersController : Controller
    {
        // GET: Providers
        db_unidecorEntities db = new db_unidecorEntities();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ProviderRegistration()
        {
            ViewBag.Idproof = db.tbl_IdProof.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult ProviderRegistration(tbl_Providers tblprov)
        {
            ViewBag.Idproof = db.tbl_IdProof.ToList();

            tbl_Login tbllog = new tbl_Login();
            tbllog.Username = tblprov.Username;
            tbllog.Password = tblprov.Password;
            tbllog.CreateDate = DateTime.Today;
            tbllog.IsApprove =false;
            tbllog.Isdelete = false;
            tbllog.role = 2;
            db.tbl_Login.Add(tbllog);
            db.SaveChanges();
            int logid = db.tbl_Login.Max(x => x.LogId);
            tblprov.LogId = logid;
            tblprov.CreateDate = DateTime.Today;
            tblprov.IsDelete = false;
            tblprov.IsApprove = false;
            db.tbl_Providers.Add(tblprov);
            db.SaveChanges();
            Response.Write("<script>alert('Registered successfully')</script>");

            return RedirectToAction("../Home/Login");
        }
        public ActionResult ProvidersHome()
        {
            return View();
        }
        public ActionResult AddProduct()
        {
            ViewBag.Category = db.tbl_Category.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult AddProduct( tbl_Products  tblprdct)
        {
            int logid =Convert.ToInt32 (Session["LogID"]);
            var data = db.tbl_Providers.Where(x => x.LogId == logid).SingleOrDefault();
            int provid = data.ProvId;
            tblprdct.Isdelete = false;
            tblprdct.Createdate = DateTime.Today;
            tblprdct.ProviderID = provid;
            var pic = "../Content/Products/" + tblprdct.uploadimage.FileName;
            tblprdct.uploadimage.SaveAs(Server.MapPath(pic));
            tblprdct.Image = "/Content/Products/" + tblprdct.uploadimage.FileName;
            db.tbl_Products.Add(tblprdct);
            db.SaveChanges();
            ViewBag.Category = db.tbl_Category.ToList();
            Response.Write("<script>alert('Product Added')</script>");

            return View();
        }
        public ActionResult ViewProduct()
        {
            int logid =Convert.ToInt32 (Session["LogID"]);
            var data = db.tbl_Providers.Where(x => x.LogId == logid).SingleOrDefault();
            int provid = data.ProvId;
            Session["provid"] = data.ProvId;
            var model = db.tbl_Products.Where(x => x.ProviderID == provid&& x.Isdelete==false).ToList();
            return View(model);
        }
        public ActionResult AddToCart()
        {
            return View();
        }
        public ActionResult SalesReport()
        {
            return View();
        }
        public ActionResult TodayReport( )
        {
            int pvid = Convert.ToInt32(Session["provid"]);
            var model = db.tbl_Sales.Where(x => x.ProvId ==pvid).ToList();
            ViewBag.SalesDetails = db.tbl_Sales.Where(x => x.ProvId == pvid).ToList();

            return View(model);
        }
        public ActionResult Logout()
        {
            return RedirectToAction("../Home/Home");
        }
        public ActionResult EditProduct(int id)
        {

            var model = db.tbl_Products.Find(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult EditProduct(tbl_Products tblprdct,int id)
        {
            tbl_Products tblp = db.tbl_Products.Find(tblprdct.ProductId);
            tblp.Quantity = tblprdct.Quantity;
            tblp.Price = tblprdct.Price;
            db.SaveChanges();
            return RedirectToAction("ViewProduct");
        }
        public ActionResult RemoveProduct(int id)
        {
            tbl_Products tbl = db.tbl_Products.Find(id);
            tbl.Isdelete = true;
            db.SaveChanges();
            return RedirectToAction("ViewProduct");
        }
    }
}