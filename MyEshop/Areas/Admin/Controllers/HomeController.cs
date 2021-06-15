using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;

namespace MyEshop.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        private MyEShop_DBEntities db = new MyEShop_DBEntities();
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }

        #region Shoppinglist

        public ActionResult Shoppinglist()
        {
            return PartialView(db.Orders.ToList());
        }

        #endregion
    }
}