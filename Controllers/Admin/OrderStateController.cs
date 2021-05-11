using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Homepage.Models;

namespace Homepage.Controllers
{
    public class OrderStateController : Controller
    {
        BookshopEntity db = new BookshopEntity();
        // GET: OrderState
        public RedirectToRouteResult Index()
        {
            return RedirectToRoute(new { controller = "OrderState", action = "DanhSachTrangThai" });
        }
        public ActionResult DanhSachTrangThai()
        {
            return View(db.TRANGTHAIDONHANGs.ToList());
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public RedirectToRouteResult Create(TRANGTHAIDONHANG state)
        {
            db.TRANGTHAIDONHANGs.Add(state);
            db.SaveChanges();
            return RedirectToRoute(new { controller = "OrderState", action = "DanhSachTrangThai" });
        }
        public ActionResult Details(int id)
        {
            return View(db.TRANGTHAIDONHANGs.Where(tt => tt.ID_TRANGTHAI == id).FirstOrDefault());
        }
    }
}