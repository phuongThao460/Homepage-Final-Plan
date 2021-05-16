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
        public ActionResult Index()
        {
            return View(db.FEEDBACKs.ToList());
        }
        public ActionResult Delete(int id)
        {
            db.FEEDBACKs.Remove(db.FEEDBACKs.Where(f => f.ID_FEEDBACK == id).FirstOrDefault());
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}