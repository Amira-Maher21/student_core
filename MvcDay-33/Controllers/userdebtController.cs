using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcDay_33.Models;
namespace MvcDay_33.Controllers
{
    public class userdebtController : Controller
    {
        mvcdbModel db = new mvcdbModel();

        // GET: userdebt
        public ActionResult Index()
        {
            ViewBag.debt = new SelectList(db.departments.ToList(), "Id", "Name");
            return View();
        }
        public ActionResult userByDept(int? deptId)
        {
            List<user> us = new List<user>();

          if (deptId !=null)
          us = db.users.Where(n => n.deptId == deptId).ToList();
          else
                us = db.users.ToList();

            return PartialView(us);
        }
        public ActionResult alldept ()
        {
            ViewBag.dept = db.departments.ToList();
            return PartialView();
        }

        public ActionResult deptdetails (int Id)
        {
            IList<user>  us = db.users.Where(n => n.deptId == Id).ToList();
            return View();
        }


    }
}