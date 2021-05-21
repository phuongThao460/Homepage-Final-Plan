using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Homepage.Models;

namespace Homepage.Controllers
{
    public class ChiTietSanPhamController : Controller
    {
        BookshopEntity database = new BookshopEntity();
        // GET: ChiTietSanPham
        public ActionResult Index(string searchString, int theloaiID = 0)
        {
            var theloai = from c in database.THELOAIs select c;
            ViewBag.theloaiID = new SelectList(theloai, "ID_THELOAI", "TEN_THELOAI"); // danh sách Category 

            var sach = from l in database.SACHes
                       join c in database.THELOAIs on l.ID_THELOAI equals c.ID_THELOAI
                       select new { l.ID_SACH, l.TEN_SACH, l.LOAI_BIA, l.BANGGIA.GIA_BAN, l.ID_THELOAI, c.TEN_THELOAI };

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
                temp.LOAI_BIA = item.LOAI_BIA;
                temp.TEN_THELOAI = item.TEN_THELOAI;
                listSach.Add(temp);
            }

            return View(listSach);
        }
        public ActionResult Details(int id)
        {
            return View(database.SACHes.Where(s => s.ID_SACH == id).FirstOrDefault());
        }
    }
}