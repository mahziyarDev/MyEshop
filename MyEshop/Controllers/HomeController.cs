using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;

namespace MyEshop.Controllers
{
    public class HomeController : Controller
    {
        private MyEShop_DBEntities db = new MyEShop_DBEntities();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AboutUs()
        {
            return View();
        }
        public ActionResult Slider()
        {
            DateTime dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            return PartialView(db.Slider.Where(s => s.IsActive && s.StartDate <= dt && s.EndDate >= dt));
        }
    }
}