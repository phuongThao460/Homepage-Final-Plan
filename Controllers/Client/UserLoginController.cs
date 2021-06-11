using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Homepage.Models;

namespace Homepage.Controllers.Customer
{
    public class UserLoginController : Controller
    {
        // GET: UserLogin
        BookshopEntity db = new BookshopEntity();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]     
        public ActionResult LoginAccount(FormCollection collection)
        {
            var tendn = collection["TEN_DANGNHAP"];
            var matkhau = collection["MATKHAU"];
            if (string.IsNullOrEmpty(tendn))
            {
                ViewData["Loi1"] = "Username not empty";
            }
            else if (string.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = "Password not empty";
            }
            else
            {
                TAIKHOANKHACHHANG kh = db.TAIKHOANKHACHHANGs.SingleOrDefault(n => n.TEN_DANGNHAP == tendn && n.MATKHAU == matkhau);
                if (kh != null)
                {
                    db.Configuration.ValidateOnSaveEnabled = false;
                    THONGTINKHACHHANG tt = db.THONGTINKHACHHANGs.SingleOrDefault(t => t.ID_TTKH == kh.ID_TTKH);
                    Session["TEN_DANGNHAP"] = tt.TEN_KHACHHANG;
                    Session["ID_TTKH"] = tt.ID_TTKH;
                    Session["EMAIL_KHACHHANG"] = tt.EMAIL_KHACHHANG;
                    Session["DIACHI"] = tt.DIACHI;
                    Session["SO_DIENTHOAI"] = tt.SO_DIENTHOAI;
                    return RedirectToAction("Index", "uHome");
                }
                else
                {
                    ViewBag.ThongBao = "Username or password is incorrect";
                }
            }
            return View();
        }
        public ActionResult RegisterUser()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegisterUser(TAIKHOANKHACHHANG _user, THONGTINKHACHHANG tt)
        {
            if (ModelState.IsValid)
            {
                var check_ID = db.TAIKHOANKHACHHANGs.Where(s => s.ID_KHACHHANG == _user.ID_KHACHHANG)
                    .FirstOrDefault();
                if (check_ID == null)
                {
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.THONGTINKHACHHANGs.Add(tt);
                    _user.ID_TTKH = tt.ID_TTKH;
                    db.TAIKHOANKHACHHANGs.Add(_user);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.ErrorRegister = "This ID is exist";
                    return View();
                }
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session["TEN_DANGNHAP"] = null;
            return Redirect("/");
        }
    }
}