using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Homepage.Models;

namespace Homepage.Controllers
{
    public class LoginController : Controller
    {
        BookshopEntity db = new BookshopEntity();
        // GET: Login
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public RedirectToRouteResult Index(TAIKHOANQUANTRIVIEN tk)
        {
            var check = db.TAIKHOANQUANTRIVIENs.Where(temp => temp.TEN_DANGNHAP == tk.TEN_DANGNHAP && temp.MATKHAU == tk.MATKHAU).FirstOrDefault();
            if (check != null)
            {
                return RedirectToRoute(new { controller = "Books", action = "Index" });
            }
            return RedirectToRoute(new { controller = "Login", action = "Index" });
        }
        public RedirectToRouteResult Logout()
        {
  
            return RedirectToRoute(new { controller = "Login", action = "Index" });
        }
    }
}