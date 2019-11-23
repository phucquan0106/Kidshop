using KidDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KidDemo.Controllers
{
    public class CartController : Controller
    {
        private MyDBContext db = new MyDBContext();
        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Buy(int id)
        {
            if (Session["cart"] == null)
            {
                List<Item> cart = new List<Item>();
                cart.Add(new Item { Product = db.Products.Find(id), Quantity = 1});
                Session["cart"] = cart;
            }
            else
            {
                List<Item> cart = (List<Item>)Session["cart"];
                int index = isExist(id);
                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    cart.Add(new Item { Product = db.Products.Find(id), Quantity = 1 });
                }
                Session["cart"] = cart;
            }

            return RedirectToAction("Index", "Products");
        }

        public int isExist(int id)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].Product.Id.Equals(id))
                    return i;
            return -1;
        }

        public ActionResult Remove(int id)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            int index = isExist(id);
            cart.RemoveAt(index);
            Session["cart"] = cart;
            return RedirectToAction("Index", "Cart");
        }

        public ActionResult UpdateCart(int productId, int newQuantity)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            foreach (Item item in cart)
            {
                if (item.Product.Id == productId)
                {
                    item.Quantity = newQuantity;
                }
            }
            Session["cart"] = cart;
            return RedirectToAction("Index", "Cart");
        }
    }
}