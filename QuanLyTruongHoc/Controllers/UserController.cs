using QuanLyTruongHoc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyTruongHoc.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            QuanlytruonghocDataContext context = new QuanlytruonghocDataContext();
            List <@user> dsuser = context.users.OrderByDescending(x => x.created_at).ToList();

            
            return View();
        }
        public ActionResult Create()
        {
            QuanlytruonghocDataContext context = new QuanlytruonghocDataContext();
            List<@user> dsuser = context.users.OrderByDescending(x => x.created_at).ToList();
            return View();
        }
        public ActionResult Detail()
        {
            QuanlytruonghocDataContext context = new QuanlytruonghocDataContext();
            List<@user> dsuser = context.users.OrderByDescending(x => x.created_at).ToList();
            return View();
        }
        public ActionResult Delete()
        {
            QuanlytruonghocDataContext context = new QuanlytruonghocDataContext();
            List<@user> dsuser = context.users.OrderByDescending(x => x.created_at).ToList();
            return View();
        }
    }
}