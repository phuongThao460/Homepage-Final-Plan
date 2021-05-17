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
        [HttpPost]
        public ActionResult Create(DONHANG newDH)
        {
            newDH.TONGTIEN = 0;
            db.DONHANGs.Add(newDH);
            db.SaveChanges();
            int id = db.DONHANGs.ToList().Last().ID_DONHANG;
            return RedirectToRoute(new { controller = "Order", action = "ManageOrderDetail", id = id });
        }
        public ActionResult Edit(int id)
        {
            return RedirectToRoute(new { controller = "Order", action = "ManageOrderDetail", id = id });
        }
        public ActionResult ManageOrderDetail(int id)
        {
            List<CHITIETDONHANG> lsDt = db.CHITIETDONHANGs.Where(ct => ct.ID_DONHANG == id).ToList();
            return View(lsDt);
        }
        public ActionResult DeleteOrderDetail(int id)
        {
            var check = db.CHITIETDONHANGs.Where(ct => ct.ID_CTDH == id).FirstOrDefault();
            db.CHITIETDONHANGs.Remove(check);
            db.SaveChanges();
            return RedirectToRoute(new { controller = "Order", action = "ManageOrderDetail", id = id });
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
                db.SaveChanges();
                int idComplete = db.TRANGTHAIDONHANGs.Where(tt => tt.TEN_TRANGTHAI == "Hoàn thành").FirstOrDefault().ID_TRANGTHAI;
                if (newStateDH.ID_TRANGTHAI == idComplete)
                {
                    HOADON newHD = new HOADON();
                    newHD.ID_TTKH = check.ID_TTKH;
                    newHD.ID_TRANGTHAI = newStateDH.ID_TRANGTHAI;
                    newHD.TONGTIEN = check.TONGTIEN;
                    newHD.ID_DONHANG = check.ID_DONHANG;
                    db.HOADONs.Add(newHD);
                    AutoConvertBill(newStateDH.ID_DONHANG);
                    
                }
                // Check if id == hoàn thành thì chuyển thành hóa đơn
                
                return RedirectToAction("DanhSachDonHang");
            }
            catch (Exception)
            {
                return RedirectToAction("DanhSachDonHang");
            }
        }
        public void AutoConvertBill(int idOrder)
        {
            HOADON checkHD = db.HOADONs.ToList().Last();
            var listCTHD = db.CHITIETDONHANGs.Where(ct => ct.ID_DONHANG == idOrder).ToList();
            foreach (var item in listCTHD)
            {
                CHITIETHOADON newCTHD = new CHITIETHOADON();
                newCTHD.ID_HOADON = checkHD.ID_HOADON;
                newCTHD.ID_SACH = item.ID_SACH;
                newCTHD.GIA_BAN = item.GIA_BAN;
                newCTHD.SOLUONG = item.SOLUONG;
                newCTHD.TONGTIEN = item.TONGTIEN;
                db.CHITIETHOADONs.Add(newCTHD);
                checkHD.TONGTIEN += item.TONGTIEN;
                
            }
            db.SaveChanges();
        }
        [HttpGet]
        public ActionResult CreateOrderDetail()
        {
            int id = int.Parse(Url.RequestContext.RouteData.Values["id"].ToString());
            CHITIETDONHANG newCT = new CHITIETDONHANG();
            newCT.ID_DONHANG = id;
            return PartialView(newCT);
        }
        [HttpPost]
        public ActionResult CreateOrderDetail(CHITIETDONHANG newCT)
        {
            var checkBook = db.SACHes.Where(s => s.ID_SACH == newCT.ID_SACH).FirstOrDefault();
            newCT.GIA_BAN = checkBook.GIA_BAN;
            newCT.TONGTIEN = checkBook.GIA_BAN * newCT.SOLUONG;
            db.CHITIETDONHANGs.Add(newCT);
            db.SaveChanges();
            return RedirectToRoute(new { controller = "Order", action = "ManageOrderDetail", id = newCT.ID_DONHANG });
        }
        public ActionResult SelectBooks()
        {
            SACH newS = new SACH();
            newS.ListSach = db.SACHes.ToList();
            return PartialView(newS);
        }
    }
}