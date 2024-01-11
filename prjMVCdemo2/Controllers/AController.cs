using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjMVCdemo2.Controllers
{
    public class AController : Controller
    {
        // GET: A
        public ActionResult ShowCountBySession()
        {
            int count = 0;
            if (Session["Count"] != null)
                count = (int)Session["Count"];
            count++;
            Session["Count"] = count;
            ViewBag.Count = count;
            return View();
        }
        public ActionResult ShowCountByCookie()
        {
            int count = 0;
            HttpCookie cookie = Request.Cookies["Count"];
            if (cookie != null)
                count = Convert.ToInt32(cookie.Value);
            count++;

            cookie = new HttpCookie("Count");
            cookie.Value = count.ToString();
            cookie.Expires = DateTime.Now.AddSeconds(20);
            Response.Cookies.Add(cookie);

            ViewBag.Count = count;
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}