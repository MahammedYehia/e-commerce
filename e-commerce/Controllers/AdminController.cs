using e_commerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using e_commerce;
using Twilio;
using Twilio.Types;
using Twilio.Rest.Api.V2010.Account;
using System.Web.Helpers;
using System.Collections;

namespace e_commerce.Controllers
{
    [Authorize(Roles="admin")]
    public class AdminController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

       
        public ActionResult Index()
        {
            return View();
        }

        
     ///////////////////////////charts functions//////////////////////////////////////
        public ActionResult charterColom() 
        {
            ArrayList xValue = new ArrayList();
            ArrayList yValue = new ArrayList();
            var result = (from n in db.OrderDetails select n);
            result.ToList().ForEach(rs => xValue.Add(rs.Item.Category.CategoryName));
            result.ToList().ForEach(rs => yValue.Add(rs.Quantity));
            new Chart(width: 500, height: 400, theme: ChartTheme.Green).AddTitle("Chart for show high product sell")
                .AddSeries("Default", chartType: "Column", xValue: xValue, yValues: yValue).Write("bmp");

            return null;
        }
        public ActionResult Pie()
        {
            ArrayList xValue = new ArrayList();
            ArrayList yValue = new ArrayList();
            var result = (from n in db.OrderDetails select n);
            result.ToList().ForEach(rs => xValue.Add(rs.Item.Category.CategoryName));
            result.ToList().ForEach(rs => yValue.Add(rs.Quantity));
            new Chart(width: 500, height: 400, theme: ChartTheme.Green).AddTitle("Chart for show high product sell")
                .AddSeries("Default", chartType: "Pie", xValue: xValue, yValues: yValue).Write("bmp");

            return null;
        }
        public ActionResult Bubble()
        {
            ArrayList xValue = new ArrayList();
            ArrayList yValue = new ArrayList();
            var result = (from n in db.OrderDetails select n);
            result.ToList().ForEach(rs => xValue.Add(rs.Item.Category.CategoryName));
            result.ToList().ForEach(rs => yValue.Add(rs.Quantity));
            new Chart(width: 500, height: 400, theme: ChartTheme.Green).AddTitle("Chart for show high product sell")
                .AddSeries("Default", chartType: "Bubble", xValue: xValue, yValues: yValue).Write("bmp");

            return null;
        }
        public ActionResult Funnel()
        {
            ArrayList xValue = new ArrayList();
            ArrayList yValue = new ArrayList();
            var result = (from n in db.OrderDetails select n);
            result.ToList().ForEach(rs => xValue.Add(rs.Item.Category.CategoryName));
            result.ToList().ForEach(rs => yValue.Add(rs.Quantity));
            new Chart(width: 500, height: 400, theme: ChartTheme.Green).AddTitle("Chart for show high product sell")
                .AddSeries("Default", chartType: "Funnel", xValue: xValue, yValues: yValue).Write("bmp");

            return null;
        }
     ////////////////////////////////////////////////////////////////


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
                const string accountSid = "AC746940f2650943c3bca4400a80c4fbd3";
                const string authToken = "55e9c74e7aadc1d782c8eb93e2423dc3";

                TwilioClient.Init(accountSid, authToken);

                var to = new PhoneNumber("+91" + sms.Number);

                var message = MessageResource.Create(
                    to,
                    from: new PhoneNumber("+16XXXXXXXXX"), 
                    //  From number, must be an SMS-enabled Twilio number ( This will send sms from ur "To" numbers ). 
                    body: "Hello " + sms.Name + " !! Welcome to Twilio SMS API example in ASP.NET MVC!!");


                ViewBag.Message = "Message Sent";
                return RedirectToAction("SendSMS");
            }
            else
            {
                return View();
            }
        }
        //public JsonResult GetNotificationContacts()
        //{
        //    var notificationRegisterTime = Session["LastUpdated"] != null ? Convert.ToDateTime(Session["LastUpdated"]) : DateTime.Now;
        //    NotificationComponent NC = new NotificationComponent();
        //    var list = NC.GetContacts(notificationRegisterTime);
        //    //update session here for get only new added contacts (notification)
        //    Session["LastUpdate"] = DateTime.Now;
        //    return new JsonResult { Data = list, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        //}

        public ActionResult chat(string msg)
        {


            var product = new Comment();
            product.CommentName = msg;

            product.date = DateTime.Now;

            db.Comments.Add(product);
             db.SaveChanges();

            return PartialView("chat",product);
        }
        public ActionResult returnchat(string msg)
        {
            var x = db.Comments.ToList();
            return PartialView("returnchat", x);
        }
        [Authorize]
        public ActionResult getallfav()
        {
            return View(db.favourites.OrderBy(x => x.favId).ToList());
        }
    
        public ActionResult getproductbyuser()
        {
            return View(db.OrderDetails.OrderBy(x => x.OrderDetailId).ToList());

        }
        public ActionResult Best_selling()
        {
            var products = db.OrderDetails.ToList();
            var product = db.Products.ToList();

            var categories =
                from p in products
                group p by p.ItemId into g
                select new bestsell
                  {
                      ItemId = g.Key, TotalUnitsInStock = g.Sum(p => p.Quantity),Items=product,
                };
                return PartialView("Best_selling", categories.OrderByDescending(c=>c.TotalUnitsInStock).Where(v=>v.TotalUnitsInStock >2));
          }

        //public ActionResult Best_selling2()
        //{
        //    var namesAndTotals =
        //        db.OrderDetails.Select(c => new {
        //            c.Item.title,
        //           c.Quantity.Sum()asm
        //        });

        //    return View();
        //}


        public ActionResult allfeedback()
        {
            var userId = User.Identity.GetUserId();
            var prod = from app in db.Likes
                       join Produc in db.Products
                       on app.productId equals Produc.id
                       where Produc.User.Id == userId
                       select app;

            var grouped = from j in prod
                          group j by j.Product.title
                              into gr
                              select new FeedBackModel
                              {
                                  title = gr.Key,
                                  Items = gr
                              };

            return View(grouped.ToList());
        }
        public ActionResult allSummary()
        {
            ViewData["Products"] = db.Products.ToList().Count();

            ViewData["Categories"] = db.Categories.ToList().Count();

            ViewData["Users"] = db.Users.ToList().Count();
            ViewData["Orders"] = db.Orders.ToList().Count();
            ViewData["OrderDetails"] = db.OrderDetails.ToList().Count();
            ViewData["carts"] = db.Carts.ToList().Count();
            ViewData["like"] = db.favourites.ToList().Count();
            ViewData["Comments"] = db.Likes.ToList().Count();

            return PartialView("allSummary");
        }




        [Authorize]
        public ActionResult ProBuyedbyadmin()
        {

            var userId = User.Identity.GetUserId();
            var prod = from app in db.OrderDetails
                          join Produc in db.Products
                          on app.ItemId equals Produc.id
                          where Produc.User.Id == userId
                          select app;

            var grouped = from j in prod
                        group j by j.Item.title
                            into gr
                            select new ProductViewModel
                            {
                                title = gr.Key,
                                Items = gr
                            };



            return View(grouped.ToList());
        }

       
	}





     



}