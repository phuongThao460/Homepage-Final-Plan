using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Homepage.Models;

namespace Homepage.Controllers
{
    public class uHomeController : Controller
    {
        BookshopEntity db = new BookshopEntity();
        public ActionResult Index()
        {
            return View(db.SACHes.ToList());
        }
        public ActionResult Search(string searchString, int theloaiID = 0)
        {
            var theloai = from c in db.THELOAIs select c;
            ViewBag.theloaiID = new SelectList(theloai, "ID_THELOAI", "TEN_THELOAI"); // danh sách Category 

            var sach = from l in db.SACHes
                       join c in db.THELOAIs on l.ID_THELOAI equals c.ID_THELOAI
                       select new { l.ID_SACH, l.TEN_SACH, l.LOAI_BIA, l.GIA_BAN, l.ID_THELOAI, l.ID_TACGIA, c.TEN_THELOAI };

            if (!String.IsNullOrEmpty(searchString))
            {
                sach = sach.Where(s => s.TEN_SACH.Contains(searchString));
            }

            if (theloaiID != 0)
            {
                sach = sach.Where(x => x.ID_THELOAI == theloaiID);
            }

            List<SACH> listSach = new List<SACH>();

            foreach (var item in sach)
            {
                SACH temp = new SACH();
                temp.ID_THELOAI = item.ID_THELOAI;
                temp.ID_SACH = item.ID_SACH;
                temp.TEN_SACH = item.TEN_SACH;
                temp.GIA_BAN = item.GIA_BAN;
                temp.ID_TACGIA = item.ID_TACGIA;
                listSach.Add(temp);
            }
            return View(listSach);
        }
        public ActionResult GetNameTG()
        {
            int id = int.Parse(Url.RequestContext.RouteData.Values["id"].ToString());
            var tacgia = db.TACGIAs.Where(tg => tg.ID_TACGIA == id).FirstOrDefault();
            return PartialView(tacgia);
        }
        public ActionResult GetNameTL()
        {
            int id = int.Parse(Url.RequestContext.RouteData.Values["id"].ToString());
            var theloai = db.THELOAIs.Where(tl => tl.ID_THELOAI == id).FirstOrDefault();
            return PartialView(theloai);
        }
        [HttpPost]
        public ActionResult Details(FEEDBACK fb)
        {
            int id = int.Parse(Url.RequestContext.RouteData.Values["id"].ToString());
            fb.ID_SACH = id;
            db.FEEDBACKs.Add(fb);
            db.SaveChanges();
            return View(db.SACHes.Where(s => s.ID_SACH == id).FirstOrDefault());
        }
        public ActionResult Details(int id)
        {
            return View(db.SACHes.Where(s => s.ID_SACH == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Create(FEEDBACK fb)
        {
            
            return PartialView("Create");
        }
    }
}