using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Homepage.Models;

namespace Homepage.Controllers
{
    public class NhaCungCapController : Controller
    {
        BookshopEntity db = new BookshopEntity();
        // GET: NhaCungCap
        public ActionResult Index()
        {
            return View(db.NHACUNGCAPs.ToList());
        }

        // GET: NhaCungCap/Details/5
        public ActionResult Details(int id)
        {
            var check = db.NHACUNGCAPs.Where(ncc => ncc.ID_NCC == id).FirstOrDefault();
            return View(check);
        }

        // GET: NhaCungCap/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NhaCungCap/Create
        [HttpPost]
        public ActionResult Create(NHACUNGCAP ncc)
        {
            try
            {
                db.NHACUNGCAPs.Add(ncc);
                db.SaveChanges();
                return RedirectToAction("Index", "NhaCungCap");
            }
            catch
            {
                return View();
            }
        }

        // GET: NhaCungCap/Edit/5
        public ActionResult Edit(int id)
        {
            return View(db.NHACUNGCAPs.Where(s => s.ID_NCC == id).FirstOrDefault());
        }

        // POST: NhaCungCap/Edit/5
        [HttpPost]
        public ActionResult Edit(NHACUNGCAP ncc)
        {
            int id = int.Parse(Url.RequestContext.RouteData.Values["id"].ToString());
            try
            {
                NHACUNGCAP d = db.NHACUNGCAPs.Where(s => s.ID_NCC == id).FirstOrDefault();
                d.TEN_NHACUNGCAP = ncc.TEN_NHACUNGCAP;
                db.SaveChanges();
                return RedirectToAction("Index", "NhaXuatBan");
            }
            catch
            {
                return View();
            }
        }
    }
}
