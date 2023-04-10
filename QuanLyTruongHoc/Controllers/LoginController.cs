using QuanLyTruongHoc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace QuanLyTruongHoc.Controllers
{
    public class LoginController : Controller
    {
        QuanlytruonghocDataContext context = new QuanlytruonghocDataContext();

        // GET: Login
        public ActionResult Index(string returnUrl)
        {
            if(User.Identity.IsAuthenticated)
            {
                if (!string.IsNullOrWhiteSpace(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            if (Request.Form.Count > 0)
            {
                var email = Request.Form["email"];
                var matkhau = Request.Form["matkhau"];

                if (checkPassword(email, matkhau))
                {
                    var userInfo = getUserInfo(email);

                    /*Session.Add("LoginSession", "true");*/
                    Session.Add("EmailSession", userInfo[0].email);
                    Session.Add("NameSession", userInfo[0].name);
                    Session.Add("UserGroupSession", userInfo[0].usergroup_id);
                    Session.Add("UserIdSession", userInfo[0].id);

                    FormsAuthentication.SetAuthCookie(email, true);

                    if (!string.IsNullOrWhiteSpace(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                    return RedirectToAction("Index");
            }
            return View();
        }

        public bool checkPassword(string email, string matkhau)
        {
            var user = context.users
                        .Where(x => x.email == email && x.password == matkhau)
                        .ToList().Count();
            if (user != 0)
                return true;
            else
                return false;
        }

        public List<user> getUserInfo(string email)
        {
            var user = context.users
                        .Where(x => x.email == email)
                        .ToList();
            return user;
        }
    }
}