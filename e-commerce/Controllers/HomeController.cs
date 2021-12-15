using e_commerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using PagedList;
using PagedList.Mvc;
using e_commerce;
using System.Threading;
using System.Globalization;
using Twilio;
using Twilio.Types;
using Twilio.Rest.Api.V2010.Account;

namespace e_commerce.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Change(String lang)
        {

            Session["lang"] = lang;
            Thread.CurrentThread.CurrentCulture = new CultureInfo(Session["lang"].ToString());
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Session["lang"].ToString());


            return RedirectToAction("Index");
        }
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Category);


            return View(products.OrderByDescending(x => x.data).ToList());

        }

        /// //////////////////////Related Products/////////////////////////////////////////

        public ActionResult Related_Product()
        {
             var productId = (int)Session["productid"];
             var result = db.Products.SingleOrDefault(x => x.id == productId);
             var result2 = db.Products.Where(x => x.MyProperty.Contains(result.MyProperty)).ToList();

             return PartialView("Related_Product",result2);
        }
        public ActionResult most_Product()
        {
            var result = db.OrderDetails.OrderByDescending(x => x.Quantity);

            return PartialView("most_Product", result.ToList());
        }
        /// ////////////////////////////////////////////////////////////
        [HttpGet]
        public ActionResult SendSMS()
        {
            return View();
        }


        [HttpPost]
        public ActionResult SendSMS(SMSForm sms)
        {
            if (ModelState.IsValid)
            {
                // Find your Account Sid and Auth Token at twilio.com/console
                const string accountSid = "AC03f5f44696234801eda82c8a96284f70";
                const string authToken = "6dea2e1c3d8aa3d3e9600d6bc117de00";

                TwilioClient.Init(accountSid, authToken);

                var to = new PhoneNumber("+2" + sms.Number);

                var message = MessageResource.Create(
                    to,
                  
                    from: new PhoneNumber("+201278841277"), //  From number, must be an SMS-enabled Twilio number ( This will send sms from ur "To" numbers ). 
                    body: "Hello" + sms.Name + " !! Welcome to Twilio SMS API example in ASP.NET MVC!!");


                ViewBag.Message = "Message Sent";
                return RedirectToAction("SendSMS");
            }
            else
            {
                return View();
            }
        }
        /// ////////////////////////////send sms////////////////////////////////
        

        /// ////////////////////////////////////////////////////////////


        //public JsonResult GetNotifications()
        //{
        //    var notificationRegisterTime = Session["LastUpdated"] != null ? Convert.ToDateTime(Session["LastUpdated"]) : DateTime.Now;
        //    NotificationComponent NC = new NotificationComponent();
        //    var list = NC.GetData(notificationRegisterTime);

        //    //update session here for get only new added contacts (notification)
        //    Session["LastUpdate"] = DateTime.Now;
        //    return new JsonResult { Data = list, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        //}


        [Authorize]
        public ActionResult Details(int productid)
        {
            var product = db.Products.Find(productid);
            if (product == null)
            {
                return HttpNotFound();
            }
            Session["productid"] = productid;
           
            var userId = User.Identity.GetUserId();
            var username = User.Identity.GetUserName();
            Session["UserName"] = username;
            Session["Userid"] = userId;

            ViewBag.like = db.favourites.Where(n => n.userId == userId && n.productId == productid).Count();
            return View(product);
        }

        public ActionResult catsearch(int Id,int? page)
        {
            var result = db.Products.Where(a => a.Categoryid ==Id).ToList().ToPagedList(page ?? 1, 3);
            ViewBag.name = result;
            return View(result);
        }

     
        [Authorize]
        public ActionResult supmitComment(string post_txt)
        {
            var userId = User.Identity.GetUserId();
           

            var productId = (int)Session["productid"];
            var product = new like();
            product.userId = userId;
            product.productId = productId;
            product.post_txt = post_txt;

           product.data = DateTime.Now;

            db.Likes.Add(product);
           var x= db.SaveChanges();

           if (x > 0)
           {
               return Json(product, JsonRequestBehavior.AllowGet);
           }
           else
           {
               return Json(new { Status = 0, Message = "comment faild" }, JsonRequestBehavior.AllowGet);
           }
             
        }
        //public ActionResult returnchat(string post_txt)
        //{
        //    var pro = (int)Session["productid"];
        //    var userId = User.Identity.GetUserId();

        //    var x = db.Likes.Where(n => n.productId == pro).ToList();
        //    return PartialView("returnchat", x);
        //}
        public ActionResult showallcomment()
        {
            var pro = (int)Session["productid"];
            var userId = User.Identity.GetUserId();

            var contacts = db.Likes.Where(n => n.productId == pro).Select(x => new
            {

                postId = x.postId,
                post_txt = x.post_txt,
                userId = x.userId,
                username=x.User.UserName,
                userimage=x.User.Image
                //productId = x.productId

            }).ToList();


            return Json(contacts, JsonRequestBehavior.AllowGet);  

        }

        [Authorize(Roles="admin")]
        public ActionResult CategoryTotal()
        {

            var Cat= db.Products.Include(x => x.Categoryid)
                .GroupBy(t => t.Category.CategoryName)
                .Select(y => new CategoryTotalinProduct
                {
                    categoryname = y.Key,
                    Total = y.Count()
                }).ToList().OrderByDescending(y => y.Total);


            return View(Cat);
        }

    


        public ActionResult addfavourite(favourite product)
        {

            var userId = User.Identity.GetUserId();
            var productId = (int)Session["productid"];

            var check = db.favourites.Where(a => a.productId == productId && a.userId == userId).ToList();

            if (check.Count < 1)
            {
                product.userId = userId;
                product.productId = productId;
                product.data = DateTime.Now;
                db.favourites.Add(product);
                db.SaveChanges();
                ViewBag.fav = product.productId;
                return Json(new { success = true, message = "Submitted Successfully" }, JsonRequestBehavior.AllowGet);
                // ViewBag.Result = "succuss added";
                //return RedirectToAction("Index");
                
             }
            else {
                return Json(new { success = false, message = "Submitted false" }, JsonRequestBehavior.AllowGet);

            }
        }
        //public ActionResult getfavourite()
        //{
        //    var pro = (int)Session["productid"];
        //    var userId = User.Identity.GetUserId();

        //    var contacts = db.favourites.Where(n => n.productId == pro && n.userId==userId).Select(x => new
        //    {

        //        favId = x.favId,
        //        userId = x.User.Id,
        //        data = x.data,
                

        //    }).ToList();

        //    ViewBag.like = db.favourites.Where(n => n.userId == userId && n.productId == pro).Count();

        //    return Json(new { success = true, message = "Submitted Successfully" }, JsonRequestBehavior.AllowGet);

        //}


        public ActionResult Deletefav(int prodid)
        {


            var userId = User.Identity.GetUserId();

            bool status = false;
            using (ApplicationDbContext dc = new ApplicationDbContext())
            {
                var v = dc.favourites.Where(a => a.productId == prodid && a.userId==userId).FirstOrDefault();
                if (v != null)
                {
                    dc.favourites.Remove(v);
                    dc.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }
        public ActionResult numberoflike()
        {     
        var prod = (int)Session["productid"];

        ViewBag.number = db.favourites.Where(x => x.productId == prod).Count();
        return PartialView("numberoflike");
        }

        public ActionResult userfav()
        { 
            var userId = User.Identity.GetUserId();
            return View(db.favourites.Where(x =>x.userId==userId).ToList());
          }

        [Authorize]
        public ActionResult getproductbyuser2()
        {
            var user = User.Identity.GetUserName();
            var prod = db.OrderDetails.Where(a => a.Order.Username == user).ToList();
            ViewBag.check = prod;
            return View(prod);
        }


        public ActionResult productpartial()
        {
            var productlist = db.Products.OrderBy(x => x.Category.CategoryName).ToList();

            return PartialView("productpartial", productlist);

        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contact (ContactUs contact)
        {
            var mail = new MailMessage();
            var Logininfo = new NetworkCredential("mahamedyehia469@gmail.com","3031996@fcih");
            mail.From = new MailAddress(contact.Email);
            mail.To.Add(new MailAddress("mahamedyehia469@gmail.com"));
            mail.Subject = contact.Subject;
            mail.IsBodyHtml = true;
            string body = "sender name:" + contact.Name + "<br>" +
                "sender Email:" + contact.Email + "<br>" +
                "sender Subject:" + contact.Subject + "<br>" +
                "sender Message:" + contact.Message + "<br>";

            mail.Body = body;

            var smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = Logininfo;
            smtpClient.Send(mail);

            return RedirectToAction("Index");
        }
        public ActionResult Search()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Search(string searchName=null)
        {
            var result = db.Products.Where(a => a.title.StartsWith(searchName)
                || a.MyProperty.StartsWith(searchName)
                || a.Category.CategoryName.StartsWith(searchName)).ToList();

            if(result.Count <= 0 || result == null)
            {
                return View(db.Products);
            }
            else {        
                return PartialView(result);
              }
        }

        public JsonResult getsearch(string term)
        {
            List<string> result;
             result = db.Products.Where(a => a.title.StartsWith(term)).Select(c =>c.title).ToList();
                //|| a.MyProperty.StartsWith(term)
                //|| a.Category.CategoryName.StartsWith(term))
                


            return Json(result,JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteComment(int postId)
        {
            bool status = false;
            var userId = User.Identity.GetUserId();

            using (ApplicationDbContext dc = new ApplicationDbContext())
            {
                var v = dc.Likes.Where(a => a.postId == postId&& a.userId== userId).FirstOrDefault();
                if (v != null)
                {
                    dc.Likes.Remove(v);
                    dc.SaveChanges();
                    status = true;
                }
            }
            //return new JsonResult ({ Data = new { status = status }},JsonRequestBehavior.AllowGet);
            return Json(new { Data = new { status = status } }, JsonRequestBehavior.AllowGet);

        }


    }
}