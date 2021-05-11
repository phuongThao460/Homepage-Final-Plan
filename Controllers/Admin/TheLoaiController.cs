using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Homepage.Models;

namespace Homepage.Controllers
{
    public class TheLoaiController : Controller
    {
        BookshopEntity db  = new BookshopEntity();
        public ActionResult Index()
        {
            return View(db.THELOAIs.ToList());
        }

        // GET: TheLoai/Details/5
        public ActionResult Details(int id)
        {
            return View(db.THELOAIs.Where(tl => tl.ID_THELOAI == id).FirstOrDefault());
        }

        // GET: TheLoai/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TheLoai/Create
        [HttpPost]
        public ActionResult Create(THELOAI tl)
        {
            try
            {
                db.THELOAIs.Add(tl);
                db.SaveChanges();
                return RedirectToAction("Index", "TheLoai");
            }
            catch
            {
                return View();
            }
        }

        // GET: TheLoai/Edit/5
        public ActionResult Edit(int id)
        {
            return View(db.THELOAIs.Where(s => s.ID_THELOAI == id).FirstOrDefault());
        }

        // POST: TheLoai/Edit/5
        [HttpPost]
        public ActionResult Edit(THELOAI tl)
        {
            int id = int.Parse(Url.RequestContext.RouteData.Values["id"].ToString());
            try
            {
                THELOAI d = db.THELOAIs.Where(s => s.ID_THELOAI == tl.ID_THELOAI).FirstOrDefault();
                d.TEN_THELOAI = tl.TEN_THELOAI;
                db.SaveChanges();
                return RedirectToAction("Index", "TheLoai");
            }
            catch
            {
                return View();
            }
        }
    }
}
