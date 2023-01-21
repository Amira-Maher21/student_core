using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcDay_33.Models;
namespace MvcDay_33.Controllers
{
    public class operationController : Controller
    {
        mvcdbModel db = new mvcdbModel();

        // GET: operation
        public ActionResult login()
        {
            if (Request.Cookies["MVCbroject"] !=null)
            {
                Session.Add("userId", Request.Cookies["MVCbroject"].Values["userId"]);
                return RedirectToAction("profile");
            }
            return View();
        }
        [HttpPost]
        public ActionResult login(user u,bool remberme)
        {
            user s = db.users.Where(n => n.Email == u.Email && n.Password == u.Password).FirstOrDefault();
            if (s!=null)
            {
                Session.Add("userId", s.Id);
                if (remberme == true)
                {
                    HttpCookie co = new HttpCookie("MVCbroject");
                    co.Values.Add("userId", s.Id.ToString());
                    co.Values.Add("Name", s.Name.ToString());
                    co.Expires = DateTime.Now.AddMonths(2);
                    Response.Cookies.Add(co);
                }

                return RedirectToAction("profile");
            }
            else
            {
                ViewBag.status = "incorrect email or password";     
            }
            return View();
        }
        public ActionResult profile ()
        {
            int Id = int.Parse( Session["userId"].ToString());
            return View(db.users.Find(Id));
        }
        public ActionResult logout()
        {
            Session["userId"] = null;
            HttpCookie c = new HttpCookie("MVCbroject");
            c.Expires = DateTime.Now.AddMonths(-1);
            Response.Cookies.Add(c);
            return RedirectToAction("login");
        }
    }
}