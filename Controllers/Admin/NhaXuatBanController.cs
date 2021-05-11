using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Homepage.Models;

namespace Homepage.Controllers
{
    public class NhaXuatBanController : Controller
    {
        BookshopEntity db = new BookshopEntity();
        // GET: NhaXuatBan
        public ActionResult Index()
        {
            return View(db.NHAXUATBANs.ToList());
        }

        // GET: NhaXuatBan/Details/5
        public ActionResult Details(int id)
        {
            return View(db.NHAXUATBANs.Where(nxb => nxb.ID_NXB == id).FirstOrDefault());
        }

        // GET: NhaXuatBan/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NhaXuatBan/Create
        [HttpPost]
        public ActionResult Create(NHAXUATBAN nxb)
        {
            try
            {
                db.NHAXUATBANs.Add(nxb);
                db.SaveChanges();
                return RedirectToAction("Index", "NhaXuatBan");
            }
            catch
            {
                return View();
            }
        }

        // GET: NhaXuatBan/Edit/5
        public ActionResult Edit(int id)
        {
            return View(db.NHAXUATBANs.Where(s => s.ID_NXB == id).FirstOrDefault());
        }

        // POST: NhaXuatBan/Edit/5
        [HttpPost]
        public ActionResult Edit(NHAXUATBAN nxb)
        {
            int id = int.Parse(Url.RequestContext.RouteData.Values["id"].ToString());
            try
            {
                NHAXUATBAN d = db.NHAXUATBANs.Where(s => s.ID_NXB == id).FirstOrDefault();
                d.TEN_NHAXUATBAN = nxb.TEN_NHAXUATBAN;
                db.SaveChanges();
                return RedirectToAction("Index", "NhaXuatBan");
            }
            catch
            {
                return View();
            }
        }

        // GET: NhaXuatBan/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }
    }
}
