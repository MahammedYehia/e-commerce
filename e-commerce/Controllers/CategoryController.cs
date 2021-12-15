using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using e_commerce.Models;
using PagedList;
using PagedList.Mvc;
namespace e_commerce.Controllers
{  
    public class CategoryController : Controller
    {
        
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Category/
        public ActionResult Index()
        {

            return View(db.Categories.ToList());


        }
        //public ActionResult showallcategory()
        //{ 
        //    var contacts = db.Categories.OrderBy(x => x.CategoryName).Select(x => new
        //    {
        //        id = x.id,

        //        CategoryName = x.CategoryName,


        //    }).ToList();


        //    return Json(contacts, JsonRequestBehavior.AllowGet);
        //}
        public JsonResult TheNameIsExit(String CategoryName)
        {
            return Json(!db.Categories.Any(x => x.CategoryName == CategoryName), JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult categorypartial()
        {

            var categorylist = db.Categories.OrderBy(x => x.CategoryName).ToList();
            return PartialView(categorylist);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Details(int id)
        {
           
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            //return View(category);

            //List<Category> listEmp = db.Categories.Where(x => x.id == id).Select(x => new Category { CategoryName=x.CategoryName }).ToList();
            //ViewBag.listofcat = listEmp;
            return PartialView("_partial1", category);
        }

        // GET: /Category/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

      [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Create([Bind(Include="id,CategoryName")] Category category)
        {
            if (db.Categories.Any(x => x.CategoryName == category.CategoryName))
            {
                //return Json(new { Status = 0, Message = "comment faild" }, JsonRequestBehavior.AllowGet);

                ModelState.AddModelError("CategoryName", category.CategoryName + " role already exit");
            }
           
            else if (ModelState.IsValid)
            {
                //var product = new Category();
                //product.id =category.id;

                Session["id"] = category.id;
                db.Categories.Add(category);
               
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(category);
        }

        // GET: /Category/Edit/5
         [Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: /Category/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Edit([Bind(Include="id,CategoryName")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }


        //public ActionResult Delete(int id)
        //{
           
        //    Category category = db.Categories.Find(id);
        //    db.Categories.Remove(category);
        //    int val=db.SaveChanges();
        //    if (val > 0)
        //    {
        //        return Json(new { State = 1, message = "delete succuss" }, JsonRequestBehavior.AllowGet);
        //    }
        //    else {
        //        return Json(new { State = 0, message = "delete fails" }, JsonRequestBehavior.AllowGet);
        //    }
        //}

       // POST: /Category/Delete/5
         [Authorize(Roles = "admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
