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
        public ActionResult DanhSachDonHang(int id)
        {
            List<DONHANG> lsDH = new List<DONHANG>();
            if(id == 0)
            {
                lsDH = db.DONHANGs.ToList();
            }
            else
            {
                lsDH = db.DONHANGs.Where(d => d.ID_DONHANG == id).ToList();
            }
            return View(SortByTime(lsDH));
            
        }
        public List<DONHANG> SortByTime(List<DONHANG> ls)
        {
            // 2 0 0 8 - 0 3 - 0 9  1 6 : 0 5 : 0 7
            // 0 1 2 3 4 5 6 7 8 9101112131415
            List<DONHANG> result = new List<DONHANG>();
            List<DONHANG> temp = new List<DONHANG>();
            int idComplete = db.TRANGTHAIDONHANGs.Where(tt => tt.TEN_TRANGTHAI == "Hoàn thành").FirstOrDefault().ID_TRANGTHAI;
            while (ls!= null && ls.Count != 0)
            {
                DONHANG min = ls[0];
                foreach(var item in ls)
                {
                    if (TimeMoreThan(min, item))
                    {
                        min = item;
                    }
                }
                ls.Remove(min);
                temp.Add(min);
            }
            foreach(var item in temp)
            {
                if(item.ID_TRANGTHAI != idComplete)
                {
                    result.Add(item);
                }
            }
            foreach (var item in temp)
            {
                if (item.ID_TRANGTHAI == idComplete)
                {
                    result.Add(item);
                }
            }
            return result;
        }
        public bool TimeMoreThan (DONHANG donXet, DONHANG donThamChieu)
        {
            // 2008-03-09 16:05:07Z
            // 012345678910
            // Năm
            int checkX = int.Parse(donXet.THOIGIAN_DAT.Substring(0, 4));
            int checkTC = int.Parse(donThamChieu.THOIGIAN_DAT.Substring(0, 4));
            if (checkX > checkTC)
            {
                return true;
            }
            if(checkX < checkTC)
            {
                return false;
            }
            // Tháng
            checkX = int.Parse(donXet.THOIGIAN_DAT.Substring(5, 2));
            checkTC = int.Parse(donThamChieu.THOIGIAN_DAT.Substring(5, 2));
            if (checkX > checkTC)
            {
                return true;
            }
            if (checkX < checkTC)
            {
                return false;
            }
            // Ngày
            checkX = int.Parse(donXet.THOIGIAN_DAT.Substring(8, 2));
            checkTC = int.Parse(donThamChieu.THOIGIAN_DAT.Substring(8, 2));
            if (checkX > checkTC)
            {
                return true;
            }
            if (checkX < checkTC)
            {
                return false;
            }
            // Giờ
            checkX = int.Parse(donXet.THOIGIAN_DAT.Substring(11, 2));
            checkTC = int.Parse(donThamChieu.THOIGIAN_DAT.Substring(11, 2));
            if (checkX > checkTC)
            {
                return true;
            }
            if (checkX < checkTC)
            {
                return false;
            }
            // Phút
            checkX = int.Parse(donXet.THOIGIAN_DAT.Substring(14, 2));
            checkTC = int.Parse(donThamChieu.THOIGIAN_DAT.Substring(14, 2));
            if (checkX > checkTC)
            {
                return true;
            }
            if (checkX < checkTC)
            {
                return false;
            }
            
            // Giây
            checkX = int.Parse(donXet.THOIGIAN_DAT.Substring(17, 2));
            checkTC = int.Parse(donThamChieu.THOIGIAN_DAT.Substring(17, 2));
            if (checkX > checkTC)
            {
                return true;
            }
            if (checkX < checkTC)
            {
                return false;
            }
            return false;
        }
        public ActionResult More(int id)
        {
            Session["Details"] = db.DONHANGs.Where(d => d.ID_DONHANG == id).FirstOrDefault();
            return RedirectToRoute(new { controller = "Order", action = "DanhSachDonHang" ,id=0});
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
            newDH.THOIGIAN_DAT = String.Format("{0:u}", DateTime.Now);
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
                    db.SaveChanges();
                    AutoConvertBill(newStateDH.ID_DONHANG);
                    
                }
                // Check if id == hoàn thành thì chuyển thành hóa đơn

                return RedirectToRoute(new { controller = "Order", action = "DanhSachDonHang", id = 0 });
            }
            catch (Exception)
            {
                return RedirectToRoute(new { controller = "Order", action = "DanhSachDonHang", id = 0 });
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
            var checkOrder = db.DONHANGs.Where(d => d.ID_DONHANG == newCT.ID_DONHANG).FirstOrDefault();
            checkOrder.TONGTIEN += newCT.TONGTIEN;
            checkOrder.THOIGIAN_DAT = String.Format("{0:u}", DateTime.Now);
            db.SaveChanges();
            return RedirectToRoute(new { controller = "Order", action = "ManageOrderDetail", id = newCT.ID_DONHANG });
        }
        public ActionResult SelectBooks()
        {
            SACH newS = new SACH();
            newS.ListSach = db.SACHes.ToList();
            return PartialView(newS);
        }
        [HttpGet]
        public ActionResult SearchBar()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult SearchBar(DONHANG srch)
        {
            var check = db.DONHANGs.Where(dh => dh.ID_DONHANG == srch.ID_DONHANG).FirstOrDefault();
            if(check != null)
            {
                return RedirectToRoute(new { controller = "Order", action = "DanhSachDonHang", id = srch.ID_DONHANG });
            }
            return RedirectToRoute(new { controller = "Order", action = "DanhSachDonHang", id = 0 });
        }
    }
}