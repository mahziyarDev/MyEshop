using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebSockets;
using DataLayer;
using DataLayer.ViewModel;

namespace MyEshop.Controllers
{
    public class ShopCartController : Controller
    {
        private MyEShop_DBEntities db = new MyEShop_DBEntities();
        // GET: ShopCart

        #region ShowCart

        public ActionResult ShowCart()
        {
            List<ShopCartItemViewModel> list = new List<ShopCartItemViewModel>();
            if (Session["ShopCart"] != null)
            {
                List<ShopCartItem> listShop = (List<ShopCartItem>)Session["ShopCart"];
                foreach (var item in listShop)
                {
                    var product = db.Product
                        .Where(p => p.ProductID == item.ProductID)
                        .Select(p => new
                        {
                            p.Title,
                            p.ImageName

                        })
                        .Single();

                    list.Add(new ShopCartItemViewModel()
                    {
                        Count = item.Count,
                        ProductID = item.ProductID,
                        Title = product.Title,
                        ImageName = product.ImageName
                    });
                }
            }
            return PartialView(list);
        }

        #endregion

        public ActionResult Index()
        {
            return View();
        }

        #region FunctionForFetList
        List<ShowOrderViewModel> getListOrder()
        {
            List<ShowOrderViewModel> list = new List<ShowOrderViewModel>();

            if (Session["ShopCart"] != null)
            {
                List<ShopCartItem> listShop = Session["ShopCart"] as List<ShopCartItem>;

                foreach (var item in listShop)
                {
                    var product = db.Product.Where(p => p.ProductID == item.ProductID).Select(p => new
                    {
                        p.ImageName,
                        p.Title,
                        p.Price
                    }).Single();
                    list.Add(new ShowOrderViewModel()
                    {
                        Count = item.Count,
                        ProductID = item.ProductID,
                        Price = product.Price,
                        ImageName = product.ImageName,
                        Title = product.Title,
                        Sum = item.Count * product.Price
                    });
                }
            }
            return list;
        }
        #endregion

        

        #region order

        public ActionResult Order()
        {

            return PartialView(getListOrder());
        }

        #endregion

        #region ForCommandOrder

        public ActionResult CommandOrder(int id, int Count)
        {
            List<ShopCartItem> listShop = Session["ShopCart"] as List<ShopCartItem>;
            int index = listShop.FindIndex(p => p.ProductID == id);
            if (Count==0)
            {
                listShop.RemoveAt(index);
            }
            else
            {
                listShop[index].Count = Count;
            }

            Session["ShopCart"] = listShop;

            return PartialView("Order", getListOrder());
        }

        #endregion

        #region Payment
        [Authorize]
        public ActionResult Payment()
        {
            int userId = db.Users.Single(u => u.UserName == User.Identity.Name).UserID;
            Orders order = new Orders()
            {
                UserID = userId,
                Date = DateTime.Now,
                IsFinaly = false,
            };
            db.Orders.Add(order);

            var listDetails = getListOrder();
            foreach (var item in listDetails)
            {
                db.OrderDetails.Add(new OrderDetails()
                {
                    Count = item.Count,
                    OrderID = order.OrderID,
                    Price = item.Price,
                    ProductID = item.ProductID,

                });
            }

            db.SaveChanges();

            //TODO : ONLINE PAYMENT

            return null;
        }

        #endregion
    }
}