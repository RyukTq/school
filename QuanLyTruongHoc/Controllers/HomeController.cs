using QuanLyTruongHoc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyTruongHoc.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            QuanlytruonghocDataContext context = new QuanlytruonghocDataContext();
            List<@new> dsNews = context.news
                                            .Where(x => x.newsgroup_id == 1 && x.status == true)
                                            .OrderByDescending(x => x.created_at)
                                            .ToList();

            return View(dsNews);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}