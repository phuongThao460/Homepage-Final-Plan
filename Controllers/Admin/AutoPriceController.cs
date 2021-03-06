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
            ListAutoPrice newObj = new ListAutoPrice();
            foreach(var item in db.SACHes.ToList())
            {
                var check = db.BANGGIAs.Where(bg => bg.ID_SACH == item.ID_SACH).FirstOrDefault();
                if(check == null)
                {
                    newObj.lsDepend.Add(item);
                }
            }
            DateTime needDate = DateTime.Now.AddDays(1);
            string date = needDate.Day.ToString();
            if (date.Length == 1)
                date = "0" + date;
            string month = needDate.Month.ToString();
            if (month.Length == 1)
                month = "0" + month;
            newObj.minDate = needDate.Year.ToString() + "-" + month + "-" + date;
            return View(newObj);
        }
        [HttpPost]
        public ActionResult Create(ListAutoPrice obj)
        {
            string ngayApDung = obj.ngayApDung;
            bool tangGiam = obj.tangGiam == "down" ? false : true;
            double giaTri = obj.giaTri;
            bool donVi = obj.donVi == "VND" ? true : false;
            ListAutoPrice newObj = new ListAutoPrice();
            foreach (var item in db.SACHes.ToList())
            {
                var check = db.BANGGIAs.Where(bg => bg.ID_SACH == item.ID_SACH).FirstOrDefault();
                if (check == null)
                {
                    newObj.lsDepend.Add(item);
                }
            }
            // Ko nhận dc ls
            foreach (var item in newObj.lsDepend)
            {
                BANGGIA newBG = new BANGGIA();
                newBG.ID_SACH = item.ID_SACH;
                newBG.NGAY_APDUNG = ngayApDung;
                newBG.TANG_GIAM = tangGiam;
                if (donVi)
                {
                    newBG.GIATRI = giaTri;
                }
                else
                {
                    newBG.GIATRI = (item.GIA_BAN * giaTri) / 100;
                }
                db.BANGGIAs.Add(newBG);
            }
            db.SaveChanges();
            return RedirectToAction("Index");
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
            var item = db.BANGGIAs.Where(bg => bg.ID_BANGGIA == id).FirstOrDefault();
            item.lsSach = db.SACHes.ToList();
            DateTime needDate = DateTime.Now.AddDays(1);
            string date = needDate.Day.ToString();
            if (date.Length == 1)
                date = "0" + date;
            string month = needDate.Month.ToString();
            if (month.Length == 1)
                month = "0" + month;
            item.minDate = needDate.Year.ToString() + "-" + month + "-" + date;
            return View(item);
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
            else
            {
                check.GIATRI = bg.GIATRI;
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
                    if (item.TANG_GIAM == true)
                    {
                        check.GIA_BAN += item.GIATRI.Value;
                    }
                    else
                    {
                        check.GIA_BAN -= item.GIATRI.Value;
                    }
                    db.BANGGIAs.Remove(item);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }
        public bool HetHan(BANGGIA bg)
        {
            int ngay = int.Parse(bg.NGAY_APDUNG.Substring(8, 2));
            // 2008-03-09
            int thang = int.Parse(bg.NGAY_APDUNG.Substring(5, 2));
            int nam = int.Parse(bg.NGAY_APDUNG.Substring(0, 4));
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