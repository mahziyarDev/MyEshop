﻿using DataLayer;
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
    }
}