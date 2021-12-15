using e_commerce.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace e_commerce.Controllers
{
    [Authorize(Roles = "admin")]
    public class RoleController : Controller
    {
         ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View(db.Roles.ToList());
        }
        //[HttpPost]
        //public ActionResult Deleteselected(IEnumerable<int> roletodelete)
        //{
        //    var s= db.Roles.Where(x => roletodelete.Contains(x.Id)).ToList()

        //        .ForEach(db.Roles.Remove(s));
        //    db.SaveChanges();
        //    return RedirectToAction("Index");

        //}
        //
        // GET: /Role/Details/5
        //[NonAction]
        public ActionResult Details(String id)
        {
            var role = db.Roles.Find(id);
            if(role==null)
            {

                return HttpNotFound();
            }

            return View(role);
        }

        //
        // GET: /Role/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Role/Create
        [HttpPost]
        public ActionResult Create(IdentityRole role)
        {
            if (db.Roles.Any(x => x.Name == role.Name))
            {
                ModelState.AddModelError("Name", role.Name + " role already exit");
            }
            
            else if (ModelState.IsValid)
            {
                db.Roles.Add(role);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
           
            return View(role);
           
        }

        //
        // GET: /Role/Edit/5
        public ActionResult Edit(String id)
        {
            var role = db.Roles.Find(id);
            if(role==null)
            {
                return HttpNotFound();

            }

            return View(role);
        }

        //
        // POST: /Role/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include="Id,Name")]IdentityRole role)
        {
            if(ModelState.IsValid)
            {
                db.Entry(role).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(role);
           
        }

        //
        // GET: /Role/Delete/5
        //public ActionResult Delete(String id)
        //{
        //    var role = db.Roles.Find(id);
        //    if (role == null)
        //    {
        //        return HttpNotFound();

        //    }

        //    return View(role);
        //}

        ////
        // POST: /Role/Delete/5
        //[HttpPost]
        //public ActionResult Delete(IdentityRole role)
        //{
        //    var myrole = db.Roles.Find(role.Id);
        //    db.Roles.Remove(myrole);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");

        //}
        [HttpGet]
        public ActionResult Delete(string id)
        {
            var myrole = db.Roles.Single(s => s.Id == id);
            db.Roles.Remove(myrole);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}
