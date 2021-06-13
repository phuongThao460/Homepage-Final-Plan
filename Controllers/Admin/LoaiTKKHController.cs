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
        public ActionResult Edit(LOAITAIKHOAN s)
        {
            int id = int.Parse(Url.RequestContext.RouteData.Values["id"].ToString());
            var check = db.LOAITAIKHOANs.Where(l => l.MUC_DATDUOC == s.MUC_DATDUOC).FirstOrDefault();
            if(check!= null)
            {
                ViewBag.ErrorContent = "Mức đạt được bị trùng.";
                return View(s);
            }
            var updateType = db.LOAITAIKHOANs.Where(l => l.ID_LOAITK == id).FirstOrDefault();
            updateType.TEN_LOAITK = s.TEN_LOAITK;
            updateType.MUC_DATDUOC = s.MUC_DATDUOC;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
