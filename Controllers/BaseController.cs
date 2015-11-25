using LicensePlatesDBFirst.Models;
using LicensePlatesDBFirst.SessionManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LicensePlatesDBFirst.Controllers
{
    public class BaseController : Controller
    {
        protected LicensePlatesDBFirstEntities db = new LicensePlatesDBFirstEntities();

        public Game G
        {
            get { return AppSession.G; }
            set { AppSession.G = value; }
        }

        public int UserId
        {
            get { return AppSession.UserId; }
            set { AppSession.UserId = value; }
        }

        public string UserName
        {
            get { return AppSession.UserName; }
            set { AppSession.UserName = value; }
        }
    }
}