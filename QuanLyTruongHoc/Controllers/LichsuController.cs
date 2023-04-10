using QuanLyTruongHoc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyTruongHoc.Controllers
{
    [Authorize(Roles = "Quản Trị, Nhân viên")]
    public class LichsuController : Controller
    {
        // GET: TinTuc
        public ActionResult Index()
        {
            QuanlytruonghocDataContext context = new QuanlytruonghocDataContext();

            List<@new> dsNews = context.news.OrderByDescending(x => x.created_at).ToList();

            List<user> dsUser = context.users.ToList();
            List<news_group> dsNews_Group = context.news_groups.ToList();
            ViewData["User"] = dsUser;
            ViewData["NhomTinTuc"] = dsNews_Group;

            return View(dsNews);
        }

    }
}