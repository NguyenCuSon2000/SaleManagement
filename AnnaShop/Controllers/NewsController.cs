using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AnnaShop.Controllers
{
    public class NewsController : Controller
    {
        private readonly INews news;
        public NewsController(INews news)
        {
            this.news = news;
        }
        // GET: News
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetAllNews()
        {
            var data = news.GetAllNews();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetNews()
        {
            var data = news.GetNews();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}