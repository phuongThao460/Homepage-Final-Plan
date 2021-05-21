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
            return View(db.SACHes.ToList()); //no bao thieu cham phay cho nao kia
        }

        public ActionResult GetNameTG()
        {
            int id = int.Parse(Url.RequestContext.RouteData.Values["id"].ToString());
            var tacgia = db.TACGIAs.Where(tg => tg.ID_TACGIA == id).FirstOrDefault();
            return PartialView(tacgia);
        }
        public ActionResult Details(int id)
        {
            return View(db.SACHes.Where(s => s.ID_SACH == id).FirstOrDefault());
        }
    }
}