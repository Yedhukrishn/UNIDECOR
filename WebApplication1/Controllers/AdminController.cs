using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using WebApplication1.Models;
namespace WebApplication1.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        db_unidecorEntities db = new db_unidecorEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AdminHome()
        {
            return View();
        }
        public ActionResult AddCategorys()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCategorys( tbl_Category tblcat)
        {
            db.tbl_Category.Add(tblcat);
            db.SaveChanges();
            Response.Write("<script>alert('Data inserted successfully')</script>");
            return View();
        }
        public ActionResult ApproveProvider()
        {
            var model = db.tbl_Providers.Where(x => x.IsApprove == false).ToList();
            return View(model);
        }
        
        public ActionResult Approve(tbl_Providers tblprov ,int id)
        {
            tbl_Providers tblpv = db.tbl_Providers.Where(x=>x.LogId==id).SingleOrDefault();
            tblpv.IsApprove = true;
            db.SaveChanges();
            tbl_Login log = db.tbl_Login.Where(x => x.LogId == id).SingleOrDefault();
            log.IsApprove = true;
            db.SaveChanges();
            return RedirectToAction("ApproveProvider");
        }
        public ActionResult Reject(int id)
        {
            tbl_Providers tblpv = db.tbl_Providers.Where(x => x.LogId == id).SingleOrDefault();
            tblpv.IsApprove = false;
            db.SaveChanges();
            tbl_Login log = db.tbl_Login.Where(x => x.LogId == id).SingleOrDefault();
            log.IsApprove = false;
            db.SaveChanges();
            return RedirectToAction("ApproveProvider");
        }
        public ActionResult ViewUser()
        {
            var model = db.tbl_User.ToList();
            return View(model);
        }
        public ActionResult ViewProduct()
        {
            var model = db.tbl_Products.Where(x=>x.Isdelete==false).ToList();
            return View(model);
        }
        public ActionResult ViewProviders()
        {
            var model = db.tbl_Providers.ToList();
            return View(model);
        }
        public ActionResult Home()
        {
            return RedirectToAction("../Home/Home");
        }
    }
}