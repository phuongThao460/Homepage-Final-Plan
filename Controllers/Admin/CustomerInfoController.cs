using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}