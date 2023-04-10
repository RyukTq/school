using QuanLyTruongHoc.Models;
using Slugify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyTruongHoc.Controllers
{
    [Authorize(Roles = "Quản Trị, Nhân viên, Giang viên")]
    public class TinTucController : Controller
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

        public ActionResult Create()
        {
            QuanlytruonghocDataContext context = new QuanlytruonghocDataContext();

            if (Request.Form.Count > 0)
            {
                var usergroup = Session["UserGroupSession"].ToString();

                SlugHelper helper = new SlugHelper();
                @new news = new @new();

                news.title = Request.Form["title"];
                news.slug = helper.GenerateSlug(Request.Form["title"]);
                news.content = Request.Unvalidated.Form["content"];
                news.created_at = DateTime.Parse(Request.Form["created_at"]);
                news.newsgroup_id = int.Parse(Request.Form["newsgroup_id"]);
                news.user_id = int.Parse(Session["UserIdSession"].ToString());

                if (usergroup == "1")       // Quan tri
                {
                    news.status = bool.Parse(Request.Form["status"]);
                }
                else if(usergroup == "3")   // Nhan vien
                {
                    news.status = false;
                }
                else if(usergroup == "2")   // Giang vien
                {
                    news.status = false;
                }


                HttpPostedFileBase file = Request.Files["imageNews"];
                if (file != null && file.FileName != "")
                {
                    string serverPath = HttpContext.Server.MapPath("~/images/tintuc");
                    string filePath = serverPath + "/" + file.FileName;
                    file.SaveAs(filePath);
                    news.imageNews = file.FileName;
                }
                else
                {
                    news.imageNews = "default-image.png";
                }

                context.news.InsertOnSubmit(news);
                context.SubmitChanges();
                return RedirectToAction("Index");
            }


            List<news_group> dsTinTuc = context.news_groups.ToList();
            ViewData["NhomTintuc"] = dsTinTuc;

            return View();
        }

        public ActionResult Details(int id)
        {
            QuanlytruonghocDataContext context = new QuanlytruonghocDataContext();

            @new p = context.news.FirstOrDefault(x => x.id == id);
            List<news_group> dsNews_Group = context.news_groups.ToList();

            ViewData["NhomTinTuc"] = dsNews_Group;
            return View(p);
        }

        public ActionResult Edit(int id)
        {
            var usergroup = Session["UserGroupSession"].ToString();

            SlugHelper helper = new SlugHelper();
            QuanlytruonghocDataContext context = new QuanlytruonghocDataContext();
            @new news = context.news.FirstOrDefault(x => x.id == id);

            if (Request.Form.Count == 0)
            {
                List<news_group> dsTinTuc = context.news_groups.ToList();
                ViewData["NhomTintuc"] = dsTinTuc;

                return View(news);
            }

            news.title = Request.Form["title"];
            news.slug = helper.GenerateSlug(Request.Form["title"]);
            news.content = Request.Unvalidated.Form["content"];
            news.created_at = DateTime.Parse(Request.Form["created_at"]);
            news.updated_at = DateTime.Now;
            news.newsgroup_id = int.Parse(Request.Form["newsgroup_id"]);

            if (usergroup == "1")        // Quan tri
            {
                news.status = bool.Parse(Request.Form["status"]);
            }
            else if (usergroup == "3")   //Nhan vien
            {
                news.status = false;
            }
            else if (usergroup == "2")  //Giang vien
            {
                news.status = false;
            }

            HttpPostedFileBase file = Request.Files["imageNews"];
            if (file != null && file.FileName != "")
            {
                string serverPath = HttpContext.Server.MapPath("~/images/tintuc");
                string filePath = serverPath + "/" + file.FileName;
                file.SaveAs(filePath);
                news.imageNews = file.FileName;
            }

            context.SubmitChanges();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Quản Trị")]
        public ActionResult Delete(int id)
        {
            QuanlytruonghocDataContext context = new QuanlytruonghocDataContext();
            @new p = context.news.FirstOrDefault(x => x.id == id);

            context.news.DeleteOnSubmit(p);
            context.SubmitChanges();

            return RedirectToAction("Index");
        }
    }
}