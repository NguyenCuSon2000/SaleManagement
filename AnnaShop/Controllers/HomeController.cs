using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess;
using Interfaces;
using Model;

namespace AnnaShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProduct product;
        private readonly ICategory category;
        public HomeController(IProduct product, ICategory category)
        {
            this.product = product;
            this.category = category;
        }
        public ActionResult Index()
        {
         
            return View();
        }
        public JsonResult GetCategory()
        {
            var data = category.GetAll();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetProduct()
        {
            var data = product.GetAll();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetNewProduct()
        {
            var data = product.GetNew();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AboutUs()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
    }
}