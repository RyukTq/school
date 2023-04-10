using QuanLyTruongHoc.Models;
using Slugify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyTruongHoc.Controllers
{
    public class TinTucChuyenNganhController : Controller
    {
        // GET: TinTuc
        public ActionResult Index()
        {
            QuanlytruonghocDataContext context = new QuanlytruonghocDataContext();

            List<@new> dsNews = context.news
                                            .Where(x => x.newsgroup_id == 2 && x.status == true)
                                            .OrderByDescending(x => x.created_at)
                                            .ToList();

            List<@new> dsLatestNews = context.news
                                            .Where(x => x.newsgroup_id == 1 && x.status == true)
                                            .OrderByDescending(x => x.created_at)
                                            .ToList();
            ViewData["TinMoiNhat"] = dsLatestNews;

            return View(dsNews);
        }
    }
}