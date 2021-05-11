using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Homepage.Models;

namespace Homepage.Controllers
{
    public class OrderController : Controller
    {
        BookshopEntity db = new BookshopEntity();
        // GET: Order
        public RedirectToRouteResult Index()
        {
            return RedirectToRoute(new { controller = "Order", action = "DanhSachDonHang" });
        }
        public ActionResult DanhSachDonHang()
        {
            return View(db.DONHANGs.ToList());
        }
    }
}