using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcDay_33.Models;
namespace MvcDay_33.Controllers
{
    public class userdataController : Controller
    {
        mvcdbModel db = new mvcdbModel();

        // GET: userdata
        public ActionResult Index()
        {
            return View(db.users.ToList());
        }

        public ActionResult details(int Id)
        {
            user u = db.users.Where(n => n.Id == Id).FirstOrDefault();
            ViewBag.st=u;
            return PartialView();
        }
        public ActionResult check ( string email)
        {
            user s = db.users.Where(n => n.Email == email).FirstOrDefault();
            if (s==null)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
    }
}