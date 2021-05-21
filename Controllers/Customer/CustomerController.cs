using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Homepage.Models;

namespace Homepage.Controllers.Customer
{
    public class CustomerController : Controller
    {
        private BookshopEntity db = new BookshopEntity();

        // GET: Customer
        public ActionResult Index()
        {
            return View(db.THONGTINKHACHHANGs.ToList());
        }

        // GET: Customer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            THONGTINKHACHHANG tHONGTINKHACHHANG = db.THONGTINKHACHHANGs.Find(id);
            if (tHONGTINKHACHHANG == null)
            {
                return HttpNotFound();
            }
            return View(tHONGTINKHACHHANG);
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_TTKH,TEN_KHACHHANG,EMAIL_KHACHHANG,DIACHI,SO_DIENTHOAI,TONG_TIEUDUNG")] THONGTINKHACHHANG tHONGTINKHACHHANG)
        {
            if (ModelState.IsValid)
            {
                db.THONGTINKHACHHANGs.Add(tHONGTINKHACHHANG);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tHONGTINKHACHHANG);
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            THONGTINKHACHHANG tHONGTINKHACHHANG = db.THONGTINKHACHHANGs.Find(id);
            if (tHONGTINKHACHHANG == null)
            {
                return HttpNotFound();
            }
            return View(tHONGTINKHACHHANG);
        }

        // POST: Customer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_TTKH,TEN_KHACHHANG,EMAIL_KHACHHANG,DIACHI,SO_DIENTHOAI,TONG_TIEUDUNG")] THONGTINKHACHHANG tHONGTINKHACHHANG)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tHONGTINKHACHHANG).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tHONGTINKHACHHANG);
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            THONGTINKHACHHANG tHONGTINKHACHHANG = db.THONGTINKHACHHANGs.Find(id);
            if (tHONGTINKHACHHANG == null)
            {
                return HttpNotFound();
            }
            return View(tHONGTINKHACHHANG);
        }

        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            THONGTINKHACHHANG tHONGTINKHACHHANG = db.THONGTINKHACHHANGs.Find(id);
            db.THONGTINKHACHHANGs.Remove(tHONGTINKHACHHANG);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
