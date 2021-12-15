using e_commerce.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace e_commerce.Controllers
{
    public class UserController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View(db.Users.Where(a => !a.UserType.Contains("admin")).ToList());
        }
        public ActionResult adminprofile()
        {
            return View(db.Users.Where(a => a.UserType.Contains("admin")).ToList());
        }
        public ActionResult MakeBlock(string id)
        {

            var x = db.Users.SingleOrDefault(a => a.Id == id).Isblocked=1;

            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DeleteBlock(string id)
        {

            var x = db.Users.SingleOrDefault(a => a.Id == id).Isblocked = 0;

            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //[HttpPost]
        //public ActionResult BlockCustomer(string user_id, int BlockDuration)
        //{
        //    //var userId = User.Identity.GetUserId();

        //    var result = false;
        //    var customer_selected = db.Users.SingleOrDefault(x => x.Id == user_id);
        //    if (customer_selected != null)
        //    {
        //        if (BlockDuration != 0)
        //        {
        //            History_OfBlock HB = new History_OfBlock
        //            {
        //                customer_id = customer_selected.Id,
        //                block_duration = BlockDuration,
        //                end_block = @DateTime.Now.AddDays(BlockDuration)
        //            };
        //            db.History_OfBlocks.Add(HB);
        //            db.SaveChanges();

        //            customer_selected.Isblocked = db.History_OfBlocks.Max(c => c.Id);
        //        }
        //        else
        //        {

        //            customer_selected.Isblocked = 0;
        //        }
        //        try
        //        {
        //            db.SaveChanges();
        //        }
        //        catch
        //        {
        //            //whdyhb
        //        }
        //        result = true;
        //    }
        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}



        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
 public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser product = db.Users.Find(id);

            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserType = new SelectList(db.Roles.ToList(),
               "Name", "Name");
            ViewBag.gender = new SelectList(new[] { "female", "male" }); return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ApplicationUser product, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                string oldpath = Path.Combine(Server.MapPath("~/uploads"), product.Image);
                if (upload != null)
                {
                    System.IO.File.Delete(oldpath);

                    string path = Path.Combine(Server.MapPath("~/uploads"), upload.FileName);

                    upload.SaveAs(path);
                    product.Image = upload.FileName;

                }


                db.Entry(product).State = EntityState.Modified;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserType = new SelectList(db.Roles.ToList(),
                  "Name", "Name");
            ViewBag.gender = new SelectList(new[] { "female", "male" }); 
            return View(product);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
   
        public ActionResult DeleteConfirmed(string Id)
        {


            foreach (var product1 in db.Likes.Where(x => x.userId == Id))
            {
                db.Likes.Remove(product1);

            }
            foreach (var product2 in db.favourites.Where(x => x.userId == Id))
            {
                db.favourites.Remove(product2);

            }
            ApplicationUser product = db.Users.Find(Id);

            db.Users.Remove(product);

            db.SaveChanges();
            return RedirectToAction("Index");
        }






    }
}
