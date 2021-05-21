﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Homepage.Models;
namespace Homepage.Controllers.Admin
{
    public class CustomerInfoController : Controller
    {
        BookshopEntity db = new BookshopEntity();
        // GET: CustomerInfo
        public ActionResult Index()
        {
            return View(db.THONGTINKHACHHANGs.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Create(THONGTINKHACHHANG tt)
        {
            try
            {
                tt.TONG_TIEUDUNG = 0;
                db.THONGTINKHACHHANGs.Add(tt);
                db.SaveChanges();
                var list = db.THONGTINKHACHHANGs.ToList();
                int id = list.Last().ID_TTKH;
                return RedirectToRoute(new { controller = "CustomerInfo", action = "Details", id = id });
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Details(int id)
        {
            var select = db.THONGTINKHACHHANGs.Where(s => s.ID_TTKH == id).FirstOrDefault();
            return View(select);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View(db.THONGTINKHACHHANGs.Where(kh => kh.ID_TTKH == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Edit(THONGTINKHACHHANG tt)
        {
            tt.TONG_TIEUDUNG = 0;
            db.THONGTINKHACHHANGs.Add(tt);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        
    }
}