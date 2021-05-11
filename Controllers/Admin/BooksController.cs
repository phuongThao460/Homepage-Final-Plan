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
        // GET: Books
        public ActionResult Index()
        {
            return View(db.SACHes.ToList());
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public RedirectToRouteResult Create(SACH newS)
        {
            db.SACHes.Add(newS);
            db.SaveChanges();
            var list = db.SACHes.ToList();
            int id = list.Last().ID_SACH;
            return RedirectToRoute(new { controller= "BookImage", action = "Index", id = id});
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
        public RedirectToRouteResult Edit(SACH sach)
        {
            int id = int.Parse(Url.RequestContext.RouteData.Values["id"].ToString());
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