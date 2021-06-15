using System;
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
            return View(db.THONGTINKHACHHANGs.OrderBy(x => x.TONG_TIEUDUNG).ToList());
        }
        public ActionResult Details(int id)
        {
            var select = db.THONGTINKHACHHANGs.Where(s => s.ID_TTKH == id).FirstOrDefault();
            return View(select);
        }
    }
}