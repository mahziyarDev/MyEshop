using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataLayer;

namespace MyEshop.Areas.Admin.Controllers
{
    public class Product_TagsController : Controller
    {
        private MyEShop_DBEntities db = new MyEShop_DBEntities();

        // GET: Admin/Product_Tags
        public ActionResult Index()
        {
            var product_Tags = db.Product_Tags.Include(p => p.Product);
            return View(product_Tags.ToList());
        }

        // GET: Admin/Product_Tags/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_Tags product_Tags = db.Product_Tags.Find(id);
            if (product_Tags == null)
            {
                return HttpNotFound();
            }
            return View(product_Tags);
        }

        // GET: Admin/Product_Tags/Create
        public ActionResult Create()
        {
            ViewBag.ProductID = new SelectList(db.Product, "ProductID", "Title");
            return View();
        }

        // POST: Admin/Product_Tags/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TagID,ProductID,Tag")] Product_Tags product_Tags)
        {
            if (ModelState.IsValid)
            {
                db.Product_Tags.Add(product_Tags);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductID = new SelectList(db.Product, "ProductID", "Title", product_Tags.ProductID);
            return View(product_Tags);
        }

        // GET: Admin/Product_Tags/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_Tags product_Tags = db.Product_Tags.Find(id);
            if (product_Tags == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductID = new SelectList(db.Product, "ProductID", "Title", product_Tags.ProductID);
            return View(product_Tags);
        }

        // POST: Admin/Product_Tags/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TagID,ProductID,Tag")] Product_Tags product_Tags)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product_Tags).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductID = new SelectList(db.Product, "ProductID", "Title", product_Tags.ProductID);
            return View(product_Tags);
        }

        // GET: Admin/Product_Tags/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_Tags product_Tags = db.Product_Tags.Find(id);
            if (product_Tags == null)
            {
                return HttpNotFound();
            }
            return View(product_Tags);
        }

        // POST: Admin/Product_Tags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product_Tags product_Tags = db.Product_Tags.Find(id);
            db.Product_Tags.Remove(product_Tags);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
