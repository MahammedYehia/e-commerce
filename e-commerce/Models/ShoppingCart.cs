using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

using System.Data.Entity;

namespace e_commerce.Models
{
    public class ShoppingCart
    {
        ApplicationDbContext storeDB = new ApplicationDbContext();
        string ShoppingCartId { get; set; }
        string ShoppingCartId2 { get; set; }

        public const string CartSessionKey = "CartId";
        public const string userSessionKey = "UserId";

        public static ShoppingCart GetCart(HttpContextBase context)
        {
            var cart = new ShoppingCart();
            
            cart.ShoppingCartId = cart.GetCartId(context);
            return cart;
        }


        /////////////////////////test///////////
        //public static ShoppingCart GetCart2(HttpContextBase context)
        //{
        //    var cart = new ShoppingCart();

        //    cart.ShoppingCartId2 = cart.GetCartId2(context);
        //    return cart;

        //}


        //public static ShoppingCart GetCart2(Controller controller)
        //{
        //    return GetCart2(controller.HttpContext);
        //}
        ////////////////////////////////////



        // Helper method to simplify shopping cart calls
        public static ShoppingCart GetCart(Controller controller)
        {
            return GetCart(controller.HttpContext);
        }

        public void AddToCart(Product item)
        {
            //var usr = context.User.Identity.GetUserId();

            var cartItem = storeDB.Carts.SingleOrDefault(c => c.CartId == ShoppingCartId
                && c.ItemId == item.id);

            var p = storeDB.Users.SingleOrDefault(o=>o.Id==item.userId);


            if (cartItem == null)
            { 

                cartItem = new Cart
                {

                    userId = ShoppingCartId,
                    ItemId = item.id,
                    CartId = ShoppingCartId,
                    Count = 1,
                    DateCreated = DateTime.Now
                    
                };


                storeDB.Carts.Add(cartItem);
            }
            else
            {

                cartItem.Count++;
            }
            //var product = storeDB.Products.SingleOrDefault(q => q.id == cartItem.ItemId);
            //product.Quentity -= cartItem.Count;
            storeDB.SaveChanges();
        }
        public int RemoveFromCart(int id)
        {

            var cartItem = storeDB.Carts.Single(
                cart => cart.CartId == ShoppingCartId
                && cart.RecordId == id);

            int itemCount = 0;

            if (cartItem != null)
            {
                if (cartItem.Count > 1)
                {
                    cartItem.Count--;
                    itemCount = cartItem.Count;
                }
                else
                {
                    storeDB.Carts.Remove(cartItem);

                }
                //var product = storeDB.Products.SingleOrDefault(q => q.id ==cartItem.ItemId);
                //product.Quentity += cartItem.Count;
                storeDB.SaveChanges();
            }
            return itemCount;
        }

        public void EmptyCart()
        {
            var cartItems = storeDB.Carts.Where(
                cart => cart.CartId == ShoppingCartId);

            foreach (var cartItem in cartItems)
            {
                storeDB.Carts.Remove(cartItem);
            }

            storeDB.SaveChanges();
        }
        public List<Cart> GetCartItems()
        {
            return storeDB.Carts.Where(cart => cart.CartId == ShoppingCartId).ToList();
        }
        //public List<Cart> GetCartItems2()
        //{
        //    return storeDB.Carts.Where(cart => cart.Item.id ==cart.ItemId ).ToList();
        //}
        public int GetCount()
        {

            int? count = (from cartItems in storeDB.Carts
                          where cartItems.CartId == ShoppingCartId
                          select (int?)cartItems.Count).Sum();

            return count ?? 0;
        }

        public decimal GetTotal()
        {

            decimal? total = (from cartItems in storeDB.Carts
                              where cartItems.CartId == ShoppingCartId
                              select (int?)cartItems.Count *
                              cartItems.Item.price).Sum();

            return total ?? decimal.Zero;
        }

        public int CreateOrder(Order order)
        {
            decimal orderTotal = 0;
            var cartItems = GetCartItems();
            foreach (var item in cartItems)
            {
                var orderDetail = new OrderDetail
                {
                    userId = ShoppingCartId,
                    ItemId = item.ItemId,
                    OrderId = order.OrderId,
                    UnitPrice = item.Item.price,
                    Quantity = item.Count,
                
                };

              
                orderTotal += (item.Count * item.Item.price);

                storeDB.OrderDetails.Add(orderDetail);

                var product = storeDB.Products.SingleOrDefault(q => q.id == item.ItemId);
                product.Quentity -= item.Count;

                storeDB.SaveChanges();

            }

            order.Total = orderTotal;


            storeDB.SaveChanges();

            EmptyCart();

            return order.OrderId;
        }
        //public int CreateOrder(Product product)
        //{
            
        //    var cartItems = GetCartItems2();

        //    foreach (var item in cartItems)
        //    {
        //        var orderDetail = new Product
        //        {

        //            price = product.price--,
                    
        //        };



        //        storeDB.Products.Add(orderDetail);

        //    }

        //    storeDB.SaveChanges();


        //    return product.price;
        //}

        public string GetCartId(HttpContextBase context)
        {
            if (context.Session[CartSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.GetUserId()))
                {
                    context.Session[CartSessionKey] =
                        context.User.Identity.GetUserId();
                }
                else
                {

                    Guid tempCartId = Guid.NewGuid();

                    context.Session[CartSessionKey] = tempCartId.ToString();
                }
            }
            return context.Session[CartSessionKey].ToString();
        }



       
        
        public void MigrateCart(string Email)
        {
            var shoppingCart = storeDB.Carts.Where(
                c => c.CartId == ShoppingCartId);

            foreach (Cart item in shoppingCart)
            {
                item.CartId = Email;
            }
            storeDB.SaveChanges();
        }
    }
}