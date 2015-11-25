using LicensePlatesDBFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LicensePlatesDBFirst.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            if (db.Users.Select(u => u.Id).Contains(UserId))
            {
                return RedirectToAction("History", "Games");
            }

            return RedirectToAction("Login");
        }

        // GET: Home/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Home/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string username)
        {
            var user = db.Users.Where(u => u.UserName.Equals(username)).FirstOrDefault();

            if (user == null)
            {
                //var errMsg = "Error: Username '" + username + "' not found.";
                //return RedirectToAction("Login", "Home", errMsg);

                user = new User();
                user.UserName = username;
                user.Password = "pass";

                db.Users.Add(user);
                db.SaveChanges();
            }

            UserId = db.Users.Where(u => u.UserName.Equals(username)).Select(s => s.Id).First();
            UserName = username;
            return RedirectToAction("History", "Games");
        }

        public ActionResult Logout()
        {
            UserId = 0;
            UserName = null;
            G = null;

            return RedirectToAction("Login");
        }
    }
}