using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using DataLayer.ViewModel;
using System.Web.Security;
namespace MyEshop.Controllers
{
    public class AccountController : Controller
    {
        private MyEShop_DBEntities db = new MyEShop_DBEntities();
        // GET: Account
        #region Register
        [Route("Register")]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("Register")]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel register)
        {
            if (ModelState.IsValid)
            {
                if (!db.Users.Any(u => u.Email == register.Email.Trim().ToLower()))
                {
                    var user = new Users()
                    {
                        UserName = register.UserName,
                        Email = register.Email.Trim().ToLower(),
                        Password = FormsAuthentication.HashPasswordForStoringInConfigFile(register.Password, "MD5"),
                        RegisterDate = DateTime.Now,
                        ActiveCode = Guid.NewGuid().ToString(),
                        IsActive = false,
                        RoleID = 1,
                    };
                    db.Users.Add(user);
                    db.SaveChanges();

                    //start Send Active Email
                    string body = PartialToStringClass.RenderPartialView("ManageEmails", "ActivationEmail", user);
                    SendEmail.Send(user.Email, "ایمیل فعالسازی", body);
                    //End Send Active Email

                    return View("SuccessRegister", user);
                }
                else
                {
                    ModelState.AddModelError("Email", "ایمیل وارد شده قبلا در سایت ثبت نام کرده است");
                }
            }
            return View(register);
        }




        #endregion

        //for Login

        #region Login

        [Route("Login")]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("Login")]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel login, string ReturnUrl = "/")
        {
            if (ModelState.IsValid)
            {
                string hashPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(login.Password, "MD5");

                var user = db.Users.SingleOrDefault(u => u.Email == login.Email && u.Password == hashPassword);
                if (user != null)
                {
                    if (user.IsActive)
                    {
                        FormsAuthentication.SetAuthCookie(user.UserName, login.RememberMe);
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        ModelState.AddModelError("Email", "حساب کاربری شما فعال نیست");
                    }
                }
                else
                {
                    ModelState.AddModelError("Email", "کاربری با مشخصات وارد شده موجود نیست");
                }
            }

            return View(login);
        }



        #endregion

        //For LogOut User

        #region LogOut

        [Route("LogOut")]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return Redirect("/Login");
        }


        #endregion

        //for Active User

        #region ActiveUser

        public ActionResult ActiveUser(string ActiveCodes)
        {
            var user = db.Users.SingleOrDefault(u => u.ActiveCode == ActiveCodes);
            if (user == null)
            {
                return HttpNotFound();
            }
            user.IsActive = true;
            user.ActiveCode = Guid.NewGuid().ToString();
            db.SaveChanges();
            ViewBag.UserName = user.UserName;
            return View();
        }

        #endregion



        //ForGotPassword

        #region ForGotPassword

        [Route("ForgotPassword")]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [Route("ForgotPassword")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(ForgotPassword forgotPassword)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.SingleOrDefault(u => u.Email == forgotPassword.Email);
                if (user != null)
                {
                    if (user.IsActive)
                    {
                        string bodyEmail = PartialToStringClass.RenderPartialView("ManageEmails", "RecoveryPassword", user);
                        SendEmail.Send(user.Email, "بازیابی کلمه عبور", bodyEmail);
                        return View("SuccessForgotPassword", user);
                    }
                    else
                    {
                        ModelState.AddModelError("Email", "حساب کاربری شما فعال نیست");
                    }
                }
                else
                {
                    ModelState.AddModelError("Email", "ایمیل وارد شده موجود نمی باشد");
                }
            }
            return View();
        }


        #endregion

        //RecoveryPassword

        #region REcoverPassword

        public ActionResult RecoveryPassword(string id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RecoveryPassword(string id, RecoveryPasswordViewModel Recovery)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.SingleOrDefault(u => u.ActiveCode == id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                user.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(Recovery.Password, "MD5");
                user.ActiveCode = Guid.NewGuid().ToString();

                db.SaveChanges();
                return Redirect("/Login?recovery=true");
            }
            return View(Recovery);
        }

        #endregion

        //list Factor

        #region Factor
        [Route("Factorlist")]
        public ActionResult ListFactor()
        {
            int userId = db.Users.Single(p => p.UserName == User.Identity.Name).UserID;
            var Factor = db.Orders.Single(p => p.UserID == userId);
          
            List<FactorViewModel> listFactor = new List<FactorViewModel>();
            listFactor.Add(new FactorViewModel()
            {
                FactorID = Factor.OrderID,
                DateFactor = Factor.Date,
                IsFinally = Factor.IsFinaly,
            });
            return View(listFactor);
        }

        #endregion

        #region DetailFactor
        [Route("DetailFactor/{id}")]
        public ActionResult DetailFactor(int id)
        {
            var detailFactor = db.OrderDetails.Where(O=>O.OrderID == id);
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