using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using DataLayer;

namespace MyEshop.Controllers
{
    public class SearchController : Controller
    {
        private MyEShop_DBEntities db = new MyEShop_DBEntities();
        public ActionResult Index(string q)
        {
            List<Product> list = new List<Product>();

            list.AddRange(db.Product_Tags
                .Where(t=>t.Tag == q)
                .Select(t=>t.Product) .ToList());


            list.AddRange(db.Product
                .Where(p => p.Title.Contains(q)
                            || p.Title.Contains(q) 
                            || p.Text.Contains(q))
                .ToList());

            ViewBag.Search = q;
            return View(list.Distinct());
        }
    }
}