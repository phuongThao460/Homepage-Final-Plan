using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Homepage.Models;

namespace Homepage.Controllers.Admin
{
    public class StatisticController : Controller
    {
        BookshopEntity db = new BookshopEntity();
        // GET: Statistic
        public ActionResult Index()
        {
            StatisticClass stc = new StatisticClass();
            return View(stc);
        }
    }
}