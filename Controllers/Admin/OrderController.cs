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
        public ActionResult More(int id)
        {
            Session["Details"] = db.DONHANGs.Where(d => d.ID_DONHANG == id).FirstOrDefault();
            return RedirectToRoute(new { controller = "Order", action = "DanhSachDonHang" });
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult SelectOrderState()
        {
            TRANGTHAIDONHANG ttnew = new TRANGTHAIDONHANG();
            ttnew.lsTTDH = db.TRANGTHAIDONHANGs.ToList();
            return PartialView(ttnew);
        }
        [HttpGet]
        public PartialViewResult UpdateState()
        {
            return PartialView();
        }
        [HttpPost]
        public RedirectToRouteResult UpdateState(DONHANG newStateDH)
        {
            try
            {
                var check = db.DONHANGs.Where(dh => dh.ID_DONHANG == newStateDH.ID_DONHANG).FirstOrDefault();
                check.ID_TRANGTHAI = newStateDH.ID_TRANGTHAI;
                // Check if id == hoàn thành thì chuyển thành hóa đơn
                db.SaveChanges();
                return RedirectToAction("DanhSachDonHang");
            }
            catch (Exception)
            {
                return RedirectToAction("DanhSachDonHang");
            }
        }
    }
}