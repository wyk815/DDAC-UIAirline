using Airline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Airline.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/
        UIAirlinesEntities db = new UIAirlinesEntities();
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(tblUserAccount model, string returnUrl)
        {
    
            
            var dataItem = db.tblUserAccounts.Where(x => x.Username == model.Username && x.Password == model.Password).FirstOrDefault();
            if (dataItem != null)
            {
                FormsAuthentication.SetAuthCookie(dataItem.Username, false);
                if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                         && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                {
                    Session["loginID"] = dataItem.UserID;
                    return Redirect(returnUrl);
                }
                else
                {
                    Session["loginID"] = dataItem.UserID;
                    return RedirectToAction("Index","Home");
                }
            }
            else
            {
                ModelState.AddModelError("", "Invalid user/pass");
                return View();
            }
        }
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(tblUserAccount model)
        {
            tblUserAccount tbl = new tblUserAccount();

            if (ModelState.IsValid)
            {
                if (db.tblUserAccounts.Any(x => x.Username == model.Username))
                {
                    ModelState.AddModelError("username", "username is not available");
                }
                else
                {
                    Guid guid = Guid.NewGuid();
                    tbl.UserID = guid;
                    tbl.Username = model.Username;
                    tbl.Password = model.Password;

                    db.tblUserAccounts.Add(tbl);
                    db.SaveChanges();
                    return RedirectToAction("Login", "Account");
                }
            }

            return View();
        }
    }
}
