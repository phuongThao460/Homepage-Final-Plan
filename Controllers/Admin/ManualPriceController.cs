using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Homepage.Models;

namespace Homepage.Controllers.Admin
{
    public class ManualPriceController : Controller
    {
        BookshopEntity db = new BookshopEntity();
        public ActionResult Index()
        {
            return View(db.SACHes.ToList());
        }
        [HttpGet]
        public PartialViewResult UpdatePrice()
        {
            return PartialView();
        }
        [HttpPost]
        public RedirectToRouteResult UpdatePrice(SACH newPrice)
        {
            if (newPrice.GIA_BAN < 0)
            {
                return RedirectToAction("Index");
            }
            else
            {
                var check = db.SACHes.Where(s => s.ID_SACH == newPrice.ID_SACH).FirstOrDefault();
                check.GIA_BAN = newPrice.GIA_BAN;
                db.SaveChanges();
                return RedirectToAction("Index");
            }


        }
    }
}