using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Homepage.Models;
using System.IO;
using System.Net;
using System.Data.Entity;

namespace Homepage.Controllers.Admin
{
    public class LoaiTKKHController : Controller
    {
        BookshopEntity db = new BookshopEntity();
        // GET: LoaiTKKH
        public ActionResult Index()
        {
            return View(db.LOAITAIKHOANs.OrderBy(x=>x.MUC_DATDUOC).ToList());
        }

        // GET: LoaiTKKH/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LoaiTKKH/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: LoaiTKKH/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "ID_LOAITK,TEN_LOAITK,MUC_DATDUOC")]LOAITAIKHOAN  s)
        {
            if (ModelState.IsValid)
            {
                db.LOAITAIKHOANs.Add(s);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(s);
        }

        // GET: LoaiTKKH/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOAITAIKHOAN s = db.LOAITAIKHOANs.Find(id);
            if (s == null)
            {
                return HttpNotFound();
            }
            return View(s);
        }

        // POST: LoaiTKKH/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "ID_LOAITK,TEN_LOAITK,MUC_DATDUOC")] LOAITAIKHOAN s)
        {
            if (ModelState.IsValid)
            {
                db.Entry(s).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(s);
        }

        // GET: LoaiTKKH/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LoaiTKKH/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
