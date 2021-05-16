using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Homepage.Models;

namespace Homepage.Controllers.Admin
{
    public class BillController : Controller
    {
        BookshopEntity db = new BookshopEntity();
        // GET: Bill
        public ActionResult Index()
        {
            return View(db.HOADONs.ToList());
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(HOADON hd)
        {
            db.HOADONs.Add(hd);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult SelectOrder()
        {
            DONHANG dh = new DONHANG();
            dh.lsDonHang = db.DONHANGs.ToList<DONHANG>();
            return PartialView(dh);
        }
        public ActionResult SelectState()
        {
            TRANGTHAIDONHANG tt = new TRANGTHAIDONHANG();
            tt.lsTTDH = db.TRANGTHAIDONHANGs.ToList();
            return PartialView(tt);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View(db.HOADONs.Where(hd => hd.ID_HOADON == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Edit(HOADON hd)
        {
            int id = int.Parse(Url.RequestContext.RouteData.Values["id"].ToString());
            var check = db.HOADONs.Where(d => d.ID_HOADON == id).FirstOrDefault();
            check.ID_TTKH = hd.ID_TTKH;
            check.ID_TRANGTHAI = hd.ID_TRANGTHAI;
            check.ID_DONHANG = hd.ID_DONHANG;
            check.TONGTIEN = hd.TONGTIEN;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}