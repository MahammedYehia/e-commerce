using e_commerce.Models;
using e_commerce.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Data.Entity;

namespace e_commerce.Controllers
{
    public class ShoppingCartController : Controller
    {
        ApplicationDbContext storeDB = new ApplicationDbContext();

        public ActionResult Index()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);
            var viewModel = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };

            return View(viewModel);
        }
        [Authorize]
        public ActionResult AddToCart(int id)
        {
            Session["user"]= User.Identity.GetUserId();

            var addedItem = storeDB.Products.Include("User").Single(item => item.id == id);
            var cart = ShoppingCart.GetCart(this.HttpContext);
            //var cart = ShoppingCart.GetCart2(this.HttpContext);

           

            cart.AddToCart(addedItem);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {

            var cart = ShoppingCart.GetCart(this.HttpContext);


            string itemName = storeDB.Carts
                .Single(item => item.RecordId == id).Item.title;


            int itemCount = cart.RemoveFromCart(id);


            var results = new ShoppingCartRemoveViewModel
            {
                Message = Server.HtmlEncode(itemName) +
                    " has been removed from your shopping cart.",
                CartTotal = cart.GetTotal(),
                CartCount = cart.GetCount(),
                ItemCount = itemCount,
                DeleteId = id
            };
            return Json(results);
        }

        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            ViewData["CartCount"] = cart.GetCount();
            return PartialView("CartSummary");
        }
        [ChildActionOnly]
        public ActionResult CartSummary2()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            ViewData["CartCount"] = cart.GetCount();
            return PartialView("CartSummary2");
        }

	}
}