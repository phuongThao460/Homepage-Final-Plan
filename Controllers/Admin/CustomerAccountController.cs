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
using System.Data.Entity.Validation;

namespace Homepage.Controllers.Admin
{
    public class CustomerAccountController : Controller
    {
        BookshopEntity db = new BookshopEntity();
        // GET: CustomerAccount
        public ActionResult Index()
        {
            var lsAccount = db.TAIKHOANKHACHHANGs.ToList();
            var lsType = db.LOAITAIKHOANs.OrderByDescending(x => x.MUC_DATDUOC).ToList();
            foreach (var item in lsAccount)
            {
                foreach (var type in lsType)
                {
                    if (item.THONGTINKHACHHANG.TONG_TIEUDUNG >= type.MUC_DATDUOC)
                    {
                        item.ID_LOAITK = type.ID_LOAITK;
                        item.ConfirmPassword = item.MATKHAU;
                        break;
                    }
                }
            }
            db.SaveChanges();

            return View(lsAccount);
        }
        public ActionResult Create()
        {
            TAIKHOANKHACHHANG t = new TAIKHOANKHACHHANG();
            ViewBag.ID_LOAITK = new SelectList(db.LOAITAIKHOANs, "ID_LOAITK", "TEN_LOAITK");
            ViewBag.ID_TTKH = new SelectList(db.THONGTINKHACHHANGs, "ID_TTKH", "TEN_KHACHHANG");
            return View(t);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TAIKHOANKHACHHANG user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // TODO: Add insert logic here
                    if (user.ImageUpload != null)
                    {
                        string file = Path.GetFileNameWithoutExtension(user.ImageUpload.FileName);
                        string extension = Path.GetExtension(user.ImageUpload.FileName);
                        file = file + extension;
                        user.ANH_DAIDIEN = "/Content/Images/" + file;
                        //pro.MoreImages= "~/Contents/images/" + file;
                        user.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/Images/"), file));
                    }
                    user.MATKHAU = GetMD5(user.MATKHAU);
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.TAIKHOANKHACHHANGs.Add(user);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    return View();
                }

            }
            ViewBag.ID_LOAITK = new SelectList(db.LOAITAIKHOANs, "ID_LOAITK", "TEN_LOAITK", user.ID_LOAITK);
            ViewBag.ID_TTKH = new SelectList(db.THONGTINKHACHHANGs, "ID_TTKH", "TEN_KHACHHANG", user.ID_TTKH);
            return View(user);

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
            TAIKHOANKHACHHANG ad = db.TAIKHOANKHACHHANGs.Find(id);
            if (ad == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_LOAITK = new SelectList(db.LOAITAIKHOANs, "ID_LOAITK", "TEN_LOAITK", ad.ID_LOAITK);
            ViewBag.ID_TTKH = new SelectList(db.THONGTINKHACHHANGs, "ID_TTKH", "TEN_KHACHHANG", ad.ID_TTKH);
            return View(ad);
        }

        // POST: Administrator/USER/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_KHACHHANG,TEN_DANGNHAP,MATKHAU,ID_LOAITK,ID_TTKH,ANH_DAIDIEN")] TAIKHOANKHACHHANG ad)
        {
            if (ModelState.IsValid)
            {
                ad.MATKHAU = GetMD5(ad.MATKHAU);
                db.Configuration.ValidateOnSaveEnabled = false;
                db.Entry(ad).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_LOAITK = new SelectList(db.LOAITAIKHOANs, "ID_LOAITK", "TEN_LOAITK", ad.ID_LOAITK);
            ViewBag.ID_TTKH = new SelectList(db.THONGTINKHACHHANGs, "ID_TTKH", "TEN_KHACHHANG", ad.ID_TTKH);
            return View(ad);
        }

        // GET: Administrator/USER/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TAIKHOANKHACHHANG ad = db.TAIKHOANKHACHHANGs.Find(id);
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
            TAIKHOANKHACHHANG ad = db.TAIKHOANKHACHHANGs.Find(id);
            db.TAIKHOANKHACHHANGs.Remove(ad);
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