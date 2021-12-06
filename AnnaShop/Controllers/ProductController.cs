using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Interfaces;
using Model;
using Model.DTO;

namespace AnnaShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProduct product;
        public ProductController(IProduct product)
        {
            this.product = product;
        }
        // GET: product
        public ActionResult Index(int maLoai)
        {
            Session.Add("maLoai", maLoai);
            return View();
        }
        public ActionResult AllProduct()
        {
            return View();
        }
        public JsonResult GetAll()
        {
            List<ProductDTO> products = product.GetAll();
            return Json(products, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Get all product by category
        /// </summary>
        /// <returns></returns>
        public JsonResult GetAll_product()
        {
            List<ProductDTO> products= product.GetProductByCategory(Convert.ToInt32(Session["maLoai"].ToString()));
            return Json(products, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Get single product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult GetSingle()
        {
            var data = product.GetSingle(Convert.ToInt32(Session["maSP"].ToString()));
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Detail(int maSP)
        {
            Session.Add("maSP", maSP);
            return View();
        }
    }
}