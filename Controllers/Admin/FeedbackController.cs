using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Homepage.Models;
namespace Homepage.Controllers.Admin
{
    public class FeedbackController : Controller
    {
        BookshopEntity db = new BookshopEntity();
        // GET: Feedback
        [HttpGet]
        public ActionResult Index()
        {
            return View(db.SACHes.ToList());
        }
        [HttpPost]
        public ActionResult Index(SACH sach)
        {
            if(sach.ID_SACH == -1)
            {
                ViewBag.ErrorContent = "Vui lòng chọn sách";
                return View(db.SACHes.ToList());
            }
            return RedirectToRoute(new { controller = "Feedback", action = "SeeFullFeedback", id = sach.ID_SACH });
        }
        public ActionResult Delete(int id)
        {
            db.FEEDBACKs.Remove(db.FEEDBACKs.Where(f => f.ID_FEEDBACK == id).FirstOrDefault());
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult SeeFullFeedback()
        {
            int id = int.Parse(Url.RequestContext.RouteData.Values["id"].ToString());
            var ls = db.FEEDBACKs.Where(item => item.ID_SACH == id).ToList();
            if(ls.Count <= 0)
            {
                ViewBag.State = false;
            }
            return View(ls);
        }
    }
}