using QuanLyTruongHoc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyTruongHoc.Controllers
{
    [Authorize]
    public class CommentController : Controller
    {
        // GET: Comment
        public ActionResult Create(string id)
        {
            if (id == null)
            {
                return Redirect("/");
            }


            QuanlytruonghocDataContext context = new QuanlytruonghocDataContext();

            /* Get id of current authenticated user */
            var email = User.Identity.Name;
            user u = context.users.Where(x => x.email == email).SingleOrDefault();
            var user_id = u.id;

            /* Get id of news */
            @new n = context.news.FirstOrDefault(x => x.slug == id);
            var news_id = n.id;

            if (Request.Form.Count > 0)
            {
                if (Request.Form["comment_text"] == "")
                {
                    /* Redirect -> Back */
                    return Redirect(Request.Headers["Referer"].ToString());
                }

                comment cmt = new comment();
                cmt.user_id = user_id;
                cmt.news_id = news_id;
                cmt.date = DateTime.Now;
                cmt.comment_text = Request.Form["comment_text"];

                context.comments.InsertOnSubmit(cmt);
                context.SubmitChanges();

                /* Redirect -> Back */
                return Redirect(Request.Headers["Referer"].ToString());
            }

            return Redirect("/");
        }

        [Authorize(Roles = "Quản Trị")]
        public ActionResult Delete(int id)
        {
            QuanlytruonghocDataContext context = new QuanlytruonghocDataContext();
            comment c = context.comments.FirstOrDefault(x => x.id == id);

            context.comments.DeleteOnSubmit(c);
            context.SubmitChanges();

            /* Redirect -> Back */
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}