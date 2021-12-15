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
namespace e_commerce.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        ApplicationDbContext storeDB = new ApplicationDbContext();
        const string PromoCode = "50";

        public ActionResult AddressAndPayment()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddressAndPayment(FormCollection values)
        {
            var order = new Order();
            TryUpdateModel(order);
            try
            {
                if (string.Equals(values["PromoCode"], PromoCode,
                    StringComparison.OrdinalIgnoreCase) == false)
                {
                    return View(order);
                }
                else
                {
                    var userid = User.Identity.GetUserId();

                    order.Username = User.Identity.Name;
                   
                    order.OrderDate = DateTime.Now;

                    // var productId = (int)Session["productid"];
                    //Session["email"] = order.Email;
                    //Session["username"] = order.FirstName;
                    //Session["pro"] = order.OrderId;
                    //Session["price"] = order.Total;
                   // storeDB.Products;
                    storeDB.Orders.Add(order);
                    storeDB.SaveChanges();

                    var cart = ShoppingCart.GetCart(this.HttpContext);
                    cart.CreateOrder(order);
                    
                    //var mail = new MailMessage();
                    //var Logininfo = new NetworkCredential("mahamedyehia469@gmail.com", "3031996@fcih");
                    //mail.From = new MailAddress((string)Session["email"]);
                    //mail.To.Add(new MailAddress("mahamedyehia469@gmail.com"));
                    //mail.Subject = order.Address;
                    //mail.IsBodyHtml = true;
                    //string body = "sender name:" + Session["username"] + "<br>" +
                    //    "sender" + Session["email"] + "<br>" +
                    //    "buy product:" + Session["pro"] + "<br>" +
                    //    "total:" + (int)Session["price"] + "<br>";

                    //mail.Body = body;

                    //var smtpClient = new SmtpClient("smtp.gmail.com", 587);
                    //smtpClient.EnableSsl = true;
                    //smtpClient.Credentials = Logininfo;

                    //smtpClient.Send(mail);
                    return RedirectToAction("Complete",new { id = order.OrderId });
                }
            }
            catch
            {

                return View(order);
            }
        }

        public ActionResult Complete(int id)
        {

            bool isValid = storeDB.Orders.Any(o => o.OrderId == id && o.Username == User.Identity.Name);

            if (isValid)
            {
                return View(id);
            }
            else
            {
                return View("Error");
            }
        }
	}
}