using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Homepage.Models;
namespace Homepage.Controllers.Admin
{
    public class CustomerAccountController : Controller
    {
        BookshopEntity db = new BookshopEntity();
        // GET: CustomerAccount
        public ActionResult Index()
        {
            return View(db.TAIKHOANKHACHHANGs.ToList());
        }
        // GET: Administrator/USER/Create
        public ActionResult UpdateCustomerMember()
        {
            var lsAccount = db.TAIKHOANKHACHHANGs.Where(tk => tk.ID_TTKH != null).ToList();
            var lsAccountType = db.LOAITAIKHOANs.OrderBy(x => x.MUC_DATDUOC).ToList();
            foreach(var item in lsAccount)
            {
                for(int i = lsAccountType.Count - 1; i >= 0; i--)
                {
                    if(item.THONGTINKHACHHANG.TONG_TIEUDUNG >= lsAccountType[i].MUC_DATDUOC)
                    {
                        ChangeCustomerMember(item.ID_KHACHHANG, lsAccountType[i].ID_LOAITK);
                        break;
                    }
                }
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public void ChangeCustomerMember(int idTK, int idLoai)
        {
            var account = db.TAIKHOANKHACHHANGs.Where(tk => tk.ID_KHACHHANG == idTK).FirstOrDefault();
            account.ID_LOAITK = idLoai;
            db.SaveChanges();
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