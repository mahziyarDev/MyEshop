using System;
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

        public ActionResult ShowComments(int id)
        {
            var comment = db.Product_comments.Where(c => c.ProductID == id);
            return PartialView(comment);
        }

        public ActionResult CreateComment(int id)
        {
            
            return PartialView(new Product_comments()
            {
                ProductID = id,
            });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateComment(Product_comments productComments)
        {
            if (ModelState.IsValid)
            {
                productComments.CreateDate = DateTime.Now;
                db.Product_comments.Add(productComments);
                db.SaveChanges();

                return PartialView("ShowComments", db.Product_comments.Where(c => c.ProductID == productComments.ProductID));
            }
            return PartialView(productComments);
        }

        [Route("Archive")]
        public ActionResult ArchiveProduct(int PageId=1 ,string Title="",int MinPrice=0,int MaxPrice=0,List<int> selectedGroups =null)
        {
            ViewBag.Groups = db.Product_Groups.ToList();
            ViewBag.ProductTitle = Title;
            ViewBag.minPrice = MinPrice;
            ViewBag.maxPrice = MaxPrice;
            ViewBag.PageId = PageId;
            ViewBag.SelectedGroup = selectedGroups;
            List<Product> list = new List<Product>();
            if (selectedGroups != null && selectedGroups.Any())
            {
                foreach (var group in selectedGroups)
                {
                    list.AddRange(db.Product_Selected_Groups.Where(g=>g.GroupID == group).Select(g=>g.Product).ToList());
                }

                list=list.Distinct().ToList();
            }
            else
            {
                list.AddRange(db.Product.ToList());
            }
            if (Title!="")
            {
                list = list.Where(p => p.Title.Contains(Title)).ToList();
            }

            if (MinPrice > 0)
            {
                list = list.Where(p => p.Price >= MinPrice).ToList();
            }
            if (MaxPrice > 0)
            {
                list = list.Where(p => p.Price <= MaxPrice).ToList();
            }

            //pagging
            int take = 9;
            int skip = (PageId - 1) * take;
            ViewBag.PAgeCount = list.Count() / take;
            return View(list.OrderByDescending(p=>p.CreateDate).Skip(skip).Take(take).ToList());
        }

    }   
}