using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Homepage.Models;

namespace Homepage.Controllers.Admin
{
    public class AutoPriceController : Controller
    {
        BookshopEntity db = new BookshopEntity();
        public ActionResult Index()
        {
            return View(db.BANGGIAs.ToList());
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(BANGGIA bg)
        {
            if (bg.GIATRI >= 0)
            {
                bg.TANG_GIAM = true;
            }
            else
            {
                var check = db.SACHes.Where(s => s.ID_SACH == bg.ID_SACH).FirstOrDefault();
                if ((check.GIA_BAN + bg.GIATRI) < 0)
                {
                    return RedirectToAction("Create");
                }
                bg.TANG_GIAM = false;
            }
            if (bg.TANG_GIAM == false)
            {
                bg.GIATRI = bg.GIATRI * (-1);
            }
            db.BANGGIAs.Add(bg);
            db.SaveChanges();
            return RedirectToAction("Index", "");
        }
        public ActionResult SelectSach()
        {
            SACH ls = new SACH();
            ls.ListSach = db.SACHes.ToList<SACH>();
            return PartialView(ls);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View(db.BANGGIAs.Where(bg => bg.ID_BANGGIA == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Edit(BANGGIA bg)
        {
            int id = int.Parse(Url.RequestContext.RouteData.Values["id"].ToString());
            var check = db.BANGGIAs.Where(g => g.ID_BANGGIA == id).FirstOrDefault();
            check.ID_SACH = bg.ID_SACH;
            check.NGAY_APDUNG = bg.NGAY_APDUNG;
            if (bg.GIATRI >= 0)
            {
                check.TANG_GIAM = true;
            }
            else
            {
                var check2 = db.SACHes.Where(s => s.ID_SACH == bg.ID_SACH).FirstOrDefault();
                if ((check2.GIA_BAN + bg.GIATRI) < 0)
                {
                    return RedirectToAction("Index");
                }
                check.TANG_GIAM = false;
            }
            if (check.TANG_GIAM == false)
            {
                check.GIATRI = bg.GIATRI * (-1);
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            db.BANGGIAs.Remove(db.BANGGIAs.Where(bg => bg.ID_BANGGIA == id).FirstOrDefault());
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult AutoUpdate()
        {
            foreach (var item in db.BANGGIAs.ToList())
            {
                if (HetHan(item))
                {
                    var check = db.SACHes.Where(s => s.ID_SACH == item.ID_SACH).FirstOrDefault();
                    if (item.TANG_GIAM)
                    {
                        check.GIA_BAN += item.GIATRI;
                    }
                    else
                    {
                        check.GIA_BAN -= item.GIATRI;
                    }
                    db.BANGGIAs.Remove(item);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }
        public bool HetHan(BANGGIA bg)
        {
            int ngay = int.Parse(bg.NGAY_APDUNG.Substring(0, 2));
            // 20/12/2021
            int thang = int.Parse(bg.NGAY_APDUNG.Substring(3, 2));
            int nam = int.Parse(bg.NGAY_APDUNG.Substring(6, 4));
            DateTime current = DateTime.Now;
            if (current.Year > nam)
            {
                return true;
            }
            else if (current.Year < nam)
            {
                return false;
            }
            if (current.Month > thang)
            {
                return true;
            }
            else if (current.Month < thang)
            {
                return false;
            }
            if (current.Day > ngay)
            {
                return true;
            }
            else if (current.Day < ngay)
            {
                return false;
            }
            return false;
        }
    }
}