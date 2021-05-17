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
        [HttpGet]
        public PartialViewResult UpdateState()
        {
            return PartialView();
        }
        [HttpPost]
        public RedirectToRouteResult UpdateState(HOADON newStateHD)
        {
            try
            {
                var check = db.HOADONs.Where(dh => dh.ID_HOADON == newStateHD.ID_HOADON).FirstOrDefault();
                check.ID_TRANGTHAI = newStateHD.ID_TRANGTHAI;
                
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }
        public ActionResult More(int id)
        {
            Session["Details"] = db.HOADONs.Where(d => d.ID_HOADON == id).FirstOrDefault();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            var checkHD = db.HOADONs.Where(hd => hd.ID_HOADON == id).FirstOrDefault();
            List<CHITIETHOADON> lsDt = db.CHITIETHOADONs.Where(ct => ct.ID_HOADON == id).ToList();
            foreach(var item in lsDt)
            {
                db.CHITIETHOADONs.Remove(item);
            }
            db.HOADONs.Remove(checkHD);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}