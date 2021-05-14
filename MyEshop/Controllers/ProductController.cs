using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;

namespace MyEshop.Controllers
{
    public class ProductController : Controller
    {
        private MyEShop_DBEntities db = new MyEShop_DBEntities();
        public ActionResult ShowGroups()
        {
            var Groups = db.Product_Groups.ToList();
            return PartialView(Groups);
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}