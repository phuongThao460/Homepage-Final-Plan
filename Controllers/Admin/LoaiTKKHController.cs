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
            return View(new LOAITAIKHOAN());
        }

        // POST: LoaiTKKH/Create
        [HttpPost]
        public ActionResult Create(LOAITAIKHOAN collection)
        {
            try
            {
                db.LOAITAIKHOANs.Add(collection);
                db.SaveChanges();
                return RedirectToAction("Index", "LoaiTKKH");
            }
            catch
            {
                return View();
            }
        }

        // GET: LoaiTKKH/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LoaiTKKH/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
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
