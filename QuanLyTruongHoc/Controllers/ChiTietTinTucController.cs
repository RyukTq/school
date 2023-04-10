using QuanLyTruongHoc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyTruongHoc.Controllers
{
    public class ChiTietTinTucController : Controller
    {
        // GET: ChiTietTinTuc
        public ActionResult Index(string id)
        {
            /*string slug = Request.QueryString["slug"];*/

            if( id == null)
            {
                return Redirect("/");
            }

            QuanlytruonghocDataContext context = new QuanlytruonghocDataContext();
            @new p = context.news.FirstOrDefault(x => x.slug == id);


            List<@new> dsLatestNews = context.news
                                            .Where(x => x.newsgroup_id == 1 && x.status == true)
                                            .OrderByDescending(x => x.created_at)
                                            .ToList();
            ViewData["TinMoiNhat"] = dsLatestNews;


            /* Get id of news */
            @new n = context.news.FirstOrDefault(x => x.slug == id);
            var news_id = n.id;

            List<comment> dsBinhLuan = context.comments
                                                .Where(x => x.news_id == news_id)
                                                .OrderByDescending(x => x.date)
                                                .ToList();
            ViewData["BinhLuan"] = dsBinhLuan;


            List<user> dsUser = context.users.ToList();
            ViewData["User"] = dsUser;


            return View(p);
        }
    }
}