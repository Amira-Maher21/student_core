using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcDay_33.Models;
using PagedList;
namespace MvcDay_33.Controllers
{
    public class userController : Controller
    {
        mvcdbModel db = new mvcdbModel();
        // GET: user
        public ActionResult Index(int? pageno ,string sortedby,string search)
        {
            if (Session["userId"] != null)
            {
                int no = pageno == null ? 1 : pageno.Value;
                var users = db.users.OrderBy(n => n.Id);
                //if (string.IsNullOrEmpty(search))
                //{
                //    var users = db.users.OrderBy(n => n.Id);
                //}
                //else
                //{
                //    db.users.Where(n => n.Name.Contains(search)).OrderBy(n => n.Id);
                //}
                if (!string.IsNullOrEmpty(search))
                {
                    users = db.users.Where(n => n.Name.Contains(search)).OrderBy(n => n.Id);
                }
                ViewBag.sort = sortedby;
                ViewBag.search = search;
                switch (sortedby)
                {
                    case "Name":
                        users = users.OrderBy(n => n.Name);
                        break;
                    case "Email":
                        users = users.OrderBy(n => n.Email);
                        break;
                    default:
                        break;
                }
                return View(users.ToPagedList(no, 3));
            }
            else
            {
                return RedirectToAction("login", "operation");
            }
        }
        public ActionResult search(int? pageno, string sortedby, string search)
        {
            if (Session["userId"] != null)
            {
                int no = pageno == null ? 1 : pageno.Value;
                var users = db.users.OrderBy(n => n.Id);
                //if (string.IsNullOrEmpty(search))
                //{
                //    var users = db.users.OrderBy(n => n.Id);
                //}
                //else
                //{
                //    db.users.Where(n => n.Name.Contains(search)).OrderBy(n => n.Id);
                //}
                if (!string.IsNullOrEmpty(search))
                {
                    users = db.users.Where(n => n.Name.Contains(search)).OrderBy(n => n.Id);
                }
                ViewBag.sort = sortedby;
                ViewBag.search = search;
                switch (sortedby)
                {
                    case "Name":
                        users = users.OrderBy(n => n.Name);
                        break;
                    case "Email":
                        users = users.OrderBy(n => n.Email);
                        break;
                    default:
                        break;
                }
                return PartialView(users.ToPagedList(no, 3));
            }
            else
            {
                return RedirectToAction("login", "operation");
            }
        }
        public ActionResult create()
        {
            //IList<department> depts = db.departments.ToList();
            //SelectList st = new SelectList ( depts, "id", "name" );
            //  return ViewBag(st);

            ViewBag.dept = new SelectList(db.departments.ToList(), "id", "name");
            return View();
        }

        [HttpPost]
        public ActionResult create(user s, HttpPostedFileBase img)
        {
            if (ModelState.IsValid)
            {
                img.SaveAs(Server.MapPath($"~/attach/{img.FileName}"));
                s.Photo = img.FileName;



                db.users.Add(s);
                db.SaveChanges();
                return RedirectToAction("index");
            }
            else
            {
                ViewBag.dept = new SelectList(db.departments.ToList(), "id", "name");
                return View();
            }
        }

        public ActionResult edit(int Id)
        {
            user s = db.users.Find(Id);
            ViewBag.dept = new SelectList(db.departments.ToList(), "id", "name");

            return View(s);
        }
        [HttpPost]
        public ActionResult edit (user s ,HttpPostedFileBase Photo)
        {
            user us = db.users.Find(s.Id);
            if (Photo != null)
            {
                Photo.SaveAs(Server.MapPath($"~/attach/{Photo.FileName}"));
                us.Photo=Photo.FileName;
            }
            us.Name = s.Name;
            us.Email = s.Email;
            us.age = s.age;
            us.Address = s.Address;   
            us.deptId = s.deptId;
            db.SaveChanges();
            return RedirectToAction("index");

           
        }

        public ActionResult delete(int Id)
        {
            user s = db.users.Find(Id);
            db.users.Remove(s);
            db.SaveChanges();
            return RedirectToAction("index");
           
        }
        public ActionResult details (int Id)
        {
            ViewBag.user = db.users.Where(n => n.Id == Id).FirstOrDefault();
            return PartialView();
        }
    }
}