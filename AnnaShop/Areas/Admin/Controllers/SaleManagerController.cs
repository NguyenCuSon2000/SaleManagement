using Interfaces;
using Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AnnaShop.Areas.Admin.Controllers
{
    public class SaleManagerController : Controller
    {
        private readonly ICart cart;
        public SaleManagerController(ICart cart)
        {
            this.cart = cart;
        }
        // GET: Admin/SaleManager
        public ActionResult Index()
        {
            if (Request.Cookies["LoginDetail"] == null || String.Compare(Request.Cookies["LoginDetail"].Values["Username"].ToString(), "annie", true) != 0)
            {
                //chuyển hướng
                Response.Redirect("/Login/Index");
            }
            return View();
        }
        public JsonResult GetAllOrder()
        {
            var data = cart.getAll();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}