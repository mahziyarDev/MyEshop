﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using DataLayer.ViewModel;

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

        public ActionResult LastProduct()
        {
            var product = db.Product.OrderByDescending(p => p.CreateDate).Take(12);
            return PartialView(product);
        }

        [Route("ShowProduct/{id}")]
        public ActionResult ShowProduct(int? id)
        {
            var product = db.Product.Find(id);
            ViewBag.ProductFeatures = product.Product_Features.DistinctBy(f => f.FeatureID).Select(f => new ShowProductFeatureViewModel()
            {
                FeatureTitlea = f.Features.FeatureTitle,
                Values = db.Product_Features.Where(fe => fe.FeatureID == f.FeatureID).Select(fe => fe.Value).ToList()
            }).ToList();
            if (product==null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
    }   
}