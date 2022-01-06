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

        public void GetCart()
        {
            var normalStrategy = new NormalStategy();
            Cart cart = Session["Cart"] as Cart;
            if (cart == null || Session["Cart"] == null)
            {
                cart = new Cart(normalStrategy);
                Session["Cart"] = cart;
            }
            //return cart;
        }
        public ActionResult AddToCart(int id, string strURL)
        {
            var normalStrategy = new NormalStategy();
            Cart cart = Session["Cart"] as Cart;
            if (cart == null || Session["Cart"] == null)
            {
                cart = new Cart(normalStrategy);
                Session["Cart"] = cart;
            }
            var _pro = database.SACHes.SingleOrDefault(s => s.ID_SACH == id);
            if (_pro != null)
            {
                cart.Add_Product_Cart(_pro);
            }
            return Redirect(strURL);
        }
        public ActionResult Update_Cart_Quantity(FormCollection form)
        {
            Cart cart = Session["Cart"] as Cart;
            int id_pro = int.Parse(form["idSach"]);
            int _quantity = int.Parse(form["cartQuantity"]);
            //int stock = 0;
            var stock = database.SACHes.Where(id => id.ID_SACH == id_pro).FirstOrDefault();
            if (stock.SOLUONG_TON < _quantity)
            {
                TempData["StatusMessage"] = "Sản phẩm chỉ còn lại " + stock.SOLUONG_TON +" cuốn";
                return RedirectToAction("ShowCart", "ShoppingCart");
            }
            else
            {
                cart.Update_quantity(id_pro, _quantity);
                return RedirectToAction("ShowCart", "ShoppingCart");

            }
            
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
        public ActionResult CheckOut(FormCollection form)
        {
            try
            {
                Cart cart = Session["Cart"] as Cart;
                SACH sach = new SACH();
                DONHANG _order = new DONHANG();
                //THONGTINKHACHHANG tt = (THONGTINKHACHHANG)Session["ID_TTKH"];
                _order.THOIGIAN_DAT = String.Format("{0:u}", DateTime.Now);
                _order.ID_TTKH = int.Parse(form["CodeCustomer"]);
                _order.ID_TRANGTHAI = 1;
                database.DONHANGs.Add(_order);
                foreach (var item in cart.Items)
                {
                    CHITIETDONHANG _order_detail = new CHITIETDONHANG();
                    _order_detail.ID_DONHANG = _order.ID_DONHANG;
                    _order_detail.ID_SACH = item._sach.ID_SACH;
                    _order_detail.GIA_BAN = (double)item._sach.GIA_BAN;
                    _order_detail.SOLUONG = Convert.ToInt16(item._quantity);
                    _order_detail.TONGTIEN = item._quantity * item._sach.GIA_BAN;
                    _order.TONGTIEN =+ _order_detail.TONGTIEN;
                    database.CHITIETDONHANGs.Add(_order_detail);
                    foreach (var p in database.SACHes.Where(s => s.ID_SACH == _order_detail.ID_SACH))
                    {
                        var update_quan_pro = p.SOLUONG_TON - item._quantity;
                        p.SOLUONG_TON = Convert.ToInt16(update_quan_pro);
                    }
                }
                
                database.SaveChanges();
                cart.ClearCart();
                return RedirectToAction("CheckOut_Success", "ShoppingCart");
            }
            catch
            {
                return Content("Error checkout. Please check information of Customer... Thanks");
            }
        }
        public ActionResult CheckOut_Success()
        {
            return View();
        }
        public ActionResult CreateCustomer()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateCustomer(THONGTINKHACHHANG tt)
        {
            try
            {
                tt.TONG_TIEUDUNG = 0;
                database.THONGTINKHACHHANGs.Add(tt);
                database.SaveChanges();
                Session["TEN_KHACHHANG"] = tt.TEN_KHACHHANG;
                Session["ID_TTKH"] = tt.ID_TTKH;
                Session["EMAIL_KHACHHANG"] = tt.EMAIL_KHACHHANG;
                Session["DIACHI"] = tt.DIACHI;
                Session["SO_DIENTHOAI"] = tt.SO_DIENTHOAI;
                return RedirectToAction("ShowCart", "ShoppingCart");
            }
            catch
            {
                return View();
            }
        }
    }
}