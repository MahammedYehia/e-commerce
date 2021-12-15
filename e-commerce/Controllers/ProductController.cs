using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using e_commerce.Models;
using System.IO;
using Microsoft.AspNet.Identity;
using PagedList;
using PagedList.Mvc;
using System.Net.Mail;
using Microsoft.AspNet.Identity.EntityFramework;

namespace e_commerce.Controllers
{
    [RoutePrefix("admin")]
    public class ProductController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Product/
       
        public ActionResult Index(int? page)
        {
            var products = db.Products.Include(p => p.Category);
           

            return View(products.OrderByDescending(x=>x.data)
                .ToList()
                .ToPagedList(page ?? 1, 3));
        }

        //public ActionResult productpartial()
        //{
        //    var productlist = db.Products.OrderBy(x => x.Category.CategoryName).ToList();

        //    return PartialView("_productpartial", productlist);
            
        //}

        // GET: /Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        public ActionResult Details2(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        // GET: /Product/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            ViewBag.Categoryid = new SelectList(db.Categories, "id", "CategoryName");
            return View();
        }

        // POST: /Product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
         [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {

                string path = Path.Combine(Server.MapPath("~/uploads"), upload.FileName);
                upload.SaveAs(path);
                product.Image = upload.FileName;

                product.userId = User.Identity.GetUserId();
                product.data = DateTime.Now;
                //Session["title"] = product.title;
                //Session["price"] = product.price;
                //Session["email"] = product.User.Email;


                Session["username"] = User.Identity.GetUserName();

                db.Products.Add(product);
            
                 db.SaveChanges();
                return RedirectToAction("Index");
                



                //foreach (var x in db.Users.Select(x => x.Email))
                //{
                //    var mail = new MailMessage();
                //    var Logininfo = new NetworkCredential("mahamedyehia469@gmail.com", "3031996@fcih");
                //    mail.From = new MailAddress((string)Session["email"]);
                //    mail.To.Add(new MailAddress(x));
                //    mail.Subject = product.title;
                //    mail.IsBodyHtml = true;
                //    string body = "sender name:" + Session["username"] + "<br>" +
                //        "sender" + "mahamedyehia469@gmail.com" + "<br>" +
                //        "product name:" + Session["title"] + "<br>" +
                //        "product price:" + Session["price"] + "<br>";

                //    mail.Body = body;

                //    var smtpClient = new SmtpClient("smtp.gmail.com", 587);
                //    smtpClient.EnableSsl = true;
                //    smtpClient.Credentials = Logininfo;

                //    smtpClient.Send(mail);

                //    return RedirectToAction("Index");

                //}

            }

            ViewBag.Categoryid = new SelectList(db.Categories.OrderBy(x => x.CategoryName), "id", "CategoryName", product.Categoryid);
            return View(product);
        }

        // GET: /Product/Edit/5
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
           
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.Categoryid = new SelectList(db.Categories, "id", "CategoryName", product.Categoryid);
            return View(product);
        }

        // POST: /Product/Edit/5
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                string oldpath = Path.Combine(Server.MapPath("~/uploads"), product.Image);
                if(upload !=null)
                {
                    System.IO.File.Delete(oldpath);

                        string path = Path.Combine(Server.MapPath("~/uploads"), upload.FileName);

                        upload.SaveAs(path);
                        product.Image = upload.FileName;

                }

                product.data = DateTime.Now;

                db.Entry(product).State = EntityState.Modified;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Categoryid = new SelectList(db.Categories, "id", "CategoryName", product.Categoryid);
            return View(product);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Search(string search)
        {
            var result = db.Products.Where(a => a.title.StartsWith(search)
                || a.MyProperty.StartsWith(search)
                || a.Category.CategoryName.StartsWith(search)).ToList();

            if (result.Count <= 0 || result == null)
            {
                return View(db.Products);
            }
            else
            {
                return PartialView("searchproduct",result);
            }
        }
        [HttpPost]
        public ActionResult sortbytitle()
        {
            var products = db.Products.Include(p => p.Category);


            return PartialView("sortproduct",products.OrderBy(x => x.title).ToList());
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
