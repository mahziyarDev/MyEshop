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
        public ActionResult ShowCart()
        {
            List<ShopCartItemViewModel> list = new List<ShopCartItemViewModel>();
            if (Session["ShopCart"]!=null)
            {
                List<ShopCartItem> listShop = (List<ShopCartItem>)Session["ShopCart"];
                foreach (var item in listShop)
                {
                    var product = db.Product
                        .Where(p => p.ProductID == item.ProductID)
                        .Select(p=>new
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
    }
}