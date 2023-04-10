using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace QuanLyTruongHoc.Controllers
{
    [Authorize]
    public class LogoutController : Controller
    {
        // GET: Logout
        public ActionResult Index()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return Redirect("/login");
        }
    }
}