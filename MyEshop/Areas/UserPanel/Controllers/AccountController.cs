using DataLayer;
using DataLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
namespace MyEshop.Areas.UserPanel.Controllers
{
    public class AccountController : UserBaseController
    {
        private MyEShop_DBEntities db = new MyEShop_DBEntities();
        // GET: UserPanel/Account

        #region ChangePassword


        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel change)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.Single(u => u.UserName == User.Identity.Name);
                string oldPasswordHash =
                    FormsAuthentication.HashPasswordForStoringInConfigFile(change.CurrentPassword, "MD5");
                if (user.Password == oldPasswordHash)
                {
                    string hashNewPasword =
                        FormsAuthentication.HashPasswordForStoringInConfigFile(change.Password, "MD5");
                    user.Password = hashNewPasword;
                    db.SaveChanges();
                    ViewBag.Success = true;
                }
                else
                {
                    ModelState.AddModelError("OldPassword", "کلمه عبور فعلی درست نمی باشد");
                }
            }
            return View();
        }
        #endregion

        #region IndexUserPanel

        public ActionResult Index()
        {
            return View();
        }

        #endregion

        //list Factor

        #region Factor

        public ActionResult ListFactor()
        {
            int userId = db.Users.Single(p => p.UserName == User.Identity.Name).UserID;
            var Factor = db.Orders.Where(p => p.UserID == userId);

            List<FactorViewModel> listFactor = new List<FactorViewModel>();
            foreach (var item in Factor)
            {
                listFactor.Add(new FactorViewModel()
                {
                    FactorID = item.OrderID,
                    DateFactor = item.Date,
                    IsFinally = item.IsFinaly,
                });
            }
            return View(listFactor);
        }

        #endregion

        #region DetailFactor
        public ActionResult DetailFactor(int id)
        {
            var detailFactor = db.OrderDetails.Where(O => O.OrderID == id);
            List<FactorDetailsViewModel> factorDetails = new List<FactorDetailsViewModel>();
            foreach (var item in detailFactor)
            {
                factorDetails.Add(new FactorDetailsViewModel()
                {
                    ProductId = item.ProductID,
                    ProductName = item.Product.Title,
                    Count = item.Count,
                    Price = item.Price,
                    Sum = item.Count * item.Price
                });
            }

            ViewBag.FactorId = id;
            return View(factorDetails);
        }

        #endregion
    }
}