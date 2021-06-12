using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Xml.Linq;
using System.Linq;
using System.Net;
using System.Web;
using System.Security.Cryptography;
using System.Web.Mvc;
using Homepage.Models;

namespace Homepage.Controllers.Admin
{
    public class AdminAccountController : Controller
    {
        BookshopEntity db = new BookshopEntity();
        // GET: AdminAccount
        public ActionResult Index()
        {
            return View(db.TAIKHOANQUANTRIVIENs.ToList());
        }
        // GET: Administrator/USER/Create
        public ActionResult Create()
        {
            TAIKHOANQUANTRIVIEN t = new TAIKHOANQUANTRIVIEN();
            return View(t);
        }

        // POST: Administrator/USER/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TAIKHOANQUANTRIVIEN admin)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // TODO: Add insert logic here
                    if (admin.ImageUpload != null)
                    {
                        string file = Path.GetFileNameWithoutExtension(admin.ImageUpload.FileName);
                        string extension = Path.GetExtension(admin.ImageUpload.FileName);
                        file = file + extension;
                        admin.ANH_DAIDIEN = "/Content/Images/" + file;
                        //pro.MoreImages= "~/Contents/images/" + file;
                        admin.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/Images/"), file));
                    }
                    admin.MATKHAU = GetMD5(admin.MATKHAU);
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.TAIKHOANQUANTRIVIENs.Add(admin);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    return View();
                }
            }
            return View(admin);

        }

        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = System.Text.Encoding.ASCII.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TAIKHOANQUANTRIVIEN ad = db.TAIKHOANQUANTRIVIENs.Find(id);
            if (ad == null)
            {
                return HttpNotFound();
            }
            return View(ad);
        }

        // POST: Administrator/USER/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_QUANTRIVIEN,TEN_DANGNHAP,MATKHAU,TEN_QUANTRIVIEN,ANH_DAIDIEN")] TAIKHOANQUANTRIVIEN ad)
        {
            if (ModelState.IsValid)
            {
                ad.MATKHAU = GetMD5(ad.MATKHAU);
                db.Configuration.ValidateOnSaveEnabled = false;
                db.Entry(ad).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ad);
        }

        // GET: Administrator/USER/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TAIKHOANQUANTRIVIEN ad = db.TAIKHOANQUANTRIVIENs.Find(id);
            if (ad == null)
            {
                return HttpNotFound();
            }
            return View(ad);
        }

        // POST: Administrator/USER/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TAIKHOANQUANTRIVIEN ad = db.TAIKHOANQUANTRIVIENs.Find(id);
            db.TAIKHOANQUANTRIVIENs.Remove(ad);
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