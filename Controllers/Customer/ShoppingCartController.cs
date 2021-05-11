using Homepage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Homepage.Controllers
{
    public class ShoppingCartController : Controller
    {
        BookshopEntity database = new BookshopEntity();
        // GET: ShoppingCart
        public ActionResult ShowCart()
        {
            if (Session["Cart"] == null)
                return View("EmptyCart");
            Cart _cart = Session["Cart"] as Cart;
            return View(_cart);
        }

        public Cart GetCart()
        {
            Cart cart = Session["Cart"] as Cart;
            if (cart == null || Session["Cart"] == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }
        public ActionResult AddToCart(int id)
        {
            var _pro = database.SACHes.SingleOrDefault(s => s.ID_SACH == id);
            if (_pro != null)
            {
                GetCart().Add_Product_Cart(_pro);
            }
            return RedirectToAction("ShowCart", "ShoppingCart");
        }
        public ActionResult Update_Cart_Quantity(FormCollection form)
        {
            Cart cart = Session["Cart"] as Cart;
            int id_pro = int.Parse(form["idSach"]);
            int _quantity = int.Parse(form["cartQuantity"]);
            cart.Update_quantity(id_pro, _quantity);
            return RedirectToAction("ShowCart", "ShoppingCart");
        }
        public ActionResult RemoveCart(int id)
        {
            Cart cart = Session["Cart"] as Cart;
            cart.Remove_CartItem(id);
            return RedirectToAction("ShowCart", "ShoppingCart");
        }

        public PartialViewResult BagCart()
        {
            int toltal_quantity_item = 0;
            Cart cart = Session["Cart"] as Cart;
            if (cart != null)
            {
                toltal_quantity_item = cart.Total_quantity();
            }
            ViewBag.QuantityCart = toltal_quantity_item;
            return PartialView("BagCart");
        }
    }
}