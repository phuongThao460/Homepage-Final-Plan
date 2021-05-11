using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Homepage.Models;

namespace Homepage.Controllers
{
    public class BookImageController : Controller
    {
        BookshopEntity db = new BookshopEntity();
        int url;
        // GET: BookImage

        public ActionResult Index()
        {
            url = int.Parse(Url.RequestContext.RouteData.Values["id"].ToString());
            var list = db.ANHBIAs.Where(s => s.ID_SACH == url);

            ViewBag.Id = url;
            return View(list);
        }
        [HttpGet]
        public ActionResult UploadImages()
        {
            return View();
        }
        [HttpPost]
        public RedirectToRouteResult UploadImages(ANHBIA anhBia)
        {
            url = int.Parse(Url.RequestContext.RouteData.Values["id"].ToString());
            if (anhBia.ImageFile != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(anhBia.ImageFile.FileName);
                string extent = Path.GetExtension(anhBia.ImageFile.FileName);
                fileName = fileName + extent;
                anhBia.ANH = "~/Content/Images/" + fileName;
                anhBia.ImageFile.SaveAs(Path.Combine(Server.MapPath("~/Content/Images/"),fileName));
            }
            anhBia.ID_SACH = url;
            db.ANHBIAs.Add(anhBia);
            db.SaveChanges();
            return RedirectToRoute(new { controller = "BookImage", action = "Index", id = url });
        }
        [HttpGet]
        public ActionResult Edit()
        {
            int id = int.Parse(Url.RequestContext.RouteData.Values["id"].ToString());

            var select = db.ANHBIAs.Where(anh => anh.ID_ANH == id).FirstOrDefault();
            return View(select);
        }
        [HttpPost]
        public RedirectToRouteResult Edit(ANHBIA anhBia)
        {
            int id = int.Parse(Url.RequestContext.RouteData.Values["id"].ToString());
            if (anhBia.ImageFile != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(anhBia.ImageFile.FileName);
                string extent = Path.GetExtension(anhBia.ImageFile.FileName);
                fileName = fileName + extent;
                anhBia.ANH = "~/Content/Images/" + fileName;
                anhBia.ImageFile.SaveAs(Path.Combine(Server.MapPath("~/Content/Images/"), fileName));
            }
            var select = db.ANHBIAs.Where(anh => anh.ID_ANH == id).FirstOrDefault();
            select.ANH = anhBia.ANH;
            db.SaveChanges();
            return RedirectToRoute(new { controller = "BookImage", action = "Index", id = select.ID_SACH });
        }

        public RedirectToRouteResult Delete()
        {
            int id = int.Parse(Url.RequestContext.RouteData.Values["id"].ToString());
            var select = db.ANHBIAs.Where(anh => anh.ID_ANH == id).FirstOrDefault();
            int idSach = int.Parse(select.ID_SACH.ToString());
            db.ANHBIAs.Remove(select);
            db.SaveChanges();
            return RedirectToRoute(new { controller="BookImage", action="Index",id = idSach});
        }
    }
}