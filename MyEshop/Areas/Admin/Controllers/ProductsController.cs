using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using InsertShowImage;
using KooyWebApp_MVC.Classes;

namespace MyEshop.Areas.Admin.Controllers
{
    public class ProductsController : Controller
    {
        private MyEShop_DBEntities db = new MyEShop_DBEntities();

        // GET: Admin/Products
        public ActionResult Index()
        {
            return View(db.Product.ToList());
        }

        // GET: Admin/Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Admin/Products/Create
        public ActionResult Create()
        {
            ViewBag.groups = db.Product_Groups.ToList();
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,Title,ShortDescription,Text,Price,ImageName,CreateDate")] Product products, List<int> selectedGroups, HttpPostedFileBase imageProduct, string tags)
        {
            if (ModelState.IsValid)
            {
                if (selectedGroups == null)
                {
                    ViewBag.ErrorSelectedGroup = true;
                    ViewBag.Groups = db.Product_Groups.ToList();
                    return View(products);
                }
                products.ImageName = "images.jpg";
                if (imageProduct != null && imageProduct.IsImage())
                {
                    products.ImageName = Guid.NewGuid().ToString() + Path.GetExtension(imageProduct.FileName);
                    imageProduct.SaveAs(Server.MapPath("/Images/ProductImages/" + products.ImageName));
                    ImageResizer img = new ImageResizer();
                    img.Resize(Server.MapPath("/Images/ProductImages/" + products.ImageName),
                        Server.MapPath("/Images/ProductImages/Thumb/" + products.ImageName));
                }
                products.CreateDate = DateTime.Now;
                db.Product.Add(products);

                foreach (int selectedGroup in selectedGroups)
                {
                    db.Product_Selected_Groups.Add(new Product_Selected_Groups()
                    {
                        ProductID = products.ProductID,
                        GroupID = selectedGroup
                    });
                }

                if (!string.IsNullOrEmpty(tags))
                {
                    string[] tag = tags.Split(',');
                    foreach (string t in tag)
                    {
                        db.Product_Tags.Add(new Product_Tags()
                        {
                            ProductID = products.ProductID,
                            Tag = t.Trim()
                        });
                    }
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Groups = db.Product_Groups.ToList();
            return View(products);
        }

        // GET: Admin/Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            ViewBag.SelectedGroups = product.Product_Selected_Groups.ToList();
            ViewBag.groups = db.Product_Groups.ToList();
            ViewBag.Tags = string.Join(",", product.Product_Tags.Select(t => t.Tag).ToList());
            return View(product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,Title,ShortDescription,Text,Price,CreateDate,ImageName")] Product product, List<int> selectedGroups, HttpPostedFileBase imageProduct, string tags)
        {
            if (ModelState.IsValid)
            {
                if (imageProduct != null && imageProduct.IsImage())
                {
                    if (product.ImageName != "images.jpg")
                    {
                        System.IO.File.Delete(Server.MapPath("/Images/ProductImages" + product.ImageName));
                        System.IO.File.Delete(Server.MapPath("/Images/ProductImages/Thumb" + product.ImageName));
                    }
                    product.ImageName = Guid.NewGuid().ToString() + Path.GetExtension(imageProduct.FileName);
                    imageProduct.SaveAs(Server.MapPath("/Images/ProductImages/" + product.ImageName));
                    ImageResizer img = new ImageResizer();
                    img.Resize(Server.MapPath("/Images/ProductImages/" + product.ImageName),
                        Server.MapPath("/Images/ProductImages/Thumb/" + product.ImageName));
                }


                db.Entry(product).State = EntityState.Modified;

                db.Product_Tags.Where(t => t.ProductID == product.ProductID).ToList()
                    .ForEach(t => db.Product_Tags.Remove(t));
                if (!string.IsNullOrEmpty(tags))
                {
                    string[] tag = tags.Split(',');
                    foreach (string t in tag)
                    {
                        db.Product_Tags.Add(new Product_Tags()
                        {
                            ProductID = product.ProductID,
                            Tag = t.Trim()
                        });
                    }
                }

                db.Product_Selected_Groups.Where(g => g.ProductID == product.ProductID).ToList()
                    .ForEach(g => db.Product_Selected_Groups.Remove(g));
                foreach (int selectedGroup in selectedGroups)
                {
                    db.Product_Selected_Groups.Add(new Product_Selected_Groups()
                    {
                        ProductID = product.ProductID,
                        GroupID = selectedGroup
                    });
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SelectedGroups = selectedGroups;
            ViewBag.groups = db.Product_Groups.ToList();
            ViewBag.Tags = tags;

            return View(product);
        }

        // GET: Admin/Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Product.Find(id);
            db.Product.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        //for gallery
        public ActionResult Gallery(int id)
        {
            ViewBag.ProductGallery = db.Product_Galleries.Where(p => p.ProductID == id).ToList();
            return View(new Product_Galleries()
            {
                ProductID = id
            });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Gallery(Product_Galleries galleries,HttpPostedFileBase imgUp)
        {

            if (ModelState.IsValid)
            {
                if (imgUp != null && imgUp.IsImage())
                {
                    galleries.ImageName = Guid.NewGuid().ToString() + Path.GetExtension(imgUp.FileName);
                    imgUp.SaveAs(Server.MapPath("/Images/ProductImages/" + galleries.ImageName));
                    ImageResizer img = new ImageResizer();
                    img.Resize(Server.MapPath("/Images/ProductImages/" + galleries.ImageName),
                        Server.MapPath("/Images/ProductImages/Thumb/" + galleries.ImageName));
                    db.Product_Galleries.Add(galleries);
                    db.SaveChanges();
                }
            }

            return RedirectToAction("Gallery", new {id = galleries.ProductID});
        }

        public ActionResult DeleteGallery(int id)
        {
            var gallery = db.Product_Galleries.Find(id);

            System.IO.File.Delete(Server.MapPath("/Images/ProductImages/" + gallery.ImageName));
            System.IO.File.Delete(Server.MapPath("/Images/ProductImages/Thumb/" + gallery.ImageName));

            db.Product_Galleries.Remove(gallery);
            db.SaveChanges();
            return RedirectToAction("Gallery", new { id = gallery.ProductID });
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
