using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        db_unidecorEntities db = new db_unidecorEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Home()
        {
            return View();
        }
        public ActionResult UserRegistration()
        {
            return RedirectToAction("../User/UserRegistraton");
        }
        public ActionResult ProviderRegistration()
        {
            return RedirectToAction("../Providers/ProviderRegistration");
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(tbl_Login tbllog)
        {
            var log = db.tbl_Login.Where(x => x.Username == tbllog.Username && x.Password == tbllog.Password).ToList();


           
            if (log.Count>0)
            {

                Session["LogID"] = log[0].LogId;
                int id = log[0].LogId;
               
                if (log[0].IsApprove == true)
                {

                    if (log[0].role == 1)
                    {
                        return RedirectToAction("../Admin/ViewProduct");

                    }
                    if (log[0].role == 2)
                    {
                        return RedirectToAction("../Providers/ViewProduct");

                    }
                    if (log[0].role == 3)

                    {
                        var datas = db.tbl_User.Where(x => x.LogId == id).SingleOrDefault();
                        Session["uSER"] = datas.UserId;
                        return RedirectToAction("../User/ViewProducts");

                    }

                }
                else
                {
                    return RedirectToAction("Login");
                }
               
            }
            return View();
        }
    }
}