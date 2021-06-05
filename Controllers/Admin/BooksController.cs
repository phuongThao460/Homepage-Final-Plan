using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Homepage.Models;

namespace Homepage.Controllers
{
    public class BooksController : Controller
    {
        BookshopEntity db = new BookshopEntity();
        public ActionResult Index()
        {
            List<SACH> ls = db.SACHes.ToList(); 
            foreach(var item in ls)
            {
                item.fomatGiaBia = StaticClass.ConvertNumberToPrice(item.GIA_BIA.ToString());
                item.fomatGiaBan = StaticClass.ConvertNumberToPrice(item.GIA_BAN.ToString());
            }
            return View(db.SACHes.ToList());
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(SACH newS)
        {
            if(newS.TEN_SACH==null||
                newS.LOAI_BIA==null||
                newS.NGAY_XUATBAN == null ||
                newS.SOLUONG_TON == null ||
                newS.SO_TRANG == null ||
                newS.KICH_THUOC == null ||
                newS.ID_NCC == null ||
                newS.ID_NXB == null ||
                newS.ID_TACGIA == null ||
                newS.ID_THELOAI == null ||
                newS.GIA_BIA == null ||
                newS.GIA_BAN == null ||
                newS.KHOILUONG == null)
            {
                ViewBag.ErrorContent = "Vui lòng nhập đủ thông tin";
                return View(newS);
            }
            db.SACHes.Add(newS);
            db.SaveChanges();
            var list = db.SACHes.ToList();
            int id = list.Last().ID_SACH;
            return RedirectToRoute(new { controller = "BookImage", action = "Index", id = id });
        }
        public ActionResult UploadImage()
        {
            return View();
        }
        public ActionResult Details()
        {
            int id = int.Parse(Url.RequestContext.RouteData.Values["id"].ToString());
            var select = db.SACHes.Where(s => s.ID_SACH == id).FirstOrDefault();
            return View(select);
        }

        [HttpGet]
        public ActionResult Edit()
        {
            int id = int.Parse(Url.RequestContext.RouteData.Values["id"].ToString());
            var select = db.SACHes.Where(s => s.ID_SACH == id).FirstOrDefault();
            return View(select);
        }

        [HttpPost]
        public ActionResult Edit(SACH sach)
        {
            if (sach.TEN_SACH == null ||
                sach.LOAI_BIA == null ||
                sach.NGAY_XUATBAN == null ||
                sach.SOLUONG_TON == null ||
                sach.SO_TRANG == null ||
                sach.KICH_THUOC == null ||
                sach.ID_NCC == null ||
                sach.ID_NXB == null ||
                sach.ID_TACGIA == null ||
                sach.ID_THELOAI == null ||
                sach.GIA_BIA == null ||
                sach.GIA_BAN == null ||
                sach.KHOILUONG == null)
            {
                ViewBag.ErrorContent = "Vui lòng nhập đủ thông tin";
                return View(sach);
            }
            int id = int.Parse(Url.RequestContext.RouteData.Values["id"].ToString());
            var select = db.SACHes.Where(s => s.ID_SACH == id).FirstOrDefault();
            select.TEN_SACH = sach.TEN_SACH;
            select.LOAI_BIA = sach.LOAI_BIA;
            select.NGAY_XUATBAN = sach.NGAY_XUATBAN;
            select.SOLUONG_TON = sach.SOLUONG_TON;
            select.SO_TRANG = sach.SO_TRANG;
            select.KICH_THUOC = sach.KICH_THUOC;
            select.ID_NCC = sach.ID_NCC;
            select.ID_NXB = sach.ID_NXB;
            select.ID_TACGIA = sach.ID_TACGIA;
            select.ID_THELOAI = sach.ID_THELOAI;
            select.GIA_BIA = sach.GIA_BIA;
            select.GIA_BAN = sach.GIA_BAN;
            select.KHOILUONG = sach.KHOILUONG;
            db.SaveChanges();
            return RedirectToRoute(new { controller = "Books", action = "Details", id = id });
        }

        #region 'DROP DOWN' AREA
        public ActionResult SelectNXB()
        {
            NHAXUATBAN nxb = new NHAXUATBAN();
            nxb.ListNXB = db.NHAXUATBANs.ToList<NHAXUATBAN>();
            return PartialView(nxb);
        }
        public ActionResult SelectTG()
        {
            TACGIA tg = new TACGIA();
            tg.ListTG = db.TACGIAs.ToList<TACGIA>();
            return PartialView(tg);
        }
        public ActionResult SelectNCC()
        {
            NHACUNGCAP ncc = new NHACUNGCAP();
            ncc.ListNCC = db.NHACUNGCAPs.ToList<NHACUNGCAP>();
            return PartialView(ncc);
        }
        public ActionResult SelectTL()
        {
            THELOAI ncc = new THELOAI();
            ncc.ListTL = db.THELOAIs.ToList<THELOAI>();
            return PartialView(ncc);
        }
        #endregion
    }
}